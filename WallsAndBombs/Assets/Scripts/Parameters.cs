using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Parameters", menuName = "Parameters Data", order = 51)]
public class Parameters : ScriptableObject
{
    [Header("Mob Spawner")]

    public GameObject m_MobPrefab = null;
    public float m_CellSize = 1f;
    public Vector2 m_FieldStart = new Vector2(0, 0);
    public Vector2 m_FieldEnd = new Vector2(8, 8);
    public float m_SpawnProbability = 0.3f;
    public float m_ShiftRadius = 0.3f;
}
