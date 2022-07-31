using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialTrail : MonoBehaviour
{
    public bool isSatellite;
    private TrailRenderer _trail;
    
    private void Awake()
    {
        _trail = GetComponent<TrailRenderer>();
    }

    private void Start()
    {
        _trail.startWidth = gameObject.transform.parent.localScale.x;
        if (isSatellite)
        {
            _trail.startWidth = gameObject.transform.parent.localScale.x * 100;
            _trail.time = 25f;
        }
    }
}
