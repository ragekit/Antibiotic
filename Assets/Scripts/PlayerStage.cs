using UnityEngine;
using System.Collections;

public class PlayerStage : MonoBehaviour {
	
	public int playerNb;
	float t = 0;
	float t2 = 1.3f;

	internal Circle center;
	internal Circle outer;
	internal Player player;

	float centerBaseScale;
	float outerBaseScale;
	
	// Use this for initialization
	void Start () {

		center = transform.Find("center").GetComponent<Circle>();
		outer = transform.Find("outer").GetComponent<Circle>();
		player = transform.Find("player").GetComponent<Player>();
		
		player.parent = this;	
		player.playerNb = playerNb;
		
		centerBaseScale = center.transform.localScale.x;
		outerBaseScale = outer.transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {

		t += Time.deltaTime/1f;
		t2 += Time.deltaTime/.8f;
		float mod = Mathf.Sin(t)/3f-.23F;		
		float mod2 = Mathf.Sin(t2)/5f;
			
		float rad = centerBaseScale + mod;
		center.setRadius(rad);
		
		rad = outerBaseScale + mod2;
		outer.setRadius(rad);
		
	}
}
