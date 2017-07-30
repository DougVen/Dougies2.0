using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TacoBehaviour : NetworkBehaviour {
	public	float start = 0f;
	public float lifespan = 0.25f;
	public GameObject explosion;
	public float despawnTime;

	void Start () {
		start = Time.time;
		Invoke ("Despawn", despawnTime);
	}

	void Despawn() {
		Destroy (gameObject);
		var position = this.transform.position;
		var explosionInstance = Instantiate (explosion, position, Quaternion.identity);
	}

	void OnTriggerEnter2D(Collider2D collision) {
		Despawn ();
	}
}
