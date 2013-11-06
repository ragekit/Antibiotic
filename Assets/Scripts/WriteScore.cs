using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

public class WriteScore : MonoBehaviour {
	
	public Font font;
	
	GUIStyle style;
	
	char[] player1, player2;
	int curItP1, curItP2;

	// Use this for initialization
	void Start () {
		style = new GUIStyle();
		style.normal.textColor = Color.white;
		style.font = font;
		style.alignment = TextAnchor.UpperCenter;
		style.fontSize = 16;
		
		//Nom de deux joueurs
		player1 = new char[3] {'a', 'a', 'a'};
		player2 = new char[3] {'a', 'a', 'a'};
		
		//Quel lettre changer (1, 2 ou 3?)
		curItP1 = 0;
		curItP2 = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
		keyboardInput();
		
		//Quand les 2 joueurs ont sauvés leur nom, l'enregistre et passe à l'écran suivant
		if(curItP1 > 2 && curItP2 > 2){
			saveScoreToText();
			Application.LoadLevel("ScoreScene");
		}
		
	}
	
	
	void OnGUI(){
		
		string name1 = new string(player1);
		string name2 = new string(player2);
		
		style.normal.textColor = Color.grey;
		GUI.Label(new Rect(0, 120, Screen.width/2, 50), "Enter your name", style);
		GUI.Label(new Rect(Screen.width/2, 120, Screen.width/2, 50), "Enter your name", style);
		
		style.normal.textColor = Color.white;
		GUI.Label(new Rect(0, 150, Screen.width/2, 50), "Score " + PlayerPrefs.GetInt("score") + ": " + name1, style);
		GUI.Label(new Rect(Screen.width/2, 150, Screen.width/2, 50), "Score " + PlayerPrefs.GetInt("score") + ": " + name2, style);
		
	}
	
	void keyboardInput(){
		
		//TODO Mapper le clavier sur le bouton arduino
		
		//Choisir une lettre - joueur 1
		if(Input.GetKeyDown("a") && curItP1 < 3){
			setPastChar(player1, curItP1);
		}
		else if(Input.GetKeyDown("s") && curItP1 < 3){
			setNextChar(player1, curItP1);
		}
		//Enter joueur 1 (valide la lettre passe à la suivante ou indique qu'il a fini)
		else if(Input.GetKeyDown("d")){
			curItP1++;
		}
		//Choisir lettre - joueur 2
		else if(Input.GetKeyDown("j") && curItP2 < 3){
			setPastChar(player2, curItP2);
		}
		else if(Input.GetKeyDown("k") && curItP2 < 3){
			setNextChar(player2, curItP2);
		}
		//Enter joueur 2
		else if(Input.GetKeyDown("l")){
			curItP2++;
		}
		
	}
	
	void setNextChar(char[] p, int it){
		if(p[it] == 'z'){
			p[it] = 'a';
		}
		else{
			p[it] = (char)(((int) p[it]) + 1);
		}
	}
	
	
	void setPastChar(char[] p, int it){
		if(p[it] == 'a'){
			p[it] = 'z';
		}
		else{
			p[it] = (char)(((int) p[it]) - 1);
		}
	}
	
	
	void saveScoreToText(){
	
		ArrayList scores = new ArrayList();
		
		//Lit le fichier texte ligne après ligne et le sauve dans le arraylist
		StreamReader sr = new StreamReader("Assets/Data/Score.txt");
		string line;
		bool notSet = true;
		
		while((line = sr.ReadLine()) != null){
			if(line.Length > 1){
				//Split la ligne nom/score (le split est fait par le tab)
				string[] strs = line.Split('\t');
				int score = int.Parse(strs[1]);
				
				//Compare le score avec celui du joueur
				//S'il le score du joueur est +  grand ou = alors est sauvé avant
				//seulement s'il n'a pas été déjà inclus
				if(PlayerPrefs.GetInt("score") >= score && notSet){
					string p1 = new string(player1);
					string p2 = new string(player2);
					string ps = p1 + "//" + p2;
					scores.Add(ps + "\t" + PlayerPrefs.GetInt("score"));
					scores.Add(line);
					notSet = false;
				}
				else{
					scores.Add(line);
				}
			}
		}
		sr.Close();
		
		//Efface le contenu du fichier texte
		File.WriteAllText("Assets/Data/Score.txt", string.Empty);
		
		StringBuilder sb = new StringBuilder();
		int cpt = 0;
		//Enregistre la nouvelle ArrayList à la place
		foreach(string score in scores){
			sb.AppendLine(score);
			sb.AppendLine();
			cpt++;
			//Save 10 max.
			if(cpt == 10){
				break;
			}
		}
		
		StreamWriter sw = new StreamWriter("Assets/Data/Score.txt");
		sw.Write(sb.ToString());
		sw.Close();
		
	}
	
}
