using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    private Movement movement;
    public float shootCooldown;
    private float lastShootTime;

    private void Start()
    {
        movement = GetComponent<Movement>();
    }

    void Update()
    {
        if (Time.time - lastShootTime > shootCooldown && Input.GetButtonDown("Fire1"))
        {
            Shooting();
        }
    }

    void Shooting()
    {
        lastShootTime = Time.time;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.GetComponent<NormalBullet>().lookingDir = (int)movement.currentDirection_;
        bullet.GetComponent<NormalBullet>().speed = 10f;
        bullet.GetComponent<NormalBullet>().direction = transform.right * movement.currentDirection_;
        bullet.GetComponent<NormalBullet>().ignoredGameobject = this.gameObject;
    }
}
