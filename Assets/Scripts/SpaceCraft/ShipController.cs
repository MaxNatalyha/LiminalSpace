using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ShipController : MonoBehaviour
{
    public float speed = 255f;
    public float dstTreshold;
    public float angularSpeed;
    public int acceleration;
    
    private Rigidbody _rb;
    private bool _isTarget;
    
    private Vector3 _targetPoint;
    private Quaternion _targetRotation;

    private PathVisualization _pathVisualization;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _pathVisualization = GetComponent<PathVisualization>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
            _rb.AddForce(transform.forward * acceleration, ForceMode.Acceleration);
        
        if (Input.GetMouseButton(1))
        {
            //Plane plane = new Plane(Vector3.up, Vector3.zero);
            Plane plane = new Plane(Vector3.up, new Vector3 (0, transform.position.y, 0));


            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if(plane.Raycast(ray, out entry))
            {
                _targetPoint = ray.GetPoint(entry);
                _targetPoint.Set(_targetPoint.x, transform.position.y, _targetPoint.z);
            }
            
            _pathVisualization.ShowTarget(_targetPoint);
            _isTarget = true;
        }

        if (_isTarget)
        {
            float dst = Vector3.Distance(transform.position, _targetPoint);
            if (dst < dstTreshold)
            {
                _rb.velocity = Vector3.zero;
                _pathVisualization.HideTarget();
                _isTarget = false;
                return;
            }
            UpdateRotation();
            _rb.position += transform.forward * speed * Time.deltaTime;
        }
        
        Debug.DrawLine(transform.position, transform.position + transform.forward * 500f, Color.red);
    }

    private void UpdateRotation()
    {
        Vector3 direction = _targetPoint - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, angularSpeed / 100 * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, dstTreshold);
        
        if (_isTarget)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(_targetPoint, 20f);
        }
    }
}
