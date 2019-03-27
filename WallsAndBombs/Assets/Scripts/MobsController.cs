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
            mob.Hurt(hurtValue);
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
}
