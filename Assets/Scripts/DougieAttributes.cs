using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine;

public class DougieAttributes : NetworkBehaviour {

	public float hp = 3;
	public Vector2 floatingBaseForce;
  	public float verticalSpeedLimit = 5f,
                 horizontalSpeed = 4f,
                 horizontalSpeedTaco = 0.0f,
                 tacoFireRate = 0.8f,
				 horizontalStoppingForce = 0.5f,
  	             nextTacoShot = 0;

	void Start () {
		floatingBaseForce = new Vector2(0, 20f);
	}
}
