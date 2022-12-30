using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DigitalRuby.Tween;

public class PathController : MonoBehaviour
{
    public List<Transform> points;

    private Transform currentTransform = null;
    private int currentPointIndex = 0;
    private bool move = false;
    private Action finished;
    private bool currentlyTweening = false;

    public float speed = 3f;
    public float rotationDuration = 1f;
    public float roationAngle = 30f;
    public bool funnyMovement;
    public bool isMovingFunny = false;

    public void MoveTransformAlongPath(Transform moveTransform, Action finished)
    {
        moveTransform.position = points[0].position;
        move = true;
        currentTransform = moveTransform;
        this.finished = finished;
        currentPointIndex = 1;
    }

    private void Update()
    {
        if(move && currentTransform != null)
        {
            if(currentPointIndex < points.Count)
            {
                if (!currentlyTweening)
                {
                    Debug.Log(" Start tween!");
                    currentlyTweening = true;
                    TweenFactory.Tween("smoothMovement", currentTransform.position, points[currentPointIndex].position, speed, TweenScaleFunctions.CubicEaseInOut, (t) => currentTransform.position = t.CurrentValue, (t) => {
                        Debug.Log("Moved to " + currentPointIndex);
                        currentlyTweening = false;
                        currentPointIndex++;
                    });
                }
            } else
            {
                move = false;
                currentPointIndex = 0;
                finished();
                currentTransform = null;
                TweenFactory.RemoveTweenKey("rotateLeft", TweenStopBehavior.DoNotModify);
                TweenFactory.RemoveTweenKey("smoothMovement", TweenStopBehavior.DoNotModify);
            }
        }

        if(move && funnyMovement && !isMovingFunny && currentTransform != null)
        {
            isMovingFunny = true;
            TweenFactory.Tween("rotateLeft", currentTransform.rotation, Quaternion.Euler(new Vector3(0, 0, roationAngle)), rotationDuration, TweenScaleFunctions.QuadraticEaseInOut, (t) => currentTransform.rotation = t.CurrentValue, (t) =>
            {
            }).ContinueWith(new QuaternionTween().Setup(Quaternion.Euler(new Vector3(0, 0, roationAngle)), Quaternion.Euler(new Vector3(0, 0, -roationAngle)), rotationDuration, TweenScaleFunctions.QuadraticEaseIn, (t) => currentTransform.rotation = t.CurrentValue, (t) => { isMovingFunny = false; }));
        }
    }
}
