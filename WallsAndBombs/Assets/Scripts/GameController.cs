using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Parameters m_Parameters = null;

    [SerializeField]
    private MobSpawner m_MobSpawner = null;

    private void Awake()
    {
        InitParameters();
    }

    private void Start()
    {
        m_MobSpawner.LateStart();
    }

    private void InitParameters()
    {
        // MobSpawner
        m_MobSpawner.MobPrefab = m_Parameters.m_MobPrefab;
        m_MobSpawner.CellSize = m_Parameters.m_CellSize;
        m_MobSpawner.FieldStart = m_Parameters.m_FieldStart;
        m_MobSpawner.FieldEnd = m_Parameters.m_FieldEnd;
        m_MobSpawner.SpawnProbability = m_Parameters.m_SpawnProbability;
        m_MobSpawner.ShiftRadius = m_Parameters.m_ShiftRadius;
    }
}
