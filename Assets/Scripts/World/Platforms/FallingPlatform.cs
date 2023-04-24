using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField]
    private float fallDelay_ = 1.0f;
    [SerializeField]
    private float destroyDelay_ = 2.0f;

    private Transform orginalPos_;

    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        throw new NotImplementedException();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Fall());
        }
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(1.0f);
        rb.bodyType = RigidbodyType2D.Dynamic;
        Destroy(gameObject, 2.0f);
    }
}
