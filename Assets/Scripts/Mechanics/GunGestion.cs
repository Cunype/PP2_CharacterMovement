using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunGestion : MonoBehaviour
{
    public bool isAntiGravityAvailable_ = false;
    public bool isGravitySecondAvailable_ = false;

    public GameObject bulletPrefab_;
    public GameObject bulletPrefabDecrease_;

    private Camera mainCamera_;
    private Vector3 mouseAim_;

    [SerializeField]
    private float aimSpeed_ = 2.0f;

    public GameObject arm_;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera_ = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (arm_)
        {
            mouseAim_ = mainCamera_.ScreenToWorldPoint(Input.mousePosition);
            mouseAim_.z = arm_.transform.position.z;

            Vector3 dir = mouseAim_ - arm_.transform.position;
            dir.Normalize();

            arm_.transform.right = Vector3.Lerp(arm_.transform.right, dir, aimSpeed_ * Time.deltaTime);
        }
        if (Input.GetButtonDown("Fire2") && isGravitySecondAvailable_)
        {
            ErasePrevious();
            Shoot(false);
        }
    }

    void ErasePrevious()
    {
        GameObject[] objectsToDelete = GameObject.FindGameObjectsWithTag("ToDelete");

        foreach (GameObject obj in objectsToDelete)
        {
            Bullet bullet = obj.GetComponent<Bullet>();
            if (bullet)
            {
                foreach (var affected in bullet.affectedGameObjects)
                {
                    if(affected != null)
                    {
                        Gravity gravity = affected.GetComponent<Gravity>();
                        if (gravity)
                        {
                            gravity.gravityScale_ = 4.25f;
                        }
                    }
                }
            }
            Destroy(obj);
        }
    }

    void Shoot(bool increase)
    {
        if (increase)
        {
            GameObject tmp = Instantiate(bulletPrefab_, arm_.transform.position, Quaternion.identity);
            Bullet lastBullet_ = tmp.GetComponent<Bullet>();
            if (lastBullet_)
            {
                lastBullet_.target_ = mouseAim_;
                lastBullet_.Shoot();
            }
        }
        else
        {
            GameObject tmp = Instantiate(bulletPrefabDecrease_, arm_.transform.position, Quaternion.identity);
            Bullet lastBullet_ = tmp.GetComponent<Bullet>();
            if (lastBullet_)
            {
                lastBullet_.target_ = mouseAim_;
                lastBullet_.Shoot();
            }
        }

    }
}
