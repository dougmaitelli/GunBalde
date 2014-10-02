using UnityEngine;
using System.Collections;

public class Interface : MonoBehaviour {
	
	public Texture2D progressBarEmpty;
	public Texture2D progressBarFull;

	// Use this for initialization
	void Start () {
	
	}
	
	void OnGUI () {
		GameObject playerO1 = GameObject.FindWithTag("Player1");
		
		if (playerO1) {
			Player_Script player1 = (Player_Script) playerO1.GetComponent(typeof(Player_Script));
			
			GUI.DrawTexture(new Rect(10, Screen.height - 35, 100, 10), progressBarEmpty);
	    	GUI.DrawTexture(new Rect(10, Screen.height - 35, 100 * Mathf.Clamp01(player1.life), 10), progressBarFull);
			
			GUI.DrawTexture(new Rect(10, Screen.height - 20, 100, 10), progressBarEmpty);
	    	GUI.DrawTexture(new Rect(10, Screen.height - 20, 100 * Mathf.Clamp01(player1.getExtraForce() / 100), 10), progressBarFull);
		}
			
		GameObject playerO2 = GameObject.FindWithTag("Player2");
		
		if (playerO2) {
			Player_Script player2 = (Player_Script) playerO2.GetComponent(typeof(Player_Script));
			
			GUI.DrawTexture(new Rect(Screen.width - 110, Screen.height - 35, 100, 10), progressBarEmpty);
	    	GUI.DrawTexture(new Rect(Screen.width - 110, Screen.height - 35, 100 * Mathf.Clamp01(player2.life), 10), progressBarFull);
			
			GUI.DrawTexture(new Rect(Screen.width - 110, Screen.height - 20, 100, 10), progressBarEmpty);
	    	GUI.DrawTexture(new Rect(Screen.width - 110, Screen.height - 20, 100 * Mathf.Clamp01(player2.getExtraForce() / 100), 10), progressBarFull);
		}
	}
}
