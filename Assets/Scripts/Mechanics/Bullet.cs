using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject area_;
    public Vector3 target_;

    public List<GameObject> affectedGameObjects;

    [SerializeField]
    private float size_;
    
    public float insideGravity_;
    
    [SerializeField]
    private bool isPositive_ = true;
    void Start()
    {
        if (!isPositive_) insideGravity_ *= -1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        StartCoroutine(GoToPosition());
    }

    void Expand()
    {
        StartCoroutine(ExpandArea());
    }
    
    IEnumerator GoToPosition()
    {
        while(Vector3.Distance(transform.position,target_) > 0.1f)
        {
            Vector3 dir = target_ - transform.position;
            dir.Normalize();
            transform.right = dir;
            transform.position += dir * (Time.deltaTime * 10.0f);
            yield return null;
        }
        Expand();
    }
    
    IEnumerator ExpandArea()
    {
        while(area_.transform.localScale.x < size_)
        {
            area_.transform.localScale = Vector3.Lerp(area_.transform.localScale, new Vector3(size_, size_, size_),
                Time.deltaTime);
            yield return null;
        }
    }
}
