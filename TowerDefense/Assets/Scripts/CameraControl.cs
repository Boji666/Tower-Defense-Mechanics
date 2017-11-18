using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	private float speed = 0.5f;
	private Vector3 moveDirection = Vector3.zero;
	private Vector3 rotation = Vector3.zero;
	private float rotateSpeed = 100f;
	private bool rotate = false;
	private Vector3 originPosition = Vector3.zero;
	private Quaternion originRotation;
	// Use this for initialization
	void Start () {
		//Get the origin position and rotation
		/*originPosition = new Vector3(transform.position.x,
		                             Grid.tamGrid,
		                             transform.position.z);*/
		originPosition = new Vector3(Grid.tamGrid/2,
		                             Grid.tamGrid,
		                             -Grid.tamGrid-(Grid.tamGrid/2));
	
		originRotation = new Quaternion(transform.rotation.x,
		                                transform.rotation.y,
		                                transform.rotation.z,
		                                transform.rotation.w);
		resetCamera();
	}
	
	// Update is called once per frame
	void Update () {


		moveCamera();

		if (Input.GetMouseButtonDown(1)){
			rotate = true;
		}else if (Input.GetMouseButtonUp(1)){
				rotate = false;
		}

		if (rotate == true){
			rotateCamera();
		}


		if (rotate == false && Input.GetKey(KeyCode.Space)){
			resetCamera();
		}

		zoomCamera();
	}
	//Move the camera
	void moveCamera(){
		//Feed with WASD input.
		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");
		moveDirection = new Vector3 (x,0,z);
		//Multiply for velocity
		moveDirection = moveDirection*speed;
		//Move the camera
		transform.Translate(moveDirection,Space.World);
	
	}

	//Rotate the camera
	void rotateCamera(){
		//Feed with right mouse button input
		rotation.y = Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;
		//v3Rotate.x = Mathf.Clamp(v3Rotate.x, min, max);
		//transform.localEulerAngles = rotation;
		transform.Rotate(rotation,Space.World);
	}

	//Zoom in/out the camera
	void zoomCamera(){


		if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward
		{
			Camera.main.fieldOfView-=2;
			
		}
		if (Input.GetAxis("Mouse ScrollWheel") < 0) // back
		{
			Camera.main.fieldOfView+=2;
		}
	}
	
	//Press space to reset the camera to the origin position
	void resetCamera(){
		transform.rotation = originRotation;
		transform.position = originPosition;
	}
}
