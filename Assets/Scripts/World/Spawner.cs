using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab_;
    
    private bool Activated = true;

    [SerializeField]
    private float spawnWait_ = 0.5f;
    private void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (Activated)
        {
            Instantiate(prefab_, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnWait_);
        }
    }
}
