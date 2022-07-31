using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public  class SolarSystemGenerator : MonoBehaviour
{
    public static SolarSystemGenerator current;
    
    [Header("Celestial Prefabs")]
    public GameObject sunPref;
    public GameObject planetPref;
    public GameObject satellitePref;
    [Space(10)]
    [Header("Spawn Settings")]
    public Vector2 celestialSizeMinMax;
    public Vector2Int celestialPositionMinMax;
    public int planetCount;
    
    [Range(0,1)] public float satellitePercent;
    
    [Space(10)]
    [Header("Visualization Settings")]
    public Gradient orbitColor;
    
    //private Transform _sun;
    private List<GameObject> _planets = new List<GameObject>();
    //private List<Vector2> _planetsOrbits = new List<Vector2>();

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        SpawnCelestial(sunPref, Vector3.zero, celestialSizeMinMax.y + celestialSizeMinMax.y);

        int lastPos = celestialPositionMinMax.x;
        
        float[] planetSise = new float[planetCount];
        
        for (int i = 0; i < planetCount; i++)
        {
            planetSise[i] = GetRandom(celestialSizeMinMax);
        }
        
        Array.Sort(planetSise);

        for (int i = 0; i < planetCount; i++)
        {
            int plantetPos = lastPos + Random.Range(celestialPositionMinMax.x, celestialPositionMinMax.y);
            lastPos = plantetPos;

            //Vector2 pos = Vector2.one * plantetPos;
            //_planetsOrbits.Add(pos);

            SpawnCelestial(planetPref, GetPointOnCircle(Random.Range(0f, 360f), plantetPos), planetSise[i]);
        }
        
        int satelliteCount = Mathf.CeilToInt(planetCount * satellitePercent);
        
        for (int i = 0; i < satelliteCount; i++)
        {
            SpawnSatellite(_planets[Random.Range(0, _planets.Count)]);
        }
    }

    private void SpawnCelestial(GameObject celestialBody, Vector3 position, float size)
    {
        GameObject celestial = Instantiate(celestialBody, position, Quaternion.identity, transform);

        celestial.transform.localScale = Vector3.one * size;

        if (celestial.GetComponent<Planet>())
        {
            _planets.Add(celestial);            
            celestial.GetComponent<Planet>().Init(GetRandom(new Vector2(100f, 175f)), transform);
        }
    }

    private void SpawnSatellite(GameObject celestial)
    {
        Vector3 pos = celestial.transform.position + celestial.transform.localScale;
        pos.y = 0;
        GameObject satellite = Instantiate(satellitePref, pos, Quaternion.identity, celestial.transform);
        satellite.transform.localScale = Vector3.one / 3f;
        satellite.GetComponent<Satellite>().Init(GetRandom(new Vector2(3000f, 5000f)), celestial.transform);
        _planets.Remove(celestial);
    }

    private float GetRandom(Vector2 range)
    {
        return Random.Range(range.x, range.y);
    }
    
    private Vector3 GetPointOnCircle(float angle, int radius)
    {
        Vector3 position;
        position.x = radius * Mathf.Cos(angle);
        position.y = 0;
        position.z = radius * Mathf.Sin(angle);
        return position;
    }

    /*
    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            for (int i = 0; i < planetCount; i++)
            {
                Vector3 pos = new Vector3(_planetsOrbits[i].x, 0, 0);
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(pos, 50f);
            }
        }
    }
    */
}
