using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TacoBehaviour : NetworkBehaviour {
	public	float start = 0f;
	public float lifespan = 0.25f;

	void Start () {
		start = Time.time;
	}
}
