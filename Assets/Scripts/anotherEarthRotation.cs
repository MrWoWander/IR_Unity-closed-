using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;


public class anotherEarthRotation : NetworkBehaviour {

	public GameObject slider;
	[SyncVar] float z;
	int degrees = 100;

	public void ChangeValue(){
		z = slider.GetComponent<Slider> ().value;
		CmdHz (z);
	
	}

	/*[ClientRpc]
	void RpcHz(float z){
		slider.GetComponent<Slider> ().value = z;
	}*/
	[Command]
	void CmdHz(float z){
		slider.GetComponent<Slider> ().value = z;
	}

	void Update(){
	
		//slider.GetComponent<Slider> ().onValueChanged.AddListener (ChangeValue);
		
		gameObject.transform.Rotate(Vector3.up, z*degrees * Time.deltaTime);
	}


}
