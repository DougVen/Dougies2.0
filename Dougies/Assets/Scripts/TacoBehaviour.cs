using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TacoBehaviour : NetworkBehaviour {
	public	float start = 0f;
	public float lifespan = 0.25f;
	// Use this for initialization
	void Start () {
			//transform = GetComponent<Transform>();
			start = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - start >= lifespan)
			 Destroy(gameObject);
	}
}
