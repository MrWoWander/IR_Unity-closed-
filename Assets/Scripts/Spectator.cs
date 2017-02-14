using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Spectator : NetworkBehaviour {
	bool trigger;

	void Start(){
		trigger = true;
	}
	[Command]
	void CmdDes(){

		gameObject.transform.GetChild (0).GetComponent<MeshRenderer> ().enabled = false;

	}

	void Update () {
		if (isLocalPlayer) {
			if (Input.GetKey ("p") && trigger == true) {
				gameObject.SetActive (false);
				GameObject spec = GameObject.Find ("Spec");
				spec.GetComponent<Camera> ().enabled = true;
				spec.GetComponent<AudioListener> ().enabled = true;
				CmdDes ();
				trigger = !trigger;
			}
		}
		/*if (Input.GetKey ("p") & trigger == false) {
			gameObject.SetActive (true);
			GameObject spec = GameObject.Find ("Spec");
			spec.GetComponent<Camera> ().enabled = false;
			spec.GetComponent<AudioListener> ().enabled = false;
			trigger = !trigger;
		}*/
	}
}
