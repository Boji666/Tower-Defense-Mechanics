using UnityEngine;
using System.Collections;

public class BulletCollision : MonoBehaviour {


	static public int currentBulletDmg = 1;
	// Use this for initialization
	void Start () {}

	void Awake (){

		currentBulletDmg = 1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Check if a bullet hit something
	void OnCollisionEnter(Collision collision) {


		if(collision.gameObject.name == "Enemy") {
			Destroy(this.gameObject);
			//Decrease the corresponding hit points
			Transform bar = collision.gameObject.transform.GetChild(0);
			EnemyStats.hitPoints(currentBulletDmg,bar);
			//Check if the enemy is dead
			if (EnemyStats.dead == true){
				//Add coins
				Coins.currentCoins+=100;
				//Destroy the enemy
				Destroy(collision.gameObject);
				EnemyStats.dead = false;

			}
		}

		if(collision.gameObject.name == "Turret") {

			Destroy(this.gameObject);
		}
	}



}
