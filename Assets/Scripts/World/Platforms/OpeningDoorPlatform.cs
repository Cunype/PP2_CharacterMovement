using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningDoorPlatform : MonoBehaviour
{
    public GameObject objectToActivate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Ball"))
        {
            objectToActivate.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Ball"))
        {
            objectToActivate.SetActive(true);
        }
    }
}
