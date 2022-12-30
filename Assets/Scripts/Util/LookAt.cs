using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform lookat;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = new Vector3(lookat.transform.position.x, lookat.transform.position.y, transform.position.z);
        transform.LookAt(lookat);
    }
}
