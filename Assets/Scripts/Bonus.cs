using UnityEngine;
using System.Collections;

public class Bonus : MonoBehaviour {
	
	public AudioClip die;
	
	bool dead;
	Player player;
	Circle outer;
	Game game;
	PlayerStage ps;
	float speed;
	float speed2;
	float angle;
	float radiusToReach;
	float curRadius;
	int choice;
	
	// Use this for initialization
	void Start () {
		
		//Récupère le joueur
		player = GameObject.Find(gameObject.transform.parent.name + "/player").GetComponent<Player>();
		outer = GameObject.Find (gameObject.transform.parent.name + "/outer").GetComponent<Circle>();
		game = GameObject.Find("GameDirector").GetComponent<Game>();
		ps = transform.parent.GetComponent<PlayerStage>();
		
		/***
		 * Pour rendre le jeu un peu dur et pas trop facile, soit on ajoute un bonus entre le centre et le bord
		 * du joueur
		 * soit on l'ajoute entre le joueur et le bord du rond externe
		 * 
		 * Les bonus sont légèrement repoussé par le cercle du joueur pour que ça soit un peut + dur
		 * 
		 * Sinon les bonus peuvent bouger partout et vont à un moment traverser le cercle joueur, ce qui lui donne des points
		 * 
		 * **/
		
		angle = Random.Range(0,360);
		
		choice = Random.Range(0,2);
		if(choice == 0){
			curRadius = Random.Range(0.05f, player.getRadius()/2-0.05f);
		}
		else{
			curRadius = Random.Range(player.getRadius()/2+0.05f, outer.getRadius()/2);
		}
		
		generatePosition();
		
		speed = Random.Range(0.25f, 1.0f);
		speed2 = Random.Range(0.001f, 0.005f);
		
		dead = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(!game.gameover && !ps.end){
			/*** Détection quand le joueur touche ce bonus
			 * 
			 * Si radius du cercle == distance centre au point
			 * 
			 ****/
			float dist = Vector3.Distance(transform.position, new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z));
			
			//Si collision, supprime l'objet et augmente le score
			if(player.getRadius()/2 > dist-0.01f && player.getRadius()/2 < dist+0.01f && !dead){
				dead = true;
				renderer.enabled = false;
				audio.PlayOneShot(die);
				game.score += 10;
				Destroy(gameObject, die.length); //Attends que le son ait fini de jouer pour la destruction
			}
			
			
			
			//Déplacement du bonus dans le rond
			float x = Mathf.Cos(angle * Mathf.Deg2Rad) * curRadius + player.transform.position.x;
			float y = Mathf.Sin(angle * Mathf.Deg2Rad) * curRadius + player.transform.position.y;
			transform.position = new Vector3(x, y, transform.position.z);
			angle+=speed;
			
			
			//Regénère un nouveau radius à atteindre une fois que ce dernier à été atteint
			if(curRadius >= radiusToReach-0.1f && curRadius <= radiusToReach+0.1f){
				generatePosition();
			}
			//Déplacement du radius d'après le radius à atteindre
			if(curRadius < radiusToReach){
				curRadius+=speed2;
			}
			else if(curRadius > radiusToReach){
				curRadius-=speed2;
			}
			
		}
		
	}
	
	
	void generatePosition(){
		
		//Entre le cercle interne et le joueur
		if(choice == 0){
			radiusToReach = Random.Range(0.05f, player.getRadius()/2-0.01f);
		}
		//Entre le joueur et le cercle externe
		else{
			radiusToReach = Random.Range(player.getRadius()/2+0.05f, outer.getRadius()/2);
		}
		
	}
}
