using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceGameBoard : MonoBehaviour
{
    public GameObject gameBoard;

    private ARRaycastManager raycastManager;
    private ARPlaneManager planeManager;
    private bool placed = false;
    private bool moveInitiated = false;

    // Start is called before the first frame update.
    void Start()
    {
        // GetComponent allows us to reference other parts of this game object.
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();
    }

    // Update is called once per frame.
    void Update()
    {
        if (!placed)
        {
            if (Input.touchCount > 0 && moveInitiated)
            {
                Vector2 touchPosition = Input.GetTouch(0).position;
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                if (raycastManager.Raycast(
                    touchPosition, hits, TrackableType.PlaneWithinPolygon))
                {
                    var hitPose = hits[0].pose;
                    gameBoard.SetActive(true);
                    gameBoard.transform.position = hitPose.position;
                    placed = true;
                    planeManager.SetTrackablesActive(false);

                }
            }
        }
        else
        {
            planeManager.SetTrackablesActive(false);
        }

        if (!moveInitiated) {
            moveInitiated = true;
        }
    }

    public void AllowMoveGameBoard()
    {
        placed = false;
        gameBoard.SetActive(false);
        planeManager.SetTrackablesActive(true);
        moveInitiated = false;
    }

    public bool Placed()
    {
        return placed;
    }
}