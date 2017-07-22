using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DougieBehaviour : MonoBehaviour {

	// Use this for initialization
	private Rigidbody2D rigidbody;
	private Transform transform;

	void Start () {
		states	   = GetComponent<DougieStates>();
		attributes = GetComponent<DougieAttributes>();
		rigidbody  = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
