using UnityEngine;
using System.Collections;

public class Defenses : MonoBehaviour {



	private float timer = 3f;
	private float shootDelay = 0f;
	private int maxLasers = 1;
	private Vector3 laserSize = new Vector3(0.6f,0.4f,-1.4f);
	private int maxCanons = 1;
	private Vector3 canonSize = new Vector3(0.6f,0.4f,0.6f);
	private Vector3 progressBarFull = new Vector3(0.25f,0.1f,1.0f);

	static public Vector3 progressBarEmpty = new Vector3(0.25f,0.1f,0.0f);
	static public bool turretsOn = false;
	static public int currentTowers = 0;
	static public ArrayList turretsList = new ArrayList();
	
	// Use this for initialization
	void Start () {}

	

	// Update is called once per frame
	void Update () {

		//Click left mouse button to set Laser Turrets
		if(Input.GetMouseButtonDown(0)){
			setLaser();
			if (maxLasers != 1){
				turretsOn = true;
			}
		}

		//Click left mouse button to set Canon Turrets
		if(Input.GetMouseButtonDown(1)){
			setCanon();
			if (maxCanons != 1){
				turretsOn = true;
			}
		}

	}

	void FixedUpdate(){
		//Check if the delay is done to shoot again
		if(timer < shootDelay){
			ShootBullets.shoot();
			shootDelay = 0;
		}else if(turretsOn == true){
			shootDelay+=Time.deltaTime;
			//Debug.Log(shootDelay);
		}
	}




	//Set the lasers on the grid
	void setLaser(){
		//Check if we have more lasers to deploy on the grid
		if (maxLasers > 0){
			// Casts the ray and get the first game object hit
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			//Get the int (x,y) position to deploy the laser turret
			RaycastHit hit;
			Physics.Raycast(ray, out hit);
			/*Debug.Log("This hit at " + hit.point );*/
			float posX = Mathf.Abs(hit.point.x);
			int posXint = Mathf.RoundToInt(posX);
			float posY = Mathf.Abs(hit.point.z);
			int posYint = Mathf.RoundToInt(posY);
			//check if a valid position (inside the grid) and empty
			if ((posXint >= 0 && posXint <Grid.tamGrid) &&(posYint >= 2 && posYint <Grid.tamGrid) &&
			    (posXint >= 0 && posXint <Grid.tamGrid) &&(posYint >= 2 && posYint+1 <Grid.tamGrid) &&
			    Grid.gridTable[posXint,posYint].z == 0f &&
			    Grid.gridTable[posXint,posYint+1].z == 0f){
				//Instance the laser turret 
				GameObject laser = GameObject.CreatePrimitive(PrimitiveType.Capsule);
				laser.transform.position = new Vector3 (posXint,2f,-posYint-0.5f);
				laser.transform.localScale = laserSize;
				laser.AddComponent<TurretStats>();
				laser.name = "Laser";
				laser.GetComponent<Renderer>().materials[0].color = Color.red;
				maxLasers--;
				currentTowers+=1;
				setProgressBar(laser);
				//Add the laser to the shoo list
				turretsList.Add(laser);
				//Update the gridTable with the occupied positions by the laser
				Grid.gridTable[posXint,posYint].z = 3f;
				Grid.gridTable[posXint,posYint+1].z = 3f;
				//Debug.Log("Laser Turrets remaining: "+ maxLasers);
					//Debug.Log(Grid.gridTable[posXint,posYint]);
					//Debug.Log(Grid.gridTable[posXint,posYint+1]);
					
			}
		}
	}

	//Set the canons on the grid
	void setCanon(){
		// check if we have more canons to deploy on the grid
		if (maxCanons > 0){
			//Cast the ray and get the first game object hit
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			//Get the int (x,y) position to deploy the canon turret
			RaycastHit hit;
			Physics.Raycast(ray, out hit);
			/*Debug.Log("This hit at " + hit.point );*/
			float posX = Mathf.Abs(hit.point.x);
			int posXint = Mathf.RoundToInt(posX);
			float posY = Mathf.Abs(hit.point.z);
			int posYint = Mathf.RoundToInt(posY);
			//check if a valid position (inside the grid) and is empty
			if ((posXint >= 0 && posXint <Grid.tamGrid) &&
			    (posYint >= 2 && posYint <Grid.tamGrid) &&
			    Grid.gridTable[posXint,posYint].z == 0f){
				//set the canon turret 
				GameObject canon = GameObject.CreatePrimitive(PrimitiveType.Capsule);
				canon.transform.position = new Vector3 (posXint,2f,-posYint);
				canon.transform.localScale = canonSize;
				canon.AddComponent<TurretStats>();
				canon.name = "Canon";
				canon.GetComponent<Renderer>().materials[0].color = Color.blue;
				maxCanons--;
				currentTowers+=1;
				setProgressBar(canon);
				//Add the laser to the shoot list
				turretsList.Add(canon);
				//update the gridTable with the occupied positions by the Canon
				Grid.gridTable[posXint,posYint].z = 2f;
				//Debug.Log("Canon Turrets remaining: "+ maxCanons);
					//Debug.Log(Grid.gridTable[posXint,posYint]);

			}
		}
	}

	//init the progress bar for turrets
	 void setProgressBar(GameObject turret){

		if (turret.name == "Canon"){
			//Set the empty bar as child of the  Canon turret
			GameObject barEmpty = new GameObject();
			barEmpty.AddComponent<TurretProgressBar>();
			barEmpty.transform.parent = turret.transform;
			Transform emptyBar = turret.transform.GetChild(0);
			emptyBar.transform.localPosition = new Vector3(turret.transform.position.x*0f-0.6f,
			                                     	  turret.transform.position.y*0f,
			                                     	  turret.transform.position.z*0f-0.5f);
			emptyBar.transform.localScale = new Vector3(0.25f,0.1f,0.0f);
			emptyBar.name = "auxEmptyBar";

			//Set the full bar
			GameObject barFull = GameObject.CreatePrimitive(PrimitiveType.Cube);
			barFull.transform.parent = emptyBar.transform;
			Transform progressBar = turret.transform.GetChild(0).GetChild(0);
			progressBar.name = "ProgressBar";
			progressBar.transform.localScale = progressBarFull;
			progressBar.GetComponent<Renderer>().materials[0].color = Color.green;
			progressBar.transform.localPosition = new Vector3(turret.transform.position.x*0f,
			                                          		  turret.transform.position.y*0f,
			                                                  turret.transform.position.z*0f+0.5f);
		}else{
			//Set the empty bar as child of the  Laser turret
			GameObject barEmpty = new GameObject();
			barEmpty.AddComponent<TurretProgressBar>();
			barEmpty.transform.parent = turret.transform;
			Transform emptyBar = turret.transform.GetChild(0);
			emptyBar.transform.localPosition = new Vector3(turret.transform.position.x*0f-0.6f,
			                                               turret.transform.position.y*0f,
			                                               turret.transform.position.z*0f+0.7f);
			emptyBar.transform.localScale = new Vector3(0.25f,0.1f,0.0f);
			emptyBar.name = "auxEmptyBar";
			
			//Set the full bar
			GameObject barFull = GameObject.CreatePrimitive(PrimitiveType.Cube);
			barFull.transform.parent = emptyBar.transform;
			Transform progressBar = turret.transform.GetChild(0).GetChild(0);
			progressBar.name = "ProgressBar";
			progressBar.transform.localScale = progressBarFull;
			progressBar.GetComponent<Renderer>().materials[0].color = Color.green;
			progressBar.transform.localPosition = new Vector3(turret.transform.position.x*0f,
			                                                  turret.transform.position.y*0f,
			                                                  turret.transform.position.z*0f-0.7f);


		}
	}






}
