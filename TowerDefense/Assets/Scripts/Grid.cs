using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {


	static public Vector3[,] gridTable;
	static public int tamGrid;
	public int gridSize;
	// Use this for initialization
	void Start (){

		tamGrid = gridSize;
		createGrid();
		gridTable = new Vector3[tamGrid,tamGrid];
		initGridTable();

	}
	

	// Update is called once per frame
	void Update (){}

	//Create the grid with size tamGrid (columns x files)
	void createGrid(){

		bool changeColor = false;
		for (float column = 0f; column < (float)tamGrid; column++ ){
			if (tamGrid%2 == 0){
			changeColor = !changeColor;
			}
			for (float file = 0f; file > (float)-tamGrid; file--){
				GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
				cube.transform.position = new Vector3 (column,1f,file);
				cube.name = "cube"+column+Mathf.Abs(file);
				if ( changeColor == false){
					cube.GetComponent<Renderer>().materials[0].color = Color.white;
					changeColor = true;
				}else{
					cube.GetComponent<Renderer>().materials[0].color = Color.gray;
					changeColor = false;
				}
			}
		}
	}
	//Initialize the gridTable with pos X pos Y and state: 0 = empty
	void initGridTable(){

		for (int column = 0; column < tamGrid; column++ ){
			for (int file = 0; file > -tamGrid; file--){
				int fil = Mathf.Abs(file);
				gridTable[column,fil] = new Vector3((float)column,(float)fil,0f);
				//Debug.Log (gridTable[column,fil]);
			}
		}
	}



}
