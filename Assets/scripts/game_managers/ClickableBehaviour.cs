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
		if (Input.GetKey ("mouse 0")) {
			controller.select_unit (this);
		}
	}
}
