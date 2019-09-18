using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARButton1 : MonoBehaviour, OnTouch3D
{
    public float debounceTime = 0.3f;
    public GameObject ARSessionOrigin;
    
    private int x = 0;
    private int y = 0;
    private float remainingDebounceTime;
    private GameControl gameControl;
    private bool readyForTouch;

    void Start()
    {
        remainingDebounceTime = 0;
        gameControl = GetComponentInParent<GameControl>();
        readyForTouch = false;
    }

    void Update()
    {
        if (remainingDebounceTime > 0)
            remainingDebounceTime -= Time.deltaTime;
    }

    public void OnTouch()
    {
        if (remainingDebounceTime <= 0)
        {
            if (readyForTouch)
            {
                gameControl.ButtonClicked(x, y);
            }
            else
            {
                readyForTouch = ARSessionOrigin.GetComponent<PlaceGameBoard>().Placed();
            }
            remainingDebounceTime = debounceTime;
        }
    }

    public void SetPos(int x, int y)
    {
        this.x = x;
        this.y = y;
    }    
}