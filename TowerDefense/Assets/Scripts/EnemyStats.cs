using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour {
	
	public int enemyLevel = 0;
	public int maxHealth = 0;
	public int currentHealth = 0;
	
	static public int auxEnemyLevel = 0;
	static public int auxMaxHealth = 0;
	static public int auxCurrentHealth = 0;
	static public bool dead = false;

	// Use this for initialization
	void Start () {

		enemyLevel = auxEnemyLevel;
		maxHealth = auxMaxHealth;
		currentHealth = auxCurrentHealth;
	}


	
	// Update is called once per frame
	void Update () {

		currentHealth = EnemyStats.auxCurrentHealth;
	}

	static public void hitPoints(int damage,Transform bar){
		//Calculate hit points
		auxCurrentHealth-= damage;
		//Check is the health is less than 0
		if (auxCurrentHealth <= 0){
			auxCurrentHealth = 0;
			dead = true;
		}else
		//Rescale the life bar if current health
		if (auxCurrentHealth > 0){
			bar.transform.localScale = new Vector3(auxCurrentHealth/10f,0.1f,0.25f);

		}

	}



}
