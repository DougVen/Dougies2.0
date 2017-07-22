using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

	// Use this for initialization
	public int player;
	private KeyCode leftArrow,rightArrow,
	jump,powerup,shoot,shield;
	
	public PlayerStates states;
	void Start () {
		//futuro poner aqui get para ellos
		
	}
	

	// Update is called once per frame
	void Update () {
		

		states.goingLeft=Input.GetKeyDown(leftArrow);
		states.goingRight=Input.GetKeyDown(rightArrow);
		

		if(Input.GetKeyDown(jump))
			states.jumping=true;
		
		if(Input.GetKeyDown(powerup))
			states.powerup=true;
		
		if(Input.GetKeyDown(shoot))
			states.shooting=true;
		
		if(Input.GetKeyDown(shield))
			states.shield=true;
		
	}
}
