using UnityEngine;
using System.Collections;

public class LevelUpTurret : MonoBehaviour {

	
	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}


	static public RaycastHit turretPosition(){

		//Cast the ray and get the first game object hit
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		Physics.Raycast(ray, out hit);
		return hit;

	}


}
