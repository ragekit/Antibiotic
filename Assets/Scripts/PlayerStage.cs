using UnityEngine;
using System.Collections;

public class PlayerStage : MonoBehaviour {
	
	public int playerNb;
	public int nbrStartBonus;
	public Transform bonus;
	public int maxBonus;
	
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
	
	// Use this for initialization
	void Start () {
		
		score = 0;
		gameover = false;
		
		style = new GUIStyle();
		style.normal.textColor = Color.black;
		style2 = new GUIStyle();
		style2.normal.textColor = Color.red;
		
		curTime = Time.time;
		nextTime = Random.Range(nextPopMinT, nextPopMaxT);
		
		center = transform.Find("center").GetComponent<Circle>();
		outer = transform.Find("outer").GetComponent<Circle>();
		player = transform.Find("player").GetComponent<Player>();
		
		centerBaseScale = center.transform.localScale.x;
		outerBaseScale = outer.transform.localScale.x;
		
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
	}
	
    void OnGUI() {
		
		if(transform.name == "PlayerOneStage"){
        	GUI.Label(new Rect(10, 10, 100, 20), "Score: " + score, style);
			if(gameover){
				GUI.Label(new Rect(80, 10, 100, 20), "GAME OVER", style2);
			}
		}
		else{
			GUI.Label(new Rect(485, 10, 100, 20), "Score: " + score, style);
			if(gameover){
				GUI.Label(new Rect(400, 10, 100, 20), "GAME OVER", style2);
			}
		}
    
	}
	
	
	void createBonus(){
		
		//Met les points en aléatoire dans le cercle, entre le joueur et le cercle extérieur
		/*float angle = Random.Range(0.0f, 1.0f) * Mathf.PI * 2;
		float x = Mathf.Cos(angle) * outer.getRadius()/2 * Random.Range(0.0f, 1.0f);
		float y = Mathf.Sin(angle) * outer.getRadius()/2 * Random.Range(0.0f, 1.0f); 
		

		float offset = 1.0f;
		if(transform.name == "PlayerOneStage")
			offset *= -1;*/
		
		//Set le stage comme parent + instancie
		Transform tmp = Instantiate(bonus, new Vector3(0, 0, -9.55f), Quaternion.identity) as Transform;
		tmp.parent = gameObject.transform;
		
	}

}
