using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {


	public float areaWidth;
	public float areaHeight;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		

		float ScreenX = ((Screen.width * 0.5f) - (areaWidth * 0.5f));
		float ScreenY = ((Screen.height * 0.5f) - (areaHeight * 0.5f));
		GUILayout.BeginArea (new Rect (ScreenX,ScreenY, areaWidth, areaHeight));
		
		/*
		if (Application.platform == RuntimePlatform.OSXWebPlayer ||
			Application.platform == RuntimePlatform.WindowsWebPlayer){
			
			
			if(GUILayout.Button ("Play")){
				OpenLevel("Island Level");
			}
			if(GUILayout.Button ("Instructions")){
				OpenLevel("Instructions");
			}
			
			}else{
		*/
		if(GUI.Button (new Rect(10,75,80,20),"Play")){
			//Debug.Log("Try to charge Level!");
			OpenLevel("TowerDefense");
			//Debug.Log("Level Chargued!");
		}
		

		
		if(GUI.Button (new Rect(10,125,80,20),"Quit")){
			Debug.Log("This part works!");
			Application.Quit();

		}
		
		//}
		
		GUILayout.EndArea();
	}

	void OpenLevel(string level){
		//yield return new WaitForSeconds(0.35f);
		SceneManager.LoadScene(level);
	}
}
