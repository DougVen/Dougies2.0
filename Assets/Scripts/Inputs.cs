using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Inputs : NetworkBehaviour 
{

	// Use this for initialization
	public KeyCode 
                 up_Joy,
                 right_Joy,
                 left_Joy,
                 fire_Joy;

    public DougieStates states;

    private bool moving;

	void Start () {
		
	}

	
	public void refreshStates(){
		states.goingUp 	= Input.GetKey(up_Joy);

		states.shooting =  Input.GetKey(fire_Joy);

		if (!states.left) {
			states.right = Input.GetKey(right_Joy);
			if (states.right)
				states.goingLeft = false;
				
		}

		if (!states.right) {
			states.left = Input.GetKey(left_Joy);
			if (states.left)
				states.goingLeft = true;
		}
	}
	
	// Update is called once per frame
	void Update () {

		if(!isLocalPlayer)
			return;

		refreshStates();

		
	}
}
