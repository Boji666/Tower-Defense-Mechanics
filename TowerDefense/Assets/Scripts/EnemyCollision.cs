using UnityEngine;
using System.Collections;

public class EnemyCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}

	//Check if an enemy find a crystal
	void OnCollisionEnter(Collision collision) {

		if(collision.gameObject.name == "Crystal") {
			//Update the gridTable to set a valid position for a crystal captured
			Vector3 updatePos = (Vector3)Crystals.crystalPos[EnemyPath.crystalAssigned];
			Grid.gridTable[(int)updatePos.x,(int)updatePos.z].z = -2;
			Crystals.crystalPos.RemoveAt(EnemyPath.crystalAssigned);
			//Decrease the number of crystals on the grid
			Crystals.currentCrystals--;
			//Destroy the crystal and the enemy
			Destroy(collision.gameObject);
			Destroy(this.gameObject);

		}
	}
}
