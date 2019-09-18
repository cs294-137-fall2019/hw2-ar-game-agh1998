using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARButtonManager : MonoBehaviour
{
	private Camera arCamera;
    private PlaceGameBoard placeGameBoard;

    // Start is called before the first frame update
    void Start()
    {
    	arCamera = GetComponent<ARSessionOrigin>().camera;
        placeGameBoard = GetComponent<PlaceGameBoard>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (placeGameBoard.Placed() && Input.touchCount > 0)
        {
            Vector2 touchPosition = Input.GetTouch(0).position;
            Ray ray = arCamera.ScreenPointToRay(touchPosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit,100))
            {
                if(hit.transform.tag=="Interactable")
                    hit.transform.GetComponent<OnTouch3D>().OnTouch();
            }
        }
    }
}
