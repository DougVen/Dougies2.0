﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine;

public class BalloonCounter : NetworkBehaviour {

	 private int Balloon_Life = 3;
	 public Animator balloons;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	 void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Taco"){
        	 Balloon_Life -= 1;
           	 balloons.SetInteger(" Balloon_Life", Balloon_Life);
        }
           
    }
}
