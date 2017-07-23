using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class DougieBehaviour : NetworkBehaviour {

	public Rigidbody2D rigidbody;
	public Transform transform;
	public Collider2D feet;
	private DougieAttributes attributes;
	private DougieStates states;

	 public Animator balloons;
	 public  GameObject taco;
	  [SyncVar]
	 public int Balloon_Life = 3;

	// Use this for initialization

	void Awake(){
		transform  = GetComponent<Transform>();
		rigidbody  = GetComponent<Rigidbody2D>();

	}

	void Start () {
		states	   = GetComponent<DougieStates>();
		attributes = GetComponent<DougieAttributes>();
		rigidbody  = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate () {
		

	}
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer)
		return;

		Move();
		Jump();
		CmdFlip();
		if(states.shooting)
		CmdShoot();
	}

	Vector2 GetUpwardForce ()
	{
		float y = rigidbody.velocity.y;

		if (y < 0)
			y = 0;
		
		float delta = attributes.verticalSpeedLimit - y;
		return new Vector2 (0, attributes.floatingBaseForce.y + (delta / 5) * 2);
	}

	void SetVerticalForce ()
	{
		
		rigidbody.AddForce (GetUpwardForce());
		if (rigidbody.velocity.y >= attributes.verticalSpeedLimit)
			rigidbody.velocity = new Vector2(rigidbody.velocity.x, attributes.verticalSpeedLimit);
	}

	void Jump(){
		if (!states.goingUp)
			return;
		
		SetVerticalForce ();		
	}

	void LimitFallingForce ()
	{
		float fallingForceLimit = attributes.verticalSpeedLimit * -2f;
		if (rigidbody.velocity.y <= fallingForceLimit)
			rigidbody.velocity = new Vector2 (rigidbody.velocity.x, fallingForceLimit);
	}

	void Stop ()
	{
		if (Mathf.Abs (rigidbody.velocity.x) > 0.35f)
			rigidbody.AddForce (new Vector2 (rigidbody.velocity.x * -1.25f, 0));
		else
			rigidbody.velocity = new Vector2 (0, rigidbody.velocity.y);
	}

	void ChangeDirection ()
	{
		if (states.left && rigidbody.velocity.x > 0 || states.right && rigidbody.velocity.x < 0) 
			rigidbody.velocity = new Vector2 (rigidbody.velocity.x/1.25f, rigidbody.velocity.y);
	}

	void SetHorizontalForce()
	{
		Debug.Log (rigidbody.velocity.x);
		if (states.isMovingHorizontally()) {
			ChangeDirection();

			float x = states.left ? attributes.horizontalMovingForce * -1 : attributes.horizontalMovingForce;
			rigidbody.AddForce (new Vector2(x, 0));

			if (Mathf.Abs (rigidbody.velocity.x) > attributes.horizontalSpeedLimit) {
				Debug.Log ("checkpoint");		
				float speed = rigidbody.velocity.x > 0 ? attributes.horizontalSpeedLimit : attributes.horizontalSpeedLimit * -1;
				rigidbody.velocity = new Vector2 (speed, rigidbody.velocity.y);
			}
		}
		else {
			Stop (); 
		}
	}

	void Move(){
		SetHorizontalForce ();
		LimitFallingForce();
	}

	 [SyncVar(hook = "UpdateFlip")] public Quaternion SpriteRot;
	
	 void UpdateFlip(Quaternion NewPos)
     {
         transform.localRotation = SpriteRot;
     }

	[Command]
	void CmdFlip(){
		if(!states.goingLeft)
			SpriteRot = Quaternion.Euler(0,0, 0);
		else
			SpriteRot = Quaternion.Euler(0,180, 0);
	}


	[Command]
	void CmdShoot(){
		//check if taco shooting cooldown is off, else to do nothing
		if(Time.time < attributes.nextTacoShot)
				return;
		states.shooting = false;
		//Set time for next taco shot
		attributes.nextTacoShot = Time.time + attributes.tacoFireRate;

		//set projectiles properties before instantiation
		// Position, Rotation, etc.
		float horizontalProjectileOffset = 0.77f;
		Vector3 offset;
		if(!states.goingLeft)
				offset = transform.position + new Vector3(horizontalProjectileOffset,0,0);
		else
				offset = transform.position + new Vector3(horizontalProjectileOffset*-1,0,0);

		GameObject tacoProjectile = (GameObject)Instantiate(taco,offset,transform.rotation);
		tacoProjectile.GetComponent<Transform>().Rotate(0,180,0);

		if(!states.goingLeft)
			tacoProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(attributes.horizontalSpeedTaco,0);
		else
			tacoProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(attributes.horizontalSpeedTaco*-1,0);
	}

	 void OnCollisionExit2D(Collision2D coll) {
	  	if(coll.gameObject.tag=="Platform")
      	  if (coll.otherCollider == feet)
         	  states.onair=true;
        
    }

    void OnCollisionEnter2D(Collision2D coll) {
	  	if(coll.gameObject.tag=="Platform")
      	  if (coll.otherCollider == feet)
         	  states.onair=false;

        if(coll.gameObject.tag=="Taco")
         	CmdReceiveDamage();
        
    }
	[Command]
    	public void CmdReceiveDamage(){
		attributes.hp -= 1;
		Balloon_Life -= 1;
        balloons.SetInteger("Balloon_Life", Balloon_Life);
	//	Instantiate(balloon, new Vector3 (transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
		if(attributes.hp == 0)
			Destroy(gameObject);
	}


  
}
