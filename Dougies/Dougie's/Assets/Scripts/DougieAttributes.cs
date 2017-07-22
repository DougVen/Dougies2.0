using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine;

public class DougieAttributes : NetworkBehaviour {

	public float hp = 3;
	public Vector2  floatingForce;
  	public float    verticalSpeedLimit 	 = 5f,
                  horizontalSpeed      = 5f,
                  horizontalSpeedTaco  = 0.0f,
                  tacoFireRate         = 0.8f,
  	              nextTacoShot         = 0;
	// Use this for initialization
	void Start () {
		floatingForce 		= new Vector2(0,150f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
