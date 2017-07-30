using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonSetter : MonoBehaviour {

	public GameObject Blue;
	public GameObject Red;
	public GameObject Green;
	public GameObject Yellow;
	public Dictionary<string, GameObject> Balloons;
	public DougieBehaviour Dougie;
	public PlayerId Id;

	void PopulateDictionary ()
	{
		Balloons = new Dictionary<string, GameObject> ();
		Balloons.Add ("1", Blue);
		Balloons.Add ("2", Red);
		Balloons.Add ("3", Green);
		Balloons.Add ("4", Yellow);
	}

	void Start () {
		PopulateDictionary ();
	}

	void Update() {

		//Debug.Log ("Id: " + Id.Value);
		if (Dougie.Id == null || Id.Value == null || Dougie.Balloons != null)
			return;

		Vector3 position = new Vector3 (Dougie.transform.position.x - 0.045f, Dougie.transform.position.y + 0.45f, 0);
		GameObject instance = Instantiate (GetBalloonsPrefab(Dougie.Id.Value), position, Quaternion.identity);
		instance.gameObject.transform.parent = Dougie.transform;
		Dougie.Balloons = instance;
	}

	public GameObject GetBalloonsPrefab(string id) {
		return Balloons.ContainsKey (id) ? Balloons [id] : Balloons ["4"];
	}
}
