using UnityEngine;
using System.Collections;

public class Circle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public float getRadius()
	{
		return transform.localScale.x;
	}
	
	public void setRadius(float value)
	{
		transform.localScale = new Vector3(value,value,1);
	}
}
