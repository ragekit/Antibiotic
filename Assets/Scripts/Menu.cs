using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	
	public AudioClip startSound;	
	public Font font;
	
	GUIStyle style;
	float time;
	bool show;
	
	[HideInInspector]
	public bool start;

	// Use this for initialization
	void Start () {
		style = new GUIStyle();
		style.normal.textColor = Color.white;
		style.font = font;
		style.fontSize = 14;
		
		time = Time.time;
		
		show = true;
		start = false;
	}
	
	// Update is called once per frame
	void Update () {
		keyboardInput();
	}
	
	void OnGUI() {
		//Fait clignoter le label press start
		if(Time.time >= time + 0.5){
			time = Time.time;
			show = !show;
		}
		
		
		//L'affiche
		if(show){
			
			string str = "Press to start";
			int x = 50;
			int x2 = 340;
			if(start){
				str = "Waiting for player 2";
				x = 20;
				x2 = 310;
			}
			
			if(transform.name == "Logo")
				GUI.Label (new Rect(x, 220, 300, 100),str, style);
			else
				GUI.Label (new Rect(x2, 220, 300, 100),str, style);
		}
	}
	
	
	void keyboardInput(){
		
		//TODO: Mapper le clavier sur le bouton arduino
		
		if(Input.GetKey("d") && transform.name == "Logo"){
			//Load jeu j1
			start = true;
			
			audio.PlayOneShot(startSound);
		}
		else if(Input.GetKey("l") && transform.name == "Logo2"){
			//Load jeu j2
			start = true;
			
			audio.PlayOneShot(startSound);
		}
	}
}
