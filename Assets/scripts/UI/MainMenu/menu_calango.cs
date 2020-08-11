using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_calango : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
        Animator animator = GetComponent<Animator>();
        animator.SetBool("iddle", false);
        animator.SetBool("right", true);
        animator.speed = 0.6f;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
