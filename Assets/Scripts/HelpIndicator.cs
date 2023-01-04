using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpIndicator : MonoBehaviour
{

    public PathController pathController;
    void Start()
    {
        ShowHelpIndicator();
    }

    public void ShowHelpIndicator()
    {
        Debug.Log("transform : " + transform);
        pathController.MoveTransformAlongPath(transform, () => {
            ShowHelpIndicator();
        });
    }

    public void StopShowingHelpIndicator()
    {
        gameObject.SetActive(false);
        StopAllCoroutines();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
