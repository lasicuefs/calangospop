using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour {

	Camera viewingCamera;
	float moveSpeed = 4.0f;

	public float minScreenSize = 2;
	public float maxScreenSize = 6;
	float zoomVelocity = 1.5f;
    private Vector3 lastPosition;
    private float mouseSensitivity = .1f;
    float camHorizontalMin, camHorizontalMax, camVertMin, camVertMax, limitOffset;
    private float mapSize, totalExtraTerrain;

    MapGenerator mapGenerator;

    void Awake(){
        GameObject mapController = GameObject.Find("MapController");
        if (mapController != null) mapGenerator = mapController.GetComponent<MapGenerator>();
        if (mapGenerator != null)
        {
            mapSize = mapGenerator.mapSize * mapGenerator.tileSize;
            totalExtraTerrain = Mathf.CeilToInt(mapGenerator.mapSize * mapGenerator.ExtraTerrainPercentage) * mapGenerator.tileSize;
            limitOffset = mapSize * 0.1f;
        }
        else Debug.LogError("No map generator found");

        viewingCamera = this.GetComponent<Camera>();

        setCameraPos(-2, -((mapSize + totalExtraTerrain*2) / 4) * mapGenerator.tileSize);
        recalculateCameraLimits();
    }

	void Update () {


		//make the camera move at the same speed, independant of the tick count
		float translation = Time.deltaTime * moveSpeed; 

		//Move the GameObject
		if (Input.GetKey("a") || Input.GetKey("left"))
        {
			transform.Translate (-translation, 0, 0);
		}
		if (Input.GetKey("s") || Input.GetKey("down"))
        {
			transform.Translate (0, -translation, 0);
		}
		if (Input.GetKey("d") || Input.GetKey("right"))
        {
			transform.Translate (translation, 0, 0);
		}
		if (Input.GetKey("w") || Input.GetKey("up"))
        {
			transform.Translate (0, translation, 0);
		}

		if(Input.GetKey(KeyCode.Q)||Input.GetKey(KeyCode.KeypadMinus)) 
		{
			viewingCamera.orthographicSize = viewingCamera.orthographicSize + zoomVelocity*Time.deltaTime;
			if(viewingCamera.orthographicSize > maxScreenSize)
			{
				viewingCamera.orthographicSize = maxScreenSize; // Max size
			}

            recalculateCameraLimits();
        }


		if(Input.GetKey(KeyCode.E)||Input.GetKey(KeyCode.KeypadPlus)) 
		{
			viewingCamera.orthographicSize = viewingCamera.orthographicSize - zoomVelocity*Time.deltaTime;
			if(viewingCamera.orthographicSize < minScreenSize)
			{
				viewingCamera.orthographicSize = minScreenSize; // Min size 
			}

            recalculateCameraLimits();
        }


        //Mouse controls


        if (Input.GetMouseButtonDown(1))
        {
            lastPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 delta = Input.mousePosition - lastPosition;
            transform.Translate(delta.x * mouseSensitivity, delta.y * mouseSensitivity, 0);
            lastPosition = Input.mousePosition;
        }

        if(Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            float size = viewingCamera.orthographicSize;
            size -= Input.GetAxis("Mouse ScrollWheel") * minScreenSize;
            size = Mathf.Clamp(size, minScreenSize, maxScreenSize);
            viewingCamera.orthographicSize = size;

            recalculateCameraLimits();
        }
        

        setCameraPos(Mathf.Clamp(transform.position.x, camHorizontalMin, camHorizontalMax), Mathf.Clamp(transform.position.y, camVertMin, camVertMax));
    }

	public void setCameraPos(float x, float y){
		viewingCamera.transform.position = new Vector3 (x, y, viewingCamera.transform.position.z);
	}

    void recalculateCameraLimits()
    {
        
        float camVertExtent = viewingCamera.orthographicSize;
        float camHorzExtent = viewingCamera.aspect * camVertExtent;
        

        camHorizontalMin = - mapSize/2 + (camHorzExtent + limitOffset);
        camHorizontalMax = mapSize/2 - (camHorzExtent + limitOffset);
        camVertMax = - totalExtraTerrain / 2 - (camVertExtent + limitOffset/2);
        camVertMin = - totalExtraTerrain / 2 - mapSize/2 + (camVertExtent + limitOffset/2);
    }
}
