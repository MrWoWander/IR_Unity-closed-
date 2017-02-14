using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class PlayerNetworkControl : NetworkBehaviour {
	Camera sceneCamera;

	void Start(){
		if (!isLocalPlayer) {
			//gameObject.transform.GetChild (0).gameObject.SetActive (false);

			//gameObject.transform.GetChild (0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
			gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(false);

			//gameObject.transform.GetChild (0).gameObject.transform.GetChild (1).gameObject.SetActive (false);
			gameObject.transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(false);

			//gameObject.transform.GetChild (0).gameObject.transform.GetChild (2).gameObject.SetActive (false);
			gameObject.transform.GetChild(0).GetChild(0).GetChild(2).gameObject.SetActive(false);

			//gameObject.transform.GetChild (0).gameObject.GetComponent<AudioListener> ().enabled = false;
			gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<AudioListener>().enabled = false;
		}else {
			GameObject.Find ("StartCamera").SetActive (false);
		}
	}

}
