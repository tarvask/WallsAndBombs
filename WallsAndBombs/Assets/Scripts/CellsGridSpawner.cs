using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellsGridSpawner
{
    Transform m_SpawnTransform = null;
    GameObject m_PrefabToSpawn = null;
    float m_CellSize = 1f;
    Vector2 m_FieldStart = new Vector2(0, 0);
    Vector2 m_FieldEnd = new Vector2(8, 8);
    float m_SpawnHeight = 0f;
    bool m_SpawnOnFloor = false;
    float m_SpawnProbability = 0.3f;
    float m_ShiftRadius = 0.3f;

    // public setters for parameters
    public Transform SpawnTransform { set { m_SpawnTransform = value; } }
    public GameObject PrefabToSpawn { set { m_PrefabToSpawn = value; } }
    public float CellSize { set { m_CellSize = value; } }
    public Vector2 FieldStart { set { m_FieldStart = value; } }
    public Vector2 FieldEnd { set { m_FieldEnd = value; } }
    public float SpawnHeight { set { m_SpawnHeight = value; } }
    public bool SpawnOnFloor { set { m_SpawnOnFloor = value; } }
    public float SpawnProbability { set { m_SpawnProbability = value; } }
    public float ShiftRadius { set { m_ShiftRadius = value; } }

    public void DoSpawn()
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
                SpawnWithProbability(currentSpawnShiftedPosition, m_SpawnOnFloor, m_SpawnProbability);

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

    void SpawnWithProbability(Vector2 spawnPosition, bool spawnOnFloor, float probabilityFactor)
    {
        if (Random.Range(0f, 1f) < probabilityFactor)
        {
            SpawnSingleMob(spawnPosition, m_SpawnOnFloor);
        }
    }

    void SpawnSingleMob(Vector2 spawnPosition, bool spawnOnFloor)
    {
        GameObject mob = Object.Instantiate(m_PrefabToSpawn, m_SpawnTransform);
        // get height of mob to correctly spawn mob above the floor
        float mobSpawnHeight = m_SpawnHeight;

        if (spawnOnFloor)
        {
            mobSpawnHeight += GetColliderHeight(mob);
        }

        mob.transform.localPosition = new Vector3(spawnPosition.x, mobSpawnHeight / 2, spawnPosition.y);
    }

    float GetColliderHeight(GameObject spawnedObject)
    {
        float colliderHeight = 0;
        CapsuleCollider spawnedObjectCollider = spawnedObject.GetComponent<CapsuleCollider>();

        if (spawnedObjectCollider != null)
        {
            colliderHeight = spawnedObjectCollider.height * spawnedObject.transform.localScale.y;
        }

        return colliderHeight;
    }
}
