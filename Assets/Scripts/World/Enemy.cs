using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float shootInterval;
    private float lastShootTime;
    private Transform playerTransform;
    private bool playerInRange;
    public GameObject ignoredActor;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (playerInRange && Time.time - lastShootTime > shootInterval)
        {
            Shoot();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    void Shoot()
    {
        lastShootTime = Time.time;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.GetComponent<NormalBullet>().speed = 10.0f;
        bullet.GetComponent<NormalBullet>().direction = playerTransform.position - transform.position;
        bullet.GetComponent<NormalBullet>().direction.Normalize();
        bullet.GetComponent<NormalBullet>().ignoredGameobject = ignoredActor;
    }
}
