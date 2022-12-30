using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public float depth;
    public Camera customCamera;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = customCamera.ScreenToWorldPoint(mousePosition + new Vector3(0, 0, depth));
        transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);
    }
}
