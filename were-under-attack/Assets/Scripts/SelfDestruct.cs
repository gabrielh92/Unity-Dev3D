using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [Tooltip("s")][SerializeField] float lifetime = 2f;
    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
