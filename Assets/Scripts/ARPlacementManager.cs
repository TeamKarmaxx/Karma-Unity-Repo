using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlacementManager : MonoBehaviour
{
    public ARRaycastManager raycastManager;
    public ARPlaneManager planeManager;
    public GameObject placementIndicatorPrefab;
    public GameObject officePreviewPrefab;

    private GameObject placementIndicator;
    private GameObject officePreview;
    private Pose placementPose;
    private bool placementValid = false;
    public bool officePlaced = false;

    void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    void Start()
    {
        if (placementIndicatorPrefab != null)
        {
            placementIndicator = Instantiate(placementIndicatorPrefab);
            placementIndicator.SetActive(false);
        }
    }

    void Update()
    {
        if (officePlaced) return;

        UpdatePlacementPose();
        UpdatePlacementIndicator();

        // Naya Input System touch detection
        if (placementValid && Touch.activeTouches.Count > 0)
        {
            var touch = Touch.activeTouches[0];
            if (touch.phase == UnityEngine.InputSystem.TouchPhase.Began)
            {
                PlaceOfficePreview();
            }
        }
    }

    void UpdatePlacementPose()
    {
        Vector3 screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon);

        placementValid = hits.Count > 0;
        if (placementValid)
        {
            placementPose = hits[0].pose;
        }
    }

    void UpdatePlacementIndicator()
    {
        if (placementValid && placementIndicator != null)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.position = placementPose.position;

            Vector3 camEuler = Camera.main.transform.eulerAngles;
            placementIndicator.transform.eulerAngles = new Vector3(90f, camEuler.y, 0f);
        }
        else if (placementIndicator != null)
        {
            placementIndicator.SetActive(false);
        }
    }

    void PlaceOfficePreview()
    {
        if (officePreview == null && officePreviewPrefab != null)
        {
            Vector3 spawnPos = placementPose.position;
            officePreview = Instantiate(officePreviewPrefab, spawnPos, Quaternion.identity);

            if (FindObjectOfType<TrainingUIManager>() != null)
            {
                FindObjectOfType<TrainingUIManager>().ShowPlaceOfficeButton(true);
            }
        }
    }

    public Pose GetPlacementPose() => placementPose;
}