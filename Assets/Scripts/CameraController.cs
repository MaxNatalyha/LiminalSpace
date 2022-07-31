using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Controller Settings")]
    public Transform cameraTransform;
    
    [Space(10)]
    [Range(0, 100)] 
    public int zoomAmount = 10;
    
    [Space(10)]
    [Range(0, 10)] 
    public float movementTime = 5f;
    
    [Space(10)]
    [Range(0, 10)]
    public float zoomMin = 5f;
    [Range(50, 1000)]
    public float zoomMax = 300f;
    
    [Space(10)]
    public bool isMouseEnbale = false;

    private Vector3 newPosition;
    
    private Vector3 dragStartPosition;
    private Vector3 dragCurrentPosition;
    
    private Vector3 newZoom;

    private Vector3 rotateStartPosition;
    private Vector3 rotateCurrentPosition;

    private Quaternion newRotation;
    
    private bool isActive = false;

    private Vector3 startCameraPosition;
    private Vector3 startCameraLocalPosiotion;
    private Quaternion startCameraRotation;
    
    void Start()
    {
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
        
        startCameraPosition = transform.position;
        startCameraLocalPosiotion = cameraTransform.localPosition;
        startCameraRotation = transform.rotation;
    }
    
    void Update()
    {
        if(isMouseEnbale)
            HandleMouseInput();
            
        UpdateTransform();
    }

    public void ResetTransformToStart()
    {
        newPosition = startCameraPosition;
        newZoom = startCameraLocalPosiotion;
        newRotation = startCameraRotation;
    }
    

    public void HandleMouseInput()
    {
        if(Input.mouseScrollDelta.y != 0)
        {
            Vector3 tempZoom;
            newZoom += Input.mouseScrollDelta.y * new Vector3(0, zoomAmount, -zoomAmount);
            
            tempZoom.x = 0;
            tempZoom.y = Mathf.Clamp(newZoom.y, zoomMin, zoomMax);
            tempZoom.z = -tempZoom.y;

            newZoom = tempZoom;
        }
        
        if(Input.GetMouseButtonDown(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if(plane.Raycast(ray, out entry))
            {
                dragStartPosition = ray.GetPoint(entry);
            }
        }

        if(Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                dragCurrentPosition = ray.GetPoint(entry);

                newPosition = transform.position + dragStartPosition - dragCurrentPosition;
            }
        }
        
        //Rotation
        /*
        if(Input.GetMouseButtonDown(2))
        {
            rotateStartPosition = Input.mousePosition;
        }
        
        if(Input.GetMouseButton(2))
        {
            rotateCurrentPosition = Input.mousePosition;

            Vector3 difference = rotateStartPosition - rotateCurrentPosition;

            rotateStartPosition = rotateCurrentPosition;

            newRotation *= Quaternion.Euler(Vector3.up * (-difference.x / 5f));
        }
        */
    }

    private void UpdateTransform()
    {
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
        
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
    }
    
    public void RotateLeft()
    {
        newRotation *= Quaternion.AngleAxis(-90f, Vector3.up);
    }

    public void RotateRigth()
    {
        newRotation *= Quaternion.AngleAxis(90f, Vector3.up);
    }
    
    public void EnableController()
    {
        if (isActive)
        {
            isActive = false;
        }
        else
        {
            isActive = true;
        }
    }
}
