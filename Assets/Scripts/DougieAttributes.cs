using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DougieAttributes : MonoBehaviour {

	public float hp = 3;
	public Vector2 floatingBaseForce;
  	public float verticalSpeedLimit = 5f,
				 horizontalSpeedLimit = 6f,
                 horizontalSpeed = 4f,
                 horizontalSpeedTaco = 9f,
                 tacoFireRate = 0.8f,
				 horizontalStoppingForce = 0.5f,
				 horizontalMovingForce = 5f,
  	             nextTacoShot = 0;

	void Start () {
		floatingBaseForce = new Vector2(0, 20f);
	}
}
