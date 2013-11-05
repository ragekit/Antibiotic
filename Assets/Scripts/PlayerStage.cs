using UnityEngine;
using System.Collections;

public class PlayerStage : MonoBehaviour {
	
	public int playerNb;
	public int nbrStartBonus;
	public Transform bonus;
	public int maxBonus;
	public Font font;
	
	float t = 0;
	float t2 = 1.3f;
	
	internal Circle center;
	internal Circle outer;
	internal Player player;
	internal Game gameMain;
	
	GUIStyle style;
	GUIStyle style2;
	float centerBaseScale;
	float outerBaseScale;
	
	float nextPopMinT = 1f;
	float nextPopMaxT = 3f;
	float nextTime;
	float curTime;
	
	AudioSource beep;
	
	// Use this for initialization
	void Start () {
		
		style = new GUIStyle();
		style.normal.textColor = Color.white;
		style.font = font;
		style.fontSize = 12;
		style2 = new GUIStyle();
		style2.normal.textColor = Color.red;
		style2.font = font;
		style2.fontSize = 23;
		
		curTime = Time.time;
		nextTime = Random.Range(nextPopMinT, nextPopMaxT);
		
		center = transform.Find("center").GetComponent<Circle>();
		outer = transform.Find("outer").GetComponent<Circle>();
		player = transform.Find("player").GetComponent<Player>();
		gameMain = GameObject.Find("GameDirector").GetComponent<Game>();
		
		centerBaseScale = center.transform.localScale.x;
		outerBaseScale = outer.transform.localScale.x;
		
		beep = gameObject.GetComponent<AudioSource>();
		
		//Crée le stage avec x bonus placé en aléatoire dans le cercle
		for(int i=0; i<nbrStartBonus; i++){
			createBonus();
		}
		
	}
	
	
	// Update is called once per frame
	void Update () {
		if(!gameMain.gameover){
			
			//Fais bouger les cercles
			t += Time.deltaTime/1f;					//Vitesse de mouvement du cercle
			t2 += Time.deltaTime/.8f;
			
			float mod = Mathf.Sin(t)/3f-.23F;	
			float rad = centerBaseScale + mod;
			center.setRadius(rad);
			
			mod = Mathf.Sin(t2)/5.0f;				//Taille max atteint par le cercle
			rad = outerBaseScale + mod;
			outer.setRadius(rad);
			
			
			//Map le pitch du son d'après la distance du joueur à l'un des 2 cercles
			float vToMap = player.getRadius()-center.getRadius();
			string nearest = "Inner";
			if(outer.getRadius()-player.getRadius() < player.getRadius()-center.getRadius()){
				vToMap = outer.getRadius()-player.getRadius();
				nearest = "Outer";
			}
			
			float mappedV = gameMain.map(vToMap, 0.0f, 0.5f, 5.0f, 2.0f);
				
			beep.pitch = mappedV;
			
			
			//Fais popper un nouveau bonus après un certain temps
			int nbr = gameObject.GetComponentsInChildren<Bonus>().Length;
			if(Time.time >= nextTime && nbr <= maxBonus){
				nextTime = Time.time + Random.Range(nextPopMinT, nextPopMaxT);
				createBonus();
			}
			
			
			//Détecte si le joueur touche un bord, si oui fait baisser la vie
			if(player.isTouchingBorder()){
				gameMain.decreaseLife();
			}
		}
		else{
			beep.volume = 0.0f;
		}
	}
	
    void OnGUI() {
		
		if(transform.name == "PlayerOneStage"){
        	GUI.Label(new Rect(10, 5, 100, 20), "Score: " + gameMain.score, style);
			if(gameMain.gameover){
				GUI.Label(new Rect(55, 146, 100, 20), "GAME OVER", style2);
			}
		}
		else{
			GUI.Label(new Rect((Screen.width/2)+10, 5, 100, 20), "Score: " + gameMain.score, style);
			if(gameMain.gameover){
				GUI.Label(new Rect(335, 146, 100, 20), "GAME OVER", style2);
			}
		}
    
	}
	
	
	void createBonus(){
		
		//Set le stage comme parent du bonus + instancie
		Transform tmp = Instantiate(bonus, new Vector3(0, 0, -9.55f), Quaternion.identity) as Transform;
		tmp.parent = gameObject.transform;
		
	}

}
