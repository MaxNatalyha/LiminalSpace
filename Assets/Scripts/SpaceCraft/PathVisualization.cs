using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathVisualization : MonoBehaviour
{
    public GameObject targetPref;

    private void Start()
    {
        targetPref.SetActive(false);
    }

    public void ShowTarget(Vector3 pos)
    {
        targetPref.transform.position = pos;
        targetPref.SetActive(true);
    }

    public void HideTarget()
    {
        targetPref.SetActive(false);
    }
}
