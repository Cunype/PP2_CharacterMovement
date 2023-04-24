using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NormalBullet : MonoBehaviour
{
    public float speed = 10.0f;
    public float lifetime = 2.0f;

    public Vector3 direction;
    public int lookingDir = 1;

    private Gravity gravity;
    public GameObject ignoredGameobject = null;

    void Start()
    {
        transform.position += direction * 2.0f * lookingDir;
        Destroy(gameObject, lifetime);
    }
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject != ignoredGameobject)
        {
            if (other.CompareTag("Destructible"))
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
                return;
            }
            if ((other.CompareTag("Player") || other.CompareTag("Enemy")) && !other.CompareTag("Bullet"))
            {
                if (other.CompareTag("Player")){
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    return;
                }
                Destroy(other.gameObject.transform.parent.gameObject);
                Destroy(other.gameObject);
                return;
            }
            if ((!other.CompareTag("Bullet")) && !other.CompareTag("Ignore"))
            {
                Destroy(gameObject);
                return;
            }
        }
    }
}
