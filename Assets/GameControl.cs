using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameControl : MonoBehaviour
{
	public GameObject circleObject;
	public GameObject crossObject;
    public GameObject button;
    public Text resultText;
	
	private const int kEmpty = 0;
	private const int kCircle = 1;
	private const int kCross = 2;

	private int[,] gameState;
    private List<GameObject> placedObjects;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++){
                GameObject newButton = Instantiate(button, this.gameObject.transform);
                newButton.GetComponent<ARButton1>().SetPos(i, j);
                newButton.transform.Translate(new Vector3(i * 0.21f, 0, j * 0.21f));
                newButton.SetActive(true);
            }
        }
        gameState = new int[3,3];
        placedObjects = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetGame()
    {
    	gameState = new int[3,3];
        foreach(GameObject placedObject in placedObjects) {
            Destroy(placedObject);
        }
        placedObjects = new List<GameObject>();
    }

    public void ButtonClicked(int x, int y)
    {
    	if(gameState[x,y] == kEmpty)
    	{
            gameState[x,y] = kCircle;
    		PlaceObject(x, y, kCircle);
            if(!CheckGameEnd()) {
                int opponentMove = GetOponentMove();
                gameState[opponentMove / 3, opponentMove % 3] = kCross;
                PlaceObject(opponentMove / 3, opponentMove % 3, kCross);
                CheckGameEnd();
            }
    	}
    }

    private bool CheckGameEnd() {
        int gameResult = GameResult();
        if (gameResult != 0) {
            if (gameResult == kCircle){
                resultText.text = "Player Wins!";
            } else if (gameResult == kCross) {
                resultText.text = "CPU Wins.";
            } else {
                resultText.text = "Draw";
            }
            resultText.gameObject.SetActive(true);   
        }
        return gameResult != 0;
    }

    private void PlaceObject(int x, int y, int objectType)
    {
        GameObject newObject;
        if(objectType == kCircle) {
            newObject = Instantiate(circleObject, this.gameObject.transform);
            newObject.transform.Translate(new Vector3(x * 0.21f, 0, y * 0.21f));
            newObject.SetActive(true);
            placedObjects.Add(newObject);
        }else if (objectType == kCross) {
            newObject = Instantiate(crossObject, this.gameObject.transform);
            newObject.transform.Translate(new Vector3(x * 0.21f, 0, y * 0.21f));
            newObject.SetActive(true);
            placedObjects.Add(newObject);
        }
        
    }

    private int GetOponentMove(){
        System.Random random = new System.Random();
        int randomNumber = random.Next(0, 9);
        while(gameState[randomNumber / 3, randomNumber % 3] != kEmpty){
            randomNumber = random.Next(0, 9);
        }
        return randomNumber;
    }

    private int GameResult()
    {	
        int num_cross;
        int num_circle;
    	for(int i = 0; i < 3; i++) 
    	{
    		num_cross = 0;
    		num_circle = 0;
    		for(int j = 0; j < 3; j++)
    		{
    			if (gameState[i,j] == kCircle)
    			{
    				num_circle++;
    			}	
    			else if (gameState[i,j] == kCross)
    			{
    				num_cross++;
    			}
    		}
    		if (num_cross == 3)
    		{
    			return kCross;
    		}
    		else if (num_circle == 3) 
    		{
    			return kCircle;
    		}
    	}

    	for(int i = 0; i < 3; i++) 
    	{
    		num_cross = 0;
            num_circle = 0;
    		for(int j = 0; j < 3; j++)
    		{
    			if (gameState[j,i] == kCircle)
    			{
    				num_circle++;
    			}	
    			else if (gameState[j,i] == kCross)
    			{
    				num_cross++;
    			}
    		}
    		if (num_cross == 3)
    		{
    			return kCross;
    		}
    		else if (num_circle == 3) 
    		{
    			return kCircle;
    		}
    	}
    	
    	num_cross = 0;
        num_circle = 0;
    	for(int i = 0; i < 3; i++) 
    	{
    		
    		if(gameState[i,i] == kCircle)
    		{
    			num_circle++;
    		} else if (gameState[i,i] == kCross){
    			num_cross++;
    		}
    	}
    	if (num_cross == 3)
		{
			return kCross;
		}
		else if (num_circle == 3) 
		{
			return kCircle;
		}

		num_cross = 0;
        num_circle = 0;
    	for(int i = 0; i < 3; i++) 
    	{
    		if(gameState[i,2-i] == kCircle)
    		{
    			num_circle++;
    		} else if (gameState[i,2-i] == kCross){
    			num_cross++;
    		}
    	}
    	if (num_cross == 3)
		{
			return kCross;
		}
		else if (num_circle == 3) 
		{
			return kCircle;
		}

		// game is not over if the board is not filled
		for (int i = 0; i < 3; i++){
			for (int j = 0; j < 3; j++){
				if (gameState[i,j] == kEmpty){
					return 0;
				}
			}
		}

		// draw
    	return 3;
    }
}
