using UnityEngine;
 
public class AudioHandler : MonoBehaviour
{
	
	//Singleton
    public static AudioHandler instance;
 
    void Awake(){
		
		//Instancie la classe de musique de fond uniquement si elle n'a pas été instanciée dans des scènes avant
		if(instance != null && instance != this){
			//Détruit cet objet s'il a déjà été instancié
			Destroy(this.gameObject);
			return;
		}
		else{
			instance = this;
		}
		
		//Permet de garder cet objet en mémoire entre les scènes
		DontDestroyOnLoad(this.gameObject);
    }
	
	
	public static AudioHandler GetInstance(){
		return instance;
	}
	
	void Update(){
		
	}
	
}