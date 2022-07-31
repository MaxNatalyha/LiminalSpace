using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : CelestialObject
{
    public void Init(float speed, Transform target)
    {
        this._speed = speed;
        this._target = target;
    }

    private void Update()
    {
        RotateAround(_target);
    }

}
