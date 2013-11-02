using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	//MAIN GAME CLASS WITH GAMELOOP, game behavior
	//Player p1;
	//Player p2;
	
	void Start () {
		//ArduinoHandler.Instance.Start();

	}
	
	void Update () {

		if(Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
		
	}


	void FixedUpdate() {

		//update arduino values
		//ArduinoHandler.Instance.Update();

	}
	
	void OnApplicationQuit()
	{
		//ArduinoHandler.Instance.OnApplicationQuit();
	}
	
	void OnGameOver()
	{
		//ArduinoHandler.Instance.Splash();
	}
	
}
