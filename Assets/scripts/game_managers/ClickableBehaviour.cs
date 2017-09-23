using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableBehaviour : MonoBehaviour {

	PainelUnidadeController controller;

	// Use this for initialization
	void Start () {
		controller = GameObject.Find ("PainelUnidade").GetComponent<PainelUnidadeController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown () {
		Debug.Log ("mouse detec");
		if (Input.GetKey ("mouse 0")) {
			Debug.Log ("down detec");
			controller.select_Animal (this);
		}
	}
}
