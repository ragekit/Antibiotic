using UnityEngine;
using System.Collections;


[RequireComponent(typeof(AudioSource))]
public class Game : MonoBehaviour {
	
	public int maxLife;
	public int lifeMinWhenTouched;
	public int score;
	
	[HideInInspector]
	public bool gameover;
	
	float curTime;
	
	void Start () {
		//ArduinoHandler.Instance.Start();
		
		gameover = false;
		maxLife = 100;
		lifeMinWhenTouched = 10;
		score = 0;
		
		curTime = Time.time;
	}
	
	void Update () {

		if(Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
		
		//Attends 3 sec. après le gameover pour passer à l'écran suivant
		if(Time.time >= curTime + 3 && gameover){
			//Sauve les deux scores en interne
			PlayerPrefs.SetInt("score", score);
			Application.LoadLevel("SetScoreScene");
		}
		
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
	
	
	void saveScore(){
	}
	
	
	public void decreaseLife(){
		//Met compteur pour pas que ça decrease trop vite...bloque pendant 0.5sec
		if(Time.time >= curTime + 0.5){
			maxLife -= lifeMinWhenTouched;
			//Gameover quand vie est à 0
			if(maxLife <= 0){
				gameover = true;
				curTime = Time.time;
			}
			curTime = Time.time;
		}
	}
	
		
	public float map(float s, float a1, float a2, float b1, float b2){
	    return b1 + (s-a1)*(b2-b1)/(a2-a1);
	}
	
}
