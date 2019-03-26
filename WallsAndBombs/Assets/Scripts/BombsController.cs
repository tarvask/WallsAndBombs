using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombsController
{
    CellsGridSpawner m_BombsSpawner = null;
    List<Bomb> m_AllBombs = new List<Bomb>();

    GameObject m_ExplosionPrefab = null;
    float m_ExplosionDuration = 0.3f;
    float m_ExplosionSize = 1f;

    public GameObject ExplosionPrefab { set { m_ExplosionPrefab = value; } }
    public float ExplosionDuration { set { m_ExplosionDuration = value; } }
    public float ExplosionSize { set { m_ExplosionSize = value; } }

    public BombsController(CellsGridSpawner bombsSpawner)
    {
        m_BombsSpawner = bombsSpawner;
        m_BombsSpawner.OnObjectSpawned += OnBombSpawned;
    }

    void OnBombSpawned(GameObject bombGO)
    {
        Bomb bomb = bombGO.GetComponent<Bomb>();
        bomb.OnBombExploded += OnBombExploded;
        m_AllBombs.Add(bomb);
    }

    void OnBombExploded(Transform bombTransform)
    {
        CreateExplosion(bombTransform);
        Object.Destroy(bombTransform.gameObject);
    }

    void CreateExplosion(Transform bombTransform)
    {
        GameObject explosionGO = Object.Instantiate(m_ExplosionPrefab, bombTransform.parent);
        explosionGO.transform.localPosition = bombTransform.localPosition;
        Explosion explosion = explosionGO.GetComponent<Explosion>();
        explosion.Duration = m_ExplosionDuration;
        explosion.Size = m_ExplosionSize;
    }
}
