using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Parameters", menuName = "Parameters Data", order = 51)]
public class Parameters : ScriptableObject
{
    [Header("Spawners Common Stuff")]

    public float m_CellSize = 1f;
    public Vector2 m_FieldStart = new Vector2(0, 0);
    public Vector2 m_FieldEnd = new Vector2(8, 8);

    [Header("Mob Spawner")]

    public GameObject m_MobPrefab = null;
    public float m_MobSpawnDelay = 0f;
    public bool m_MobInfiniteSpawn = false;
    public float m_MobSpawnHeight = 0f;
    public bool m_MobSpawnOnFloor = true;
    public float m_MobSpawnProbability = 0.3f;
    public float m_MobShiftRadius = 0.3f;

    [Header("Bomb Spawner")]

    public GameObject m_BombPrefab = null;
    public float m_BombSpawnDelay = 0.5f;
    public bool m_BombInfiniteSpawn = true;
    public float m_BombSpawnHeight = 3f;
    public bool m_BombSpawnOnFloor = false;
    public float m_BombSpawnProbability = 0.3f;
    public float m_BombShiftRadius = 0.3f;
}
