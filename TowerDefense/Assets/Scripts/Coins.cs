using UnityEngine;
using System.Collections;

public class Coins : MonoBehaviour {

	static public int currentCoins = 400;

	// Use this for initialization
	void Start () {}
	

	void OnGUI(){
		//Show the score on the screen
		GUI.Label(new Rect(0f,0f,50,25),"Coins: ");
		GUI.Box(new Rect(40f,0f,50,25),currentCoins.ToString());
		GUI.Label(new Rect(100f,0f,55,25),"Crystals: ");
		GUI.Box(new Rect(155f,0f,50,25),Crystals.currentCrystals.ToString());
		GUI.Label(new Rect(210f,0f,50,25),"Turrets: ");
		GUI.Box(new Rect(260f,0f,50,25),Defenses.currentTowers.ToString());
	}
	
	// Update is called once per frame
	void Update () {}
}
