using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobsController
{
    CellsGridSpawner m_MobsSpawner = null;
    List<Mob> m_AllMobs = new List<Mob>();

    public MobsController(CellsGridSpawner mobsSpawner)
    {
        m_MobsSpawner = mobsSpawner;
        m_MobsSpawner.OnObjectSpawned += OnMobSpawned;
    }

    void OnMobSpawned(GameObject mob)
    {
        m_AllMobs.Add(mob.GetComponent<Mob>());
    }
}
