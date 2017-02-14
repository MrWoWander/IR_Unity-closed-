using UnityEngine;
using UnityEngine.Networking;

public class SyncFire : NetworkBehaviour {

	[SerializeField] GameObject bulletPrefab;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (isLocalPlayer) {
			if (FibrumInput.GetJoystickButtonDown (FibrumInput.Button.A)) {
				CmdFire ();
			}
		}
	}

	[Command]

	void CmdFire(){
		GameObject bullet = Instantiate (bulletPrefab, 
			gameObject.transform.position + gameObject.transform.TransformDirection (Vector3.forward * 0.5f - Vector3.up * 0.5f), 
			gameObject.transform.GetChild(0).gameObject.transform.rotation) as GameObject;
		bullet.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward*20f,ForceMode.Impulse);
		Destroy (bullet,10f);
		NetworkServer.Spawn (bullet);

	}


}
