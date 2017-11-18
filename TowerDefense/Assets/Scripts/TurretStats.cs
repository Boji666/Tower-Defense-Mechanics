using UnityEngine;
using System.Collections;

public class TurretStats : MonoBehaviour {


	public int level = 1;
	public int bulletDamage = 1;
	public float upgradeTime = 0;
	public float shootSpeed = 1;

	static private int auxBulletDmg = 1;

	private Vector3 lvUpSymbolSize = new Vector3(0.1f,0.1f,0.1f);

	// Use this for initialization
	void Start () {}



	void OnMouseDown(){
		//Maybe is better add the level as a child of the turret
		if(Defenses.turretsOn == true){
			//Get the position of the turret
			RaycastHit hit = LevelUpTurret.turretPosition();
			//Check if we click on a turret
			if (hit.transform.gameObject.name == "Laser" ||
			    hit.transform.gameObject.name == "Canon"){
				//check if the progress bar is at 100%
				Transform bar = hit.transform.GetChild(0);
				if( bar.transform.localScale.z == 1){
					//Check if we have enough money to upgrade
					if (Coins.currentCoins >= 100){
						if (level == 1){
							GameObject cube1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
							cube1.transform.localScale = lvUpSymbolSize;
							cube1.transform.position = new Vector3 (hit.transform.position.x,
							                                        hit.transform.position.y+0.5f,
							                                        hit.transform.position.z);
							cube1.GetComponent<Renderer>().materials[0].color = Color.red + Color.blue;
							level+=1;
							cube1.name = "Level"+level;
							bulletDamage+=1;
							auxBulletDmg+=1;
							bulletDmgChange();
							bar.transform.localScale = Defenses.progressBarEmpty;
							cube1.transform.parent = hit.transform;
							Coins.currentCoins-=100;
						}else if (level == 2){
							GameObject cube2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
							cube2.transform.localScale = lvUpSymbolSize;
							cube2.transform.position = new Vector3 (hit.transform.position.x,
							                                        hit.transform.position.y+0.5f,
							                                        hit.transform.position.z-0.1f);
							cube2.GetComponent<Renderer>().materials[0].color = Color.red + Color.yellow;
							level+=1;
							cube2.name = "Level"+level;
							bulletDamage+=1;
							auxBulletDmg+=1;
							bulletDmgChange();
							bar.transform.localScale = Defenses.progressBarEmpty;
							cube2.transform.parent = hit.transform;
							Coins.currentCoins-=100;
						}
					}
				}
			}
		}

	}
	
	// Update is called once per frame
	void Update () {

		if(Defenses.turretsList.Count > 0){
			upgradeProgressBar();
		}
	}

	void upgradeProgressBar(){

		if(upgradeTime > 1){
			upgradeTime = 1;
		}
		if(upgradeTime < 1){
			//Increase the progress bar
			upgradeTime += Time.deltaTime*0.1f;;
		}
	}

	static public void bulletDmgChange(){

		BulletCollision.currentBulletDmg = auxBulletDmg;
	}
}
