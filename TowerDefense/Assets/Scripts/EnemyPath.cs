using UnityEngine;
using System.Collections;
// To use queue
using System.Collections.Generic;



public class EnemyPath : MonoBehaviour {


	static private Vector3 [,] pathMap;
	static private Vector3 enemyCoords;
	static private List<Vector2> checkNeighbours;

	static public int crystalAssigned = -1;
	static public List<Vector3> pathToCrystal = new List<Vector3>();
	static public Vector3 currentNode = Vector3.zero;


	// Use this for initialization
	void Start () {
		//init checkNeighbours
		initCheckNeighbours();

	}


	
	// Update is called once per frame
	void Update () {
	
	}

	//Get the path to the crystal 
	static public void remakePath(){

		Vector3 auxCurrentNode = currentNode;
		//remake the path from crystal to the enemy origin position
		//while (currentNode.x != enemyCoords.x && currentNode.y != enemyCoords.z)
		while (currentNode.z != 0){
			//Add the node to the path to crystal
			pathToCrystal.Add(currentNode);
			//Find the next node with less distance
			float posX = 0;
			float posY = 0;
			foreach(Vector2 neighbour in checkNeighbours){
				posX = currentNode.x + neighbour.x;
				posY = currentNode.y + neighbour.y;
				//Check if the node is a valid position
				if ((posX > -1f && posX < (float)Grid.tamGrid) &&
				    (posY > -1f && posY < (float)Grid.tamGrid)){
					//Check if the next node have less distance and is not occupied
					if(pathMap[(int)posX,(int)posY].z < currentNode.z &&
					   pathMap[(int)posX,(int)posY].z > -1f){

						auxCurrentNode = pathMap[(int)posX,(int)posY];
					}
				}
			}
			currentNode = auxCurrentNode;
		}

		pathToCrystal.Add(currentNode);
		pathToCrystal.Reverse();
		/*
		foreach (Vector3 pos in pathToCrystal){
			Debug.Log(pos);
		}*/

	}
	
	//Find the path to the crystal assigned
	static public void optimalPath_BFS(){
		//Init queue, visited list and get enemy first position
		enemyPosition();
		Queue<Vector3> nodesToCheck = new Queue<Vector3>();
		List<Vector2> visited = new List<Vector2>();
		nodesToCheck.Enqueue(new Vector3(enemyCoords.x,Mathf.Abs(enemyCoords.z),0));
		visited.Add(new Vector2(enemyCoords.x,Mathf.Abs(enemyCoords.z)));
		Vector3 crystalNode = (Vector3)Crystals.crystalPos[crystalAssigned];
		//origin enemy spawn has distance 0
		pathMap[(int)enemyCoords.x,(int)Mathf.Abs(enemyCoords.z)].z = 0;
		//Start to find the optimal path
		while (nodesToCheck.Count > 0){ 
			//Check if the optimal path to the crystal was found
			Vector3 actualNode = nodesToCheck.Dequeue();
			if (actualNode.x == crystalNode.x &&
			    actualNode.y == crystalNode.y){
				//Get the last node to reconstruct the path
				currentNode = actualNode;
				nodesToCheck.Clear();
				visited.Clear();
				break;

			}else{

				float posX = 0;
				float posY = 0;
				//Check the neighbours around the actual node
				foreach(Vector2 neighbour in checkNeighbours){

					posX = actualNode.x+neighbour.x;
					posY = actualNode.y+neighbour.y;
					//Check if the current position is valid
					if ((posX > -1f && posX < (float)Grid.tamGrid) &&
					    (posY > -1f && posY < (float)Grid.tamGrid)){

						Vector2 auxPos = new Vector2(posX,posY);
						//Check if the node is on the visited list and empty
						if (visited.Contains(auxPos) == false &&
						    pathMap[(int)posX,(int)posY].z == -1f){
								//Update the visited list and the queue
								Vector3 auxNode = new Vector3(posX,posY,actualNode.z+1);
								visited.Add(auxPos);
								nodesToCheck.Enqueue(auxNode);
								pathMap[(int)posX,(int)posY].z = auxNode.z;
						}
					}
					//numNeighbour++;
				}
			}
		}
		//Debug.Log ("Nodo Crystal: " + currentNode);
	}

	//Set the first position of the enemy
	static public void enemyPosition(){

		enemyCoords = Enemies.enemyPos;

	}

	//Assign the crystal that the enemy have to capture
	static public void assignCrystal(){
		//Set a random crystal to find
		crystalAssigned = Random.Range(0,Crystals.currentCrystals-1);	                        
	}
	
	
	//Initialize the pathMap with pos x, pos y and state: -1 = empty, -2 = occupied
	static public void initPathMap(){
		
		pathMap = new Vector3[Grid.tamGrid,Grid.tamGrid];
		for (int column = 0; column < Grid.tamGrid; column++ ){
			for (int file = 0; file > -Grid.tamGrid; file--){
				int fil = Mathf.Abs(file);
				pathMap[column,fil] = Grid.gridTable[column,fil];
				if (pathMap[column,fil].z == 2 || pathMap[column,fil].z == 3 ){
					pathMap[column,fil].z = -2;
				}else{
					pathMap[column,fil].z = -1;
				}
				//Debug.Log (pathMap[column,fil]);
			}
		}

	}
	


	//Init the list of neighbours position to check
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
