using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Crystals : MonoBehaviour {

	private int maxCrystals = 3;
	private Vector3 crystalSize = new Vector3(0.8f,0.1f,0.8f);
	public List<Vector2> checkNeighbours;

	static public ArrayList crystalPos = new ArrayList();
	static public int currentCrystals = 0;
	// Use this for initialization
	void Start () {
		//init checkNeighbours
		initCheckNeighbours();
		//Set the crystal on the grid	
		setCrystals();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void setCrystals(){

		int column = 0;
		int file = 0;
		int numCrystals = 0;
		bool noNeighbours = true;
		bool neighbourFound = false;
		//Check if we set all the crystals on the grid
		while( numCrystals < maxCrystals){
			noNeighbours = true;
			neighbourFound = false;
			//check if the positions around the crystal are empty
			while (noNeighbours == true){
				//Get a random position of the grid
				column = Random.Range(0,Grid.tamGrid);
				file = Random.Range(2,Grid.tamGrid);
				/*
				//Check each position
				for(int n=0; n < 8; n++){
					//check this lines below!!
					float posX = column + checkNeighbours[n].x;  
					float posY = file + checkNeighbours[n].y;
					if ((posX > -1f && posX < (float)Grid.tamGrid) && (posY > -1f && posY < (float)Grid.tamGrid)){
						if(Grid.gridTable[(int)posX,(int)posY].z == 0){
							noNeighbours = false;
						}else{
							noNeighbours = false;
							neighbourFound = true;
						}
					}
				}*/
				//Check each position
				foreach (Vector2 posAround in checkNeighbours){
					float posX = column + posAround.x;  
					float posY = file + posAround.y;
					if ((posX > -1f && posX < (float)Grid.tamGrid) &&
					    (posY > -1f && posY < (float)Grid.tamGrid)){
						if(Grid.gridTable[(int)posX,(int)posY].z == 0){
							noNeighbours = false;
						}else{
							noNeighbours = false;
							neighbourFound = true;
						}
					}
				}
			}
			//Check if the random position is empty
			if (Grid.gridTable[column,file].z == 0f && neighbourFound == false){
				//Set the crystal in the random position
				GameObject crystal = GameObject.CreatePrimitive(PrimitiveType.Cube);
				crystal.transform.position = new Vector3 (column,1.75f,-file);
				crystal.transform.localScale = crystalSize;
				crystal.AddComponent<Rigidbody>();
				crystal.GetComponent<Rigidbody>().useGravity = false;
				crystal.name = "Crystal";
				crystal.GetComponent<Renderer>().materials[0].color = Color.green;
				//update the gridTable
				Grid.gridTable[column,file].z = 1f;
				crystalPos.Add(new Vector3 (column,file,0f));
				currentCrystals+=1;
				numCrystals+=1;
				//Debug.Log(Grid.gridTable[columns,files]);
			}
		}

	}


	void initCheckNeighbours(){
		
		checkNeighbours = new List<Vector2>();
		checkNeighbours.Add(new Vector2(-1f,-1f));
		checkNeighbours.Add(new Vector2(0f,-1f));
		checkNeighbours.Add(new Vector2(+1f,-1f));
		checkNeighbours.Add(new Vector2(-1f,0f));
		checkNeighbours.Add(new Vector2(+1f,0f));
		checkNeighbours.Add(new Vector2(-1f,+1f));
		checkNeighbours.Add(new Vector2(0f,+1f));
		checkNeighbours.Add(new Vector2(+1f,+1f));

	}



}
