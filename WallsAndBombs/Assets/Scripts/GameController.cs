using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Parameters m_Parameters = null;
    [SerializeField]
    private HierarchyRootsCarrier m_RootsCarrier = null;

    private CellsGridSpawner m_MobSpawner = null;
    private CellsGridSpawner m_BombSpawner = null;

    private void Awake()
    {
        m_MobSpawner = new CellsGridSpawner(this);
        m_BombSpawner = new CellsGridSpawner(this);

        InitParameters();
    }

    private void Start()
    {
        StartCoroutine(m_MobSpawner.DoSpawn());
        StartCoroutine(m_BombSpawner.DoSpawn());
    }

    private void InitParameters()
    {
        // MobSpawner
        m_MobSpawner.SpawnTransform = m_RootsCarrier.m_MobsRoot;
        m_MobSpawner.PrefabToSpawn = m_Parameters.m_MobPrefab;
        m_MobSpawner.SpawnDelay = m_Parameters.m_MobSpawnDelay;
        m_MobSpawner.InfiniteSpawn = m_Parameters.m_MobInfiniteSpawn;
        m_MobSpawner.CellSize = m_Parameters.m_CellSize;
        m_MobSpawner.FieldStart = m_Parameters.m_FieldStart;
        m_MobSpawner.FieldEnd = m_Parameters.m_FieldEnd;
        m_MobSpawner.SpawnHeight = m_Parameters.m_MobSpawnHeight;
        m_MobSpawner.SpawnOnFloor = m_Parameters.m_MobSpawnOnFloor;
        m_MobSpawner.SpawnProbability = m_Parameters.m_MobSpawnProbability;
        m_MobSpawner.ShiftRadius = m_Parameters.m_MobShiftRadius;

        // BombSpawner
        m_BombSpawner.SpawnTransform = m_RootsCarrier.m_BombsRoot;
        m_BombSpawner.PrefabToSpawn = m_Parameters.m_BombPrefab;
        m_BombSpawner.SpawnDelay = m_Parameters.m_BombSpawnDelay;
        m_BombSpawner.InfiniteSpawn = m_Parameters.m_BombInfiniteSpawn;
        m_BombSpawner.CellSize = m_Parameters.m_CellSize;
        m_BombSpawner.FieldStart = m_Parameters.m_FieldStart;
        m_BombSpawner.FieldEnd = m_Parameters.m_FieldEnd;
        m_BombSpawner.SpawnHeight = m_Parameters.m_BombSpawnHeight;
        m_BombSpawner.SpawnOnFloor = m_Parameters.m_BombSpawnOnFloor;
        m_BombSpawner.SpawnProbability = m_Parameters.m_BombSpawnProbability;
        m_BombSpawner.ShiftRadius = m_Parameters.m_BombShiftRadius;
    }
}
