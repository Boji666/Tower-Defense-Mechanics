using UnityEngine;
using System.Collections;

public class ShootBullets : MonoBehaviour {

	
	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}
	

	//Shoot the turrets that have a turn to shoot
	static public void shoot(){
		//Get each turret and shoot
		foreach(GameObject turret in Defenses.turretsList){
			//Instantiate and shoot the bullet
			GameObject bullet = GameObject.CreatePrimitive(PrimitiveType.Cube);
			bullet.GetComponent<Renderer>().materials[0].color = turret.GetComponent<Renderer>().materials[0].color;
			bullet.AddComponent<Rigidbody>();
			bullet.GetComponent<Rigidbody>().useGravity = false;
			bullet.name = "Bullet";
			bullet.AddComponent<BulletCollision>();
			if (turret.name == "Laser"){
				bullet.transform.localScale = new Vector3 (0.5f,0f,1f);
				bullet.transform.position = new Vector3(turret.transform.position.x,
				                                        turret.transform.position.y,
				                                        turret.transform.position.z+1.25f);
				bullet.GetComponent<Rigidbody>().AddForce(0f,0f,100f);
				Destroy(bullet,3f);
			}else if (turret.name == "Canon"){
				bullet.transform.localScale = new Vector3 (0.5f,0.0f,0.5f);
				bullet.transform.position = new Vector3(turret.transform.position.x,
				                                        turret.transform.position.y,
				                                        turret.transform.position.z+1.25f);
				bullet.GetComponent<Rigidbody>().AddForce(0f,0f,100f);
				Destroy(bullet,3f);
			}
		}
	}
}
