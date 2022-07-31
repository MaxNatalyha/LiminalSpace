using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StarsDome : MonoBehaviour {

    public MeshRenderer starPrefab;
    public Vector2 radiusMinMax;
    public int count = 1000;
    const float calibrationDst = 2000;
    public Vector2 brightnessMinMax;
    public float rotateSpeed;
    public float domeScaleMltp;

    Camera cam;

    void Start () {
        cam = Camera.main;
        float starDst = cam.farClipPlane - radiusMinMax.y;
        float scale = starDst / calibrationDst;


        for (int i = 0; i < count; i++) {
            MeshRenderer star = Instantiate (starPrefab, Random.onUnitSphere * starDst / domeScaleMltp, Quaternion.identity, transform);
            float t = SmallestRandomValue (6);
            star.transform.localScale = Vector3.one * Mathf.Lerp (radiusMinMax.x, radiusMinMax.y, t) * scale;
            star.material.color = Color.Lerp (Color.black, star.material.color, Mathf.Lerp (brightnessMinMax.x, brightnessMinMax.y, t));
        }
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime / 100f, Space.Self);
    }

    float SmallestRandomValue (int iterations) {
        float r = 1;
        for (int i = 0; i < iterations; i++) {
            r = Mathf.Min (r, Random.value);
        }
        return r;
    }
    
    void LateUpdate () {
        if (cam != null) {
            transform.position = cam.transform.position;
        }
    }
    
}