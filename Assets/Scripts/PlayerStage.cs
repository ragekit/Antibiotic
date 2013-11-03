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
	
	[HideInInspector]
	public bool gameover;	//TODO: Voir si c'est juste..plutôt ici ou dans Game? Comme le gameover est propre à chaque playerstage, je l'ai mis ici
	[HideInInspector]
	public float score;
	
	internal Circle center;
	internal Circle outer;
	internal Player player;
	
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
		
		score = 0;
		gameover = false;
		
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
		
		centerBaseScale = center.transform.localScale.x;
		outerBaseScale = outer.transform.localScale.x;
		
		beep = gameObject.GetComponent<AudioSource>();
		
		//Crée le stage avec x bonus placer en aléatoire dans le cercle
		for(int i=0; i<nbrStartBonus; i++){
			createBonus();
		}
		
	}
	
	
	// Update is called once per frame
	void Update () {
		if(!gameover){
			//Fais bouger les cercles
			t += Time.deltaTime/1f;
			t2 += Time.deltaTime/.8f;
			
			float mod = Mathf.Sin(t)/3f-.23F;	
			float rad = centerBaseScale + mod;
			center.setRadius(rad);
			
			mod = Mathf.Sin(t2)/5f;
			rad = outerBaseScale + mod;
			outer.setRadius(rad);
			
			
			//Map le pitch d'après la distance du joueur à l'un des 2 cercles
			//Plus proche de quel cercle?
			float vToMap = player.getRadius()-center.getRadius();
			string nearest = "Inner";
			if(outer.getRadius()-player.getRadius() < player.getRadius()-center.getRadius()){
				vToMap = outer.getRadius()-player.getRadius();
				nearest = "Outer";
			}
			
			float mappedV = map(vToMap, 0.0f, 0.5f, 5.0f, 2.0f);
				
			beep.pitch = mappedV;
			
			
			//Fais popper un nouveau bonus
			int nbr = gameObject.GetComponentsInChildren<Bonus>().Length;
			if(Time.time >= nextTime && nbr <= maxBonus){
				nextTime = Time.time + Random.Range(nextPopMinT, nextPopMaxT);
				createBonus();
			}
			
			
			//Détecte si le joueur touche un bord
			if(player.isTouchingBorder()){
				gameover = true;
			}
		}
		else{
			beep.volume = 0.0f;
		}
	}
	
    void OnGUI() {
		
		if(transform.name == "PlayerOneStage"){
        	GUI.Label(new Rect(10, 5, 100, 20), "Score: " + score, style);
			if(gameover){
				GUI.Label(new Rect(55, 146, 100, 20), "GAME OVER", style2);
			}
		}
		else{
			GUI.Label(new Rect(465, 5, 100, 20), "Score: " + score, style);
			if(gameover){
				GUI.Label(new Rect(335, 146, 100, 20), "GAME OVER", style2);
			}
		}
    
	}
	
	
	void createBonus(){
		
		//Set le stage comme parent + instancie
		Transform tmp = Instantiate(bonus, new Vector3(0, 0, -9.55f), Quaternion.identity) as Transform;
		tmp.parent = gameObject.transform;
		
	}

	
	float map(float s, float a1, float a2, float b1, float b2){
	    return b1 + (s-a1)*(b2-b1)/(a2-a1);
	}

}
