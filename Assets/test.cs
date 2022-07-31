using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class test : MonoBehaviour
{
    public int length;
    public int count;
    public Vector2 minMax;
    
    public int radius = 25;
    public int step = 10;

    public bool circleVis;


    private void Update()
    {
        Debug.DrawLine(transform.position, transform.position + transform.forward * 25f, Color.red);
    }

    private void OnDrawGizmos()
    {
        if (circleVis)
            OrbitVisualize();
    }
    /*
    public void DrawLine()
    {
        Debug.DrawLine(Vector3.zero, Vector3.forward * length, Color.blue);
        float lineSegment = length / count;
        for (int i = 0; i < count; i++)
        {
            float pos = lineSegment * i + Random.Range(minMax.x, minMax.y);
            Debug.DrawRay(new Vector3(0,0, pos), Vector3.up, Color.green);
        }
    }
    */
    private void OrbitVisualize()
    {
        float angleMltp = 2f / step;
            
        for (int i = 0; i < step; i++)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(GetPointOnCircle(angleMltp * i * Mathf.PI),
                GetPointOnCircle(angleMltp * (i + 1) * Mathf.PI));
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(GetPointOnCircle(angleMltp * i * Mathf.PI), 1f);
        }
    }
    
    private Vector3 GetPointOnCircle(float angle)
    {
        Vector3 position;
        position.x = radius * Mathf.Cos(angle);
        position.y = 0;
        position.z = radius * Mathf.Sin(angle);
        return position;
    }
}
