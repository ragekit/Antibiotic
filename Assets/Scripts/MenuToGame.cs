using UnityEngine;
using System.Collections;

public class MenuToGame : MonoBehaviour {
	
	Menu menu1, menu2;
	bool startSwitch;
	float time;
	float timeSwitch;
	
	// Use this for initialization
	void Start () {
		
		menu1 = GameObject.Find("Logo").GetComponent<Menu>();
		menu2 = GameObject.Find("Logo2").GetComponent<Menu>();
		
		startSwitch = false;
		
		timeSwitch = Time.time;
		time = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		
		//Si pas starté après 10sec passe au menu de scores
		if(Time.time >= timeSwitch + 10){
			Application.LoadLevel("ScoreScene");
		}
		
		
		//Attends 2 secondes une fois que les deux joueurs ont fait start avant de lancer l'écran suivant
		if(menu1.start && menu2.start && !startSwitch){
			startSwitch = true;
			time = Time.time;
		}
		else if(startSwitch && Time.time >= time + 2){
			//Destroy le singleton
			GameObject music = GameObject.Find("GameMusic");
			DestroyImmediate(music);
			Application.LoadLevel("GameScene");
		}
	}
	
}
