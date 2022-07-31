using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CelestialObject : MonoBehaviour
{
    public string Name;
    protected float _speed;
    protected Transform _target;
    
    protected virtual void RotateAround(Transform target)
    {
        transform.RotateAround(target.position, Vector3.up, this._speed * Time.deltaTime / 100); 
    }
}
