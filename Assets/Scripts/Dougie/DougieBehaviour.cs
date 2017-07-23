using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class DougieBehaviour : NetworkBehaviour {

	public Quaternion SpriteRot;
	public Rigidbody2D rigidbody;
	public Transform transform;
	public Collider2D feet;
	private DougieAttributes attributes;
	private DougieStates states;
	public Animator balloons;
	public GameObject taco;

	public Vector3 position;

	public int Balloon_Life = 3;

	void Awake(){
		transform  = GetComponent<Transform>();
		rigidbody  = GetComponent<Rigidbody2D>();
	}

	void Start () {
		states	   = GetComponent<DougieStates>();
		attributes = GetComponent<DougieAttributes>();
		rigidbody  = GetComponent<Rigidbody2D>();
	}

	void Shoot ()
	{
		if(Time.time < attributes.nextTacoShot)
			return;

		Bounds bounds = GetComponent<SpriteRenderer> ().bounds;
		var horizontalOffset =  bounds.size.x;

		float speed = attributes.horizontalSpeedTaco;

		if (states.goingLeft) {
			horizontalOffset *= -1;
			speed *= -1;
		}

		Vector3 offset = bounds.center + new Vector3 (horizontalOffset, 0, 0);
		Vector2 velocity = new Vector2 (speed, 0);
		
		CmdShoot (offset, velocity);

		attributes.nextTacoShot = Time.time + attributes.tacoFireRate;
		states.shooting = false;
	}

	[Command]
	void CmdShoot(Vector3 position, Vector2 velocity){
		var gameObject = Instantiate (taco, position, Quaternion.identity);
		gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2(velocity.x, velocity.y);
		NetworkServer.Spawn(gameObject);
		Destroy (gameObject, 1f);
	}

	void Update () {

		if (!isLocalPlayer)
			return;

		Move();
		Jump();
		Flip();

		if (states.shooting) 
			Shoot ();
		position = transform.position;
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
		if (Mathf.Abs (rigidbody.velocity.x) > 0.25f)
			rigidbody.AddForce (new Vector2 (rigidbody.velocity.x * -1.25f, 0));
		else
			rigidbody.velocity = new Vector2 (0, rigidbody.velocity.y);
	}

	 void ChangeDirection ()
	{
		if (states.left && rigidbody.velocity.x > 3 || states.right && rigidbody.velocity.x < -3) 
			rigidbody.velocity = new Vector2 (rigidbody.velocity.x * 0.95f, rigidbody.velocity.y);
	}

	void SetHorizontalForce()
	{
		if (states.isMovingHorizontally()) {
			ChangeDirection ();

			float x = states.left ? attributes.horizontalMovingForce * -1 : attributes.horizontalMovingForce;
			rigidbody.AddForce (new Vector2(x, 0));

			if (Mathf.Abs (rigidbody.velocity.x) > attributes.horizontalSpeedLimit) {
				float speed = rigidbody.velocity.x > 0 ? attributes.horizontalSpeedLimit : attributes.horizontalSpeedLimit * -1;
				rigidbody.velocity = new Vector2 (speed, rigidbody.velocity.y);
			}
		}
		else 
			Stop (); 
		
	}

	void Move(){
		SetHorizontalForce ();
		LimitFallingForce();
	}

	void Flip(){
		if(states.goingLeft)
			transform.rotation = Quaternion.Euler(0,180, 0);
		else
			transform.rotation = Quaternion.Euler(0,0, 0);
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
