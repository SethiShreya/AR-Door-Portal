using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TaptoPlace : MonoBehaviour
{
    public GameObject objectToSpawn;
    private GameObject spawnedObject;
    private ARRaycastManager aRRayCastManager;
    private Vector3 arpose;
    private List<ARRaycastHit> hits= new List<ARRaycastHit>();
    
    void Start()
    {
        aRRayCastManager = GetComponent<ARRaycastManager>();
        if (aRRayCastManager == null)
        {
            Debug.Log("Ray Cast Manager could not be found");
        }
    }

    private bool tryGetTouchPose(out Vector2 arpose)
    {
        if (Input.touchCount > 0)
        {
            arpose = Input.GetTouch(0).position;
            return true;
        }
        arpose = default;
        return false;
    }

    
    void Update()
    {
        if (!tryGetTouchPose(out Vector2 touchposition))
        {
            return;
        }

        if (aRRayCastManager.Raycast(arpose, hits, TrackableType.PlaneWithinBounds)){
            var hitpose = hits[0].pose;
            if (spawnedObject == null)
            {
                spawnedObject = Instantiate(objectToSpawn, hitpose.position, hitpose.rotation);
            }
            else
            {
                spawnedObject.transform.position=(hitpose.position);
            }
        }
    }
}
