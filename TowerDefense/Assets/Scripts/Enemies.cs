using UnityEngine;
using System.Collections;


public class Enemies : MonoBehaviour {
	
		
	private Vector3 enemySize = new Vector3(0.7f,0.25f,0.7f);
	private Vector3 lifeBarSize = new Vector3(1.0f,0.1f,0.25f);
	private bool spawnEnemy = true;

	static public Vector3 enemyPos = Vector3.zero;


	// Use this for initialization
	void Start () {}



	// Update is called once per frame
	void Update () {

		if (Defenses.currentTowers == 2 && Crystals.crystalPos.Count != 0){
			//Init all parameters
			if(spawnEnemy == true){
				spawnEnemy = false;
				EnemyPath.initPathMap();
				EnemyPath.assignCrystal();
				enemySpawn();
				EnemyPath.optimalPath_BFS();
				EnemyPath.remakePath();
				//Move the enemy
				StartCoroutine("moveEnemy");

			}
		}

		if (Crystals.crystalPos.Count == 0){
			Debug.Log("Juego Terminado");
			Time.timeScale = 0;
			Application.Quit();

		}

	}


	//Set an enemy on the grid
	void enemySpawn(){
		//Set the enemy in spawn position
		GameObject enemy = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		float posX = Random.Range(0,9);
		enemy.transform.position = new Vector3 (posX,2f,0f);
		enemyPos = enemy.transform.position;
		enemy.transform.localScale = enemySize;
		enemy.name = "Enemy";
		enemy.AddComponent<Rigidbody>();
		enemy.GetComponent<Rigidbody>().useGravity = false;
		enemy.GetComponent<Rigidbody>().isKinematic = true;
		enemy.AddComponent<EnemyCollision>();
		setLifeBar(enemy);
		enemy.AddComponent<EnemyStats>();
		setStats(enemy);

	}


	//Set the life bar for one enemy
	void setLifeBar(GameObject enemy){

		//Set the empty bar as child of the turret
		GameObject barEmpty = new GameObject();
		barEmpty.transform.parent = enemy.transform;
		Transform emptyBar = enemy.transform.GetChild(0);
		emptyBar.transform.parent = enemy.transform;
		emptyBar.transform.localPosition = new Vector3(enemy.transform.position.x*0-0.5f,
		                                     	  	   enemy.transform.position.y*0+1.25f,
		                                          	   enemy.transform.position.z*0+0.5f);
		emptyBar.name = "fullAuxBar";
		emptyBar.transform.localScale = lifeBarSize;

		//Set the full bar as child of the empty bar
		GameObject barFull = GameObject.CreatePrimitive(PrimitiveType.Cube);
		barFull.transform.parent = barEmpty.transform;
		Transform lifeBar = enemy.transform.GetChild(0).GetChild(0);
		barFull.transform.parent = emptyBar.transform;
		lifeBar.transform.localPosition = new Vector3(enemy.transform.position.x*0+0.5f,
		                                              enemy.transform.position.y*0+1.25f,
		                                              enemy.transform.position.z*0+0.5f);
		lifeBar.name = "LifeBar";
		lifeBar.transform.localScale = lifeBarSize;
		lifeBar.GetComponent<Renderer>().materials[0].color = Color.red;
	}

	void setStats(GameObject enemy){

		//Set hit points and level to the enemy
		EnemyStats.auxEnemyLevel = Random.Range(1,3);

		if (EnemyStats.auxEnemyLevel == 1){
			EnemyStats.auxMaxHealth = 1;
			EnemyStats.auxCurrentHealth = 1;
			enemy.GetComponent<Renderer>().materials[0].color = Color.gray;
		}
		if (EnemyStats.auxEnemyLevel == 2){
			EnemyStats.auxMaxHealth = 5;
			EnemyStats.auxCurrentHealth = 5;
			enemy.GetComponent<Renderer>().materials[0].color = Color.yellow;
		}
		if (EnemyStats.auxEnemyLevel == 3){
			EnemyStats.auxMaxHealth = 10;
			EnemyStats.auxCurrentHealth = 10;
			enemy.GetComponent<Renderer>().materials[0].color = Color.cyan;
		}
	}
	

	//Move the enemy
	IEnumerator moveEnemy(){

		GameObject enemy = GameObject.Find("Enemy");
		if (enemy != null){
			foreach (Vector3 pos in EnemyPath.pathToCrystal){
				//Check if the enemy is destroyed by the turrets
				if (enemy != null){
					//move the enemy to the curret pos
					enemy.transform.localPosition = new Vector3 (pos.x,2f,-pos.y);
					yield return new WaitForSeconds(0.5f);

				}else{
					//The enemy was destroyed
					EnemyPath.pathToCrystal.Clear();
					//Stop  move Coroutine
					StopCoroutine("moveEnemy");
					break;	
				}
			}
		}
		//Set flag to spawn next enemy
		EnemyPath.pathToCrystal.Clear();
		spawnEnemy = true;

	}

	

}
