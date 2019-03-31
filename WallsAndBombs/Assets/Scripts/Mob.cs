using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    // serialized for debug
    [SerializeField] int m_HealthPoints = 50;

    public int HealthPoints { set { m_HealthPoints = value; } }

    public event System.Action<Mob> OnMobDied;

    public void Hurt(int hurtValue)
    {
        m_HealthPoints -= hurtValue;

        if (m_HealthPoints <= 0)
        {
            if (OnMobDied != null)
            {
                OnMobDied(this);
            }
        }
    }
}
