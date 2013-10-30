using UnityEngine;
using System.Collections;

public class Paint : MonoBehaviour {
	
	public float red;
	public float green;
	public float blue;
	
	
	// Use this for initialization
	void Start () {
		renderer.material.shader = Shader.Find("Diffuse");
		renderer.material.color = new Color(red,green,blue);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
