using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public float FollowSpeed = 2f;
    public float yOffset =1f;
    public Transform target;
    
    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x,target.position.y + yOffset,-10f);
        transform.position = Vector3.Slerp(transform.position,newPos,FollowSpeed*Time.deltaTime);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, 95.0f, transform.position.x), Mathf.Clamp(transform.position.y,8.0f, transform.position.y), -10f);
    }
}
