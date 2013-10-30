using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	//MAIN GAME CLASS WITH GAMELOOP, game behavior
	Player p1;
	Player p2;
	bool isGameOver;
	
	void Start () {
		ArduinoHandler.Instance.Start();
		isGameOver = false;
	}
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)) Application.Quit();

		//update arduino values
		ArduinoHandler.Instance.Update();
		
		if(p1.isTouchingBorder() || p2.isTouchingBorder())
		{
			OnGameOver();
		}
	}
	
	void OnApplicationQuit()
	{
		ArduinoHandler.Instance.OnApplicationQuit();
	}
	
	void OnGameOver()
	{
		if(isGameOver == false)
		{
			ArduinoHandler.Instance.Splash();
			isGameOver = true;
		}
	}
}
