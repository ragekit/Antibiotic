using UnityEngine;
using System.Collections;

public class Player : Circle {
	
	float maxScale;
	float minScale;
	internal PlayerStage parent;
	internal int playerNb;
	
	
	// Use this for initialization
	void Start () {
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
		setRadius(ArduinoHandler.Instance.potValues[playerNb-1]*2);
	}
}
