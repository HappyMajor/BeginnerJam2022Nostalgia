using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

//Needs to be placed on a component with a collider because of the use of the OnMouseDrag function
public class Draggable : MonoBehaviour
{
    public Transform dragTargetTransform;

    public float scaleFactorHover;
    public float scaleFactorDrag;
    public Vector3 startScale;

    private Vector3 dragTargetPosition;
    private bool isDragged = false;
    private bool isHoveredOver = false;
    private bool hoverEventBlockedUntilExit = false;

    private void Start()
    {
        startScale = transform.localScale;
    }

    void OnMouseDrag()
    {
        Vector3 screenMousePosition = Input.mousePosition;
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(screenMousePosition);
        dragTargetPosition = worldMousePosition;

        TweenFactory.Tween("drag", dragTargetTransform.position, dragTargetPosition, 0.15f, TweenScaleFunctions.Linear, OnDragTweenUpdate, OnDragTweenFinished);



        if (!isDragged) {
            isDragged = true;
            TweenFactory.Tween("scaleUpEffect", dragTargetTransform.localScale, startScale * scaleFactorDrag, 0.2f, TweenScaleFunctions.QuadraticEaseIn, OnScaleTweenUpdate, null);
        }
    }

    private void OnMouseOver()
    {
        if(!isHoveredOver && !isDragged && !hoverEventBlockedUntilExit)
        {
            isHoveredOver = true;
            TweenFactory.Tween("scaleUpEffect", dragTargetTransform.localScale, startScale * scaleFactorHover, 0.2f, TweenScaleFunctions.QuadraticEaseIn, OnScaleTweenUpdate, null);
        }
    }

    private void OnMouseExit()
    {
        if(isHoveredOver && !isDragged)
        {
            isHoveredOver = false;
            TweenFactory.Tween("scaleDownEffect", dragTargetTransform.localScale, startScale, 0.2f, TweenScaleFunctions.QuadraticEaseIn, OnScaleTweenUpdate, null);
        }

        if(hoverEventBlockedUntilExit)
        {
            hoverEventBlockedUntilExit = false;
        }
    }

    private void OnMouseUp()
    {
        if (isDragged) {
            isDragged = false;
            TweenFactory.Tween("scaleDownEffect", dragTargetTransform.localScale, startScale, 0.2f, TweenScaleFunctions.QuadraticEaseIn, OnScaleTweenUpdate, null);
            hoverEventBlockedUntilExit = true;
        }
    }

    private void OnDestroy()
    {
        TweenFactory.RemoveTweenKey("scaleDownEffect", TweenStopBehavior.DoNotModify);
        TweenFactory.RemoveTweenKey("scaleUpEffect", TweenStopBehavior.DoNotModify);
        TweenFactory.RemoveTweenKey("drag", TweenStopBehavior.DoNotModify);
    }

    void OnScaleTweenUpdate(ITween<Vector3> progress)
    {
        dragTargetTransform.localScale = progress.CurrentValue;
    }

    void OnRotateTweenUpdate(ITween<Vector3> progress)
    {
        dragTargetTransform.eulerAngles = progress.CurrentValue;
    }

    void OnDragTweenFinished(ITween<Vector3> progress)
    {
        dragTargetTransform.position = new Vector3(progress.CurrentValue.x, progress.CurrentValue.y, 0f);
        isDragged = false;
    }

    void OnDragTweenUpdate(ITween<Vector3> progress)
    {
        dragTargetTransform.position = new Vector3(progress.CurrentValue.x, progress.CurrentValue.y, 0f);
    }
}
