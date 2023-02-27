using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShootBasketball : MonoBehaviour
{
    public bool isShoot = false;
    public Transform targetPosition;
    public Animator animator;
    public float vx;
    public Transform startTrans;
    public Vector3 bezierControlPoint;
    public float height;
    public int resolution;
    public Vector3[] _path;
    
    public void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {  
            animator.SetTrigger("Shoot");
            if (isShoot)
            {

            }
        }
        
    }

    public void shootBaketball()
    {
        isShoot = true;
        startTrans = transform;
        bezierControlPoint = (startTrans.position + targetPosition.position) * 0.5f + Vector3.up * height;
        _path = new Vector3[resolution];
        for(int i = 0; i < resolution; i++)
        {
            var t = (i + 1) / (float)resolution;
            _path[i] = GetBezierPoint(t, startTrans.position, bezierControlPoint, targetPosition.position);//贝塞尔曲线获得t时的路径点
        }
    }

    public static Vector3 GetBezierPoint(float t,Vector3 start,Vector3 center,Vector3 end)
    {
        return (1 - t) * (1 - t) * start + 2 * t * (1 - t) * center + t * t * end;  //贝塞尔曲线
    }
}
