using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobsController
{
    CellsGridSpawner m_MobsSpawner = null;
    BombsController m_BombsController = null;
    List<Mob> m_AllMobs = new List<Mob>();

    int m_MobHealthPoints = 50;

    public int MobHealthPoints { set { m_MobHealthPoints = value; } }

    public MobsController(CellsGridSpawner mobsSpawner, BombsController bombsController)
    {
        m_MobsSpawner = mobsSpawner;
        m_MobsSpawner.OnObjectSpawned += OnMobSpawned;
        m_BombsController = bombsController;
        m_BombsController.OnSomeBombExploded += HurtMobs;
    }

    void OnMobSpawned(GameObject mobGO)
    {
        Mob mob = mobGO.GetComponent<Mob>();
        mob.HealthPoints = m_MobHealthPoints;
        mob.OnMobDied += RemoveMob;
        m_AllMobs.Add(mob);
    }

    void RemoveMob(Mob deadMob)
    {
        m_AllMobs.Remove(deadMob);
        Object.Destroy(deadMob.gameObject);
    }

    void HurtMobs(Transform explosionTransform, float hurtRadius, int hurtValue)
    {
        List<Mob> mobsToHurt = GetMobsInRadius(explosionTransform, hurtRadius);

        foreach (Mob mob in mobsToHurt)
        {
            float unshelteredPart = CheckMobShelter(mob, explosionTransform.localPosition);
            mob.Hurt(Mathf.CeilToInt(hurtValue * unshelteredPart));
        }
    }

    List<Mob> GetMobsInRadius(Transform explosionTransform, float hurtRadius)
    {
        List<Mob> result = new List<Mob>();

        foreach (Mob mob in m_AllMobs)
        {
            Vector3 deltaPosition = mob.transform.localPosition - explosionTransform.localPosition;

            if (deltaPosition.sqrMagnitude < hurtRadius * hurtRadius)
            {
                result.Add(mob);
            }
        }

        return result;
    }

    float CheckMobShelter(Mob mob, Vector3 explosionPosition)
    {
        int checksNumber = 16;
        int unshelteredChecks = 0;
        CapsuleCollider mobCollider = mob.GetComponent<CapsuleCollider>();
        float mobRadius = mobCollider.radius * mob.transform.localScale.x;

        GeometryHelper.FindTouchPoints(explosionPosition, mob.transform.localPosition, mobRadius,
            out Vector3 firstTouchPoint, out Vector3 secondTouchPoint, out float distance);

        // do raycasting
        // set precise distance to avoid shelterin
        unshelteredChecks = CountUnshelteredCheckpoints(explosionPosition, firstTouchPoint, secondTouchPoint, checksNumber, distance);

        return unshelteredChecks / checksNumber;
    }

    int CountUnshelteredCheckpoints(Vector3 explosionPosition, Vector3 firstTouchPoint, Vector3 secondTouchPoint, int checksNumber, float distance)
    {
        int unshelteredChecks = 0;

        // build vector with checkpoints
        Vector3 touchPointsVector = secondTouchPoint - firstTouchPoint;
        int layerMask = LayerMask.GetMask("Mobs");
        // invert layer mask to raycast all except the Mobs
        layerMask = ~layerMask;

        for (int i = 0; i < checksNumber; i++)
        {
            Vector3 currentCheckPoint = firstTouchPoint + touchPointsVector * i / (checksNumber - 1);
            Vector3 direction = currentCheckPoint - explosionPosition;

            // no shelters on the way
            if (!Physics.Raycast(explosionPosition, direction, distance, layerMask))
            {
                unshelteredChecks++;
            }
        }

        return unshelteredChecks;
    }
}
