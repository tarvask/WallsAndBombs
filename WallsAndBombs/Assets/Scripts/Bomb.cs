using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public event System.Action<Transform> OnBombDetonated;

    void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.name == "Plane")
        //{
            // notify world of explosion
            OnBombDetonated(transform);
        //}
    }
}
