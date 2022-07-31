using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitVisualization : MonoBehaviour
{
    private LineRenderer _line;
    private float _radius;
    public int smoothness = 16;
    private Transform _target;

    private void Awake()
    {
        _line = GetComponentInChildren<LineRenderer>();
        _target = gameObject.transform.parent.GetComponent<Transform>();
    }

    private void Start()
    {
        DrawOrbit();
    }

    private void DrawOrbit()
    {
        _radius = Vector3.Distance(transform.position, _target.position);
        float angle = 2f / smoothness;

        for (int i = 0; i < smoothness + 1; i++)
        {
            _line.enabled = true;
            _line.positionCount = smoothness + 1;
            _line.SetPosition(i, GetPointOnCircle(angle * i * Mathf.PI));
            _line.colorGradient = SolarSystemGenerator.current.orbitColor;
            
            //_line.startColor = Color.cyan - new Color(0,0,0,.9f);
            //_line.endColor = Color.cyan - new Color(0,0,0,.9f);
            
            _line.widthMultiplier = 2;
        }
    }
    
    /*
    private void OnDrawGizmos()
    {
        _radius = Vector3.Distance(transform.position, Vector3.one);
        float angleMltp = 2f / smoothness;
        
        for (int i = 0; i < smoothness; i++)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(GetPointOnCircle(angleMltp * i * Mathf.PI), GetPointOnCircle(angleMltp * (i+1) * Mathf.PI));
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(GetPointOnCircle(angleMltp * i * Mathf.PI), 1f);
        }
    }
    */
    
    private Vector3 GetPointOnCircle(float angle)
    {
        Vector3 position;
        position.x = _target.position.x + _radius * Mathf.Cos(angle);
        position.y = 0;
        position.z = _target.position.z + _radius * Mathf.Sin(angle);
        return position;
    }
}
