using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlantPlacementManager : MonoBehaviour {
    
    public GameObject[] flowers;

    public ARSessionOrigin  sessionOrigin;
    public ARRaycastManager raycastManager;
    public ARPlaneManager   planeManager;

    private List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();

    private void Update() {
        if (Input.GetTouch(0).phase == TouchPhase.Began) {
            // Shoot raycast
            // Place the objects randomly 
            // Disable the planes and the plane manager
            bool collision = raycastManager.Raycast(Input.GetTouch(0).position, raycastHits, TrackableType.PlaneWithinPolygon);  // raycastHits is a list of ARRaycastHit objects, return bool
       
            if (collision) {
                GameObject _object = Instantiate(flowers[Random.Range(0, flowers.Length - 1)]); // Instantiate a random flower
                _object.transform.position = raycastHits[0].pose.position;                      // Set the position of the flower to the position of the raycast hit
            }

            // Disable the planes and the plane manager
            foreach (var plane in planeManager.trackables) { 
                plane.gameObject.SetActive(false);                                              // Disable the plane
            }

            planeManager.enabled = false;                                                       // Disable the plane manager
        }
    }

}
