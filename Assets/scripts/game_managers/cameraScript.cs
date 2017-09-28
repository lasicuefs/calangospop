using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour {

	Camera viewingCamera;
	float moveSpeed = 4.0f;

	public float minScreenSize = 2;
	public float maxScreenSize = 10;
	public float zoomVelocity = 1;

	void Awake(){
		viewingCamera = this.GetComponent<Camera>();
		if (viewingCamera == null){
			Debug.LogError("No camera attached to script. You need to set the viewing camera in the Inspector window");
		}
	}

	void Update () {
		//make the camera move at the same speed, independant of the tick count
		float translation = Time.deltaTime * moveSpeed; 

		//Move the GameObject
		if (Input.GetKey("a")){
			transform.Translate (-translation, 0, 0);
		}
		if (Input.GetKey("s")){
			transform.Translate (0, -translation, 0);
		}
		if (Input.GetKey("d")){
			transform.Translate (translation, 0, 0);
		}
		if (Input.GetKey("w")){
			transform.Translate (0, translation, 0);
		}

		if(Input.GetKey(KeyCode.Q)||Input.GetKey(KeyCode.Plus)) 
		{
			viewingCamera.orthographicSize = viewingCamera.orthographicSize + zoomVelocity*Time.deltaTime;
			if(viewingCamera.orthographicSize > maxScreenSize)
			{
				viewingCamera.orthographicSize = maxScreenSize; // Max size
			}
		}


		if(Input.GetKey(KeyCode.E)||Input.GetKey(KeyCode.Minus)) 
		{
			viewingCamera.orthographicSize = viewingCamera.orthographicSize - zoomVelocity*Time.deltaTime;
			if(viewingCamera.orthographicSize < minScreenSize)
			{
				viewingCamera.orthographicSize = minScreenSize; // Min size 
			}
		}
	}

	public void setCameraPos(float x, float y){
		viewingCamera.transform.position = new Vector3 (x, y, viewingCamera.transform.position.z);
	}
}
