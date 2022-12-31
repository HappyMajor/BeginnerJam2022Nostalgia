using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopFill : MonoBehaviour
{

    public RectTransform fillRect;
    public Image image;
    public float multiplicator;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.GetComponent<RectTransform>().anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
