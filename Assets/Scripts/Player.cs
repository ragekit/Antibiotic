using UnityEngine;
using System.Collections;

public class Player : Circle {
	
	float maxScale;
	float minScale;
	internal PlayerStage parent;
	internal int playerNb;
	

	Serial serial;
	
	// Use this for initialization
	void Start () {

		serial = GameObject.Find("GameDirector").GetComponent<Serial>();

	}
	
	// Update is called once per frame
	void Update () {	
		processInput();
	}
	
	public bool isTouchingBorder()
	{
		maxScale = parent.outer.getRadius();
		minScale = parent.center.getRadius();
		float scale = transform.localScale.x;
		
		if(scale > maxScale || scale < minScale) 
		{
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
}
