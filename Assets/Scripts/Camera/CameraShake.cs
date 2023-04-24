using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }

    private void Awake()
    {
        // Make sure there's only one instance of this class
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Shake(float duration, float magnitude)
    {
        StartCoroutine(ShakeCoroutine(duration, magnitude));
    }

    private IEnumerator ShakeCoroutine(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition += new Vector3( x,  y, 0.0f);

            elapsed += Time.deltaTime;

            yield return null;
        }
    }
}