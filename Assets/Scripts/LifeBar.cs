using UnityEngine;
using System.Collections;

public class LifeBar : MonoBehaviour {
	
	Game game;
	
	// Use this for initialization
	void Start () {
		Color c = new Color();
		c.r = 100.0f;
		c.g = 0.0f;
		c.b = 0.0f;
		renderer.material.color = c;
		
		game = GameObject.Find("GameDirector").GetComponent<Game>();
	}
	
	// Update is called once per frame
	void Update () {
		
		//Map taille de la barre de vie d'après la vie qu'il reste
		float width = game.map(game.maxLife, 100.0f, 0.0f, 1.6f, 0.0f);
		transform.localScale = new Vector3(width, transform.localScale.y, transform.localScale.z);
	}
}
