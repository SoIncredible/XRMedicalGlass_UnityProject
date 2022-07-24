using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatetest : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform center;
    // Update is called once per frame
    void Update()
    {
        
        transform.RotateAround(center.position,center.up,20*Time.deltaTime);
    }
}
