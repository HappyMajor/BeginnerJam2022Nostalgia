using UnityEngine;
using System.Collections;

public class NewMonoBehaviour : MonoBehaviour
{
    public Transform lookat;

    void Start()
    {

    }

    void Update()
    {
        Camera.main.transform.LookAt(lookat);
    }
}
