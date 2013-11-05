using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class ReadScore : MonoBehaviour {
	
	public Font font;
	
	GUIStyle styleT;
	GUIStyle style;
	float time;
	string scores;
	
	// Use this for initialization
	void Start () {
		
		//Init style police
		style = new GUIStyle();
		style.normal.textColor = Color.white;
		style.font = font;
		style.alignment = TextAnchor.UpperCenter;
		style.fontSize = 16;
		
		styleT = new GUIStyle();
		styleT.fontSize = 16;
		styleT.normal.textColor = Color.grey;
		styleT.font = font;
		styleT.alignment = TextAnchor.UpperCenter;
		
		time = Time.time;
		
		loadScoreFromTxt();
	}
	
	// Update is called once per frame
	void Update () {
		
		//Switch au menu après 10 secondes
		if(Time.time >=  time + 10){
			Application.LoadLevel("MenuScene");
		}
		
	}
	
	
	void OnGUI(){
		GUI.Label(new Rect(0,10,Screen.width/2,50), "Hall of fame", styleT);
		GUI.Label(new Rect(Screen.width/2,10,Screen.width/2,50), "Hall of fame", styleT);
		
		GUI.Label(new Rect(0, 65, Screen.width/2, 500), scores, style);
		GUI.Label(new Rect(Screen.width/2, 65, Screen.width/2, 500), scores, style);
	}
	
	
	void loadScoreFromTxt(){
		
		//Load fichier de scores
		StreamReader sr = new StreamReader("Assets/Data/Score.txt");
		scores = sr.ReadToEnd();
		sr.Close();
	}
	
}
