using UnityEngine;
using System.Collections;

public class TurretProgressBar : MonoBehaviour {

	
	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {

		//Timer for progress bars
		if(Defenses.turretsList.Count > 0){
			increaseProgressBar();
		}
	}
	

	//Increase the progress bar each second
	void increaseProgressBar(){
		
		foreach (GameObject turret in Defenses.turretsList){
			//Get the bar from the current turret		
			Transform progressBar = turret.transform.GetChild(0);
			//Check if the bar is bigger than 1
			if(progressBar.transform.localScale.z > 1){
				progressBar.transform.localScale = new Vector3(progressBar.transform.localScale.x,0.1f,1f);
			}
			if(progressBar.transform.localScale.z < 1){
				//Increase the progress bar
				float amountIncreased = progressBar.transform.localScale.z+Time.deltaTime*0.1f;
				progressBar.transform.localScale = new Vector3(progressBar.transform.localScale.x,0.1f,amountIncreased);
			}


		}
		
	}


	
}
