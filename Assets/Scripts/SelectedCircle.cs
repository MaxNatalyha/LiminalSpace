using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCircle : MonoBehaviour
{
    private Transform CameraRig;
    public Transform mesh;

    private void Start()
    {
        mesh.gameObject.SetActive(false);
        CameraRig = FindObjectOfType<CameraController>().GetComponent<Transform>();
        transform.LookAt(transform.position + CameraRig.transform.rotation * Vector3.forward, CameraRig.transform.rotation * Vector3.up);   
    }

    private void OnMouseEnter()
    {
        mesh.gameObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        mesh.gameObject.SetActive(false);
    }
}
