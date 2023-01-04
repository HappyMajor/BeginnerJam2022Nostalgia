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
    public bool smooth;

    public void MoveTransformAlongPath(Transform moveTransform, Action finished)
    {
        TweenFactory.RemoveTweenKey("rotateLeft" + gameObject.name, TweenStopBehavior.Complete);
        TweenFactory.RemoveTweenKey("smoothMovement" + gameObject.name, TweenStopBehavior.Complete);
        moveTransform.position = points[0].position;
        currentTransform = moveTransform;
        move = true;
        this.finished = finished;
        currentPointIndex = 1;
        currentlyTweening = false;
    }

    private void Update()
    {
        if(move && currentTransform != null)
        {
            if (currentPointIndex < points.Count)
              {
                if (!currentlyTweening)
                {
                    Func<float, float> scale;

                    if(smooth)
                    {
                        scale = TweenScaleFunctions.QuadraticEaseOut;
                    } else
                    {
                        scale = TweenScaleFunctions.Linear;
                    }
                    currentlyTweening = true;
                    TweenFactory.Tween("smoothMovement" + gameObject.name, currentTransform.position, points[currentPointIndex].position, speed, scale, (t) => currentTransform.position = t.CurrentValue, (t) => {
                        currentlyTweening = false;
                        currentPointIndex++;
                    });
                }
            } else
            {
                move = false;
                currentPointIndex = 0;
                currentTransform = null;
                TweenFactory.RemoveTweenKey("rotateLeft" + gameObject.name, TweenStopBehavior.DoNotModify);
                TweenFactory.RemoveTweenKey("smoothMovement" + gameObject.name, TweenStopBehavior.Complete);
                finished();
            }
        }

        if(move && funnyMovement && !isMovingFunny && currentTransform != null)
        {
            isMovingFunny = true;
            TweenFactory.Tween("rotateLeft" + gameObject.name, currentTransform.rotation, Quaternion.Euler(new Vector3(0, 0, roationAngle)), rotationDuration, TweenScaleFunctions.QuadraticEaseInOut, (t) => currentTransform.rotation = t.CurrentValue, null).ContinueWith(new QuaternionTween().Setup(Quaternion.Euler(new Vector3(0, 0, roationAngle)), Quaternion.Euler(new Vector3(0, 0, -roationAngle)), rotationDuration, TweenScaleFunctions.QuadraticEaseIn, (t) => currentTransform.rotation = t.CurrentValue, (t) => { isMovingFunny = false; }));
        }
    }
}
