using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform target;
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.transform.position = target.transform.position;
    }
}
