using UnityEngine.Networking;
using UnityEngine;

public class DougieStates : NetworkBehaviour {
	[SyncVar]
	public  bool right = false,
				 left = false,
				 goingUp   = false,
				 onair=false,
				 shooting  = false,
				 goingLeft=false;
}
