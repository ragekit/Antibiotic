using UnityEngine;
using System.Collections;

public class Player : Circle {
	
	internal int playerNb;
	
	Game game;
	string parentName;
	Circle outer;
	Circle center;
	
	public float speed;

	Serial serial;
	
	// Use this for initialization
	void Start () {

		serial = GameObject.Find("GameDirector").GetComponent<Serial>();
		game = GameObject.Find("GameDirector").GetComponent<Game>();
		
		parentName = transform.parent.gameObject.name;
		
		outer = GameObject.Find (parentName + "/outer").GetComponent<Circle>();
		center = GameObject.Find (parentName + "/center").GetComponent<Circle>();

	}
	
	// Update is called once per frame
	void Update () {	
		//processInput();
		keyboardInput();
	}
	
	
	public bool isTouchingBorder()
	{
		if(getRadius() > outer.getRadius() || getRadius() < center.getRadius()){
			return true;
		}
		
		return false;
	}
	
	
	void processInput()
	{
		float potValue = serial.potValues[playerNb-1];
		setRadius(potValue*2);
		//setRadius(ArduinoHandler.Instance.potValues[playerNb-1]*2);
	}
	
	
	
	void keyboardInput(){
		//Player 1
		if(!game.gameover){
			if(parentName == "PlayerOneStage"){
				if(Input.GetKey("a")){
					setRadius(getRadius() + speed);
				}
				if(Input.GetKey("s")){
					setRadius(getRadius() - speed);
				}
			}
			
			//Player 2
			if(parentName == "PlayerTwoStage"){
				
				if(Input.GetKey("j")){
					setRadius(getRadius() + speed);
				}
				if(Input.GetKey("k")){
					setRadius(getRadius() - speed);
				}
			}
		}
	}
}
