using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    GameObject m_MobPrefab = null;
    float m_CellSize = 1f;
    Vector2 m_FieldStart = new Vector2(0, 0);
    Vector2 m_FieldEnd = new Vector2(8, 8);
    float m_SpawnProbability = 0.3f;
    float m_ShiftRadius = 0.3f;

    // public setters for parameters
    public GameObject MobPrefab { set { m_MobPrefab = value; } }
    public float CellSize { set { m_CellSize = value; } }
    public Vector2 FieldStart { set { m_FieldStart = value; } }
    public Vector2 FieldEnd { set { m_FieldEnd = value; } }
    public float SpawnProbability { set { m_SpawnProbability = value; } }
    public float ShiftRadius { set { m_ShiftRadius = value; } }

    public void LateStart()
    {
        SpawnMobs();
    }

    void SpawnMobs()
    {
        Vector2 currentSpawnPosition = m_FieldStart;
        int xIterations = 0;
        int yIterations = 0;

        while (currentSpawnPosition.y < m_FieldEnd.y - m_CellSize / 2)
        {
            xIterations = 0;
            currentSpawnPosition.x = m_FieldStart.x;

            while (currentSpawnPosition.x < m_FieldEnd.x - m_CellSize / 2)
            {
                // get position of cell corner
                Vector2 currentCellPosition = m_FieldStart + new Vector2(m_CellSize * xIterations, m_CellSize * yIterations);
                // get position of cell center
                Vector2 currentCellCenterPosition = currentCellPosition + 0.5f * new Vector2(m_CellSize, m_CellSize);
                // save new spawn position to control loop
                currentSpawnPosition = currentCellCenterPosition;
                // add random shift within circle border
                Vector2 currentSpawnShiftedPosition = currentCellCenterPosition + GetRandomShiftWithinCircle(m_ShiftRadius);
                SpawnWithProbability(currentSpawnShiftedPosition, m_SpawnProbability);

                xIterations++;
            }

            yIterations++;
        }
    }

    Vector2 GetRandomShiftWithinCircle(float shiftRadius)
    {
        Vector2 result = Vector2.zero;
        int randomAngle = Random.Range(0, 360);
        float randomShiftRadius = Random.Range(0, shiftRadius);
        result.x = randomShiftRadius * Mathf.Cos(Mathf.Deg2Rad * randomAngle);
        result.y = randomShiftRadius * Mathf.Sin(Mathf.Deg2Rad * randomAngle);

        return result;
    }

    void SpawnWithProbability(Vector2 spawnPosition, float probabilityFactor)
    {
        if (Random.Range(0f, 1f) < probabilityFactor)
        {
            SpawnSingleMob(spawnPosition);
        }
    }

    void SpawnSingleMob(Vector2 spawnPosition)
    {
        GameObject mob = Instantiate(m_MobPrefab, transform);
        // get height of mob to correctly spawn mob above the floor
        float mobHeight = mob.GetComponent<CapsuleCollider>().height * mob.transform.localScale.y;
        mob.transform.localPosition = new Vector3(spawnPosition.x, mobHeight / 2, spawnPosition.y);
    }
}
