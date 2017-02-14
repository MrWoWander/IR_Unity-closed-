using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SyncRotation : NetworkBehaviour {

	[SyncVar] private Quaternion syncBodyRotation;
	[SyncVar] private Quaternion syncCamRotation;

	[SerializeField] private Transform bodyTransform;
	[SerializeField] private Transform camTransform;
	[SerializeField] private float lerpRate = 15;



	void FixedUpdate(){
	    //LocalRot ();
		TransmitRotations ();
		lerpRotations ();
	}

	void lerpRotations(){
		if (!isLocalPlayer) {
			bodyTransform.rotation = Quaternion.Lerp (bodyTransform.rotation, syncBodyRotation, Time.deltaTime * lerpRate);
			camTransform.rotation = Quaternion.Lerp (camTransform.rotation, syncCamRotation, Time.deltaTime * lerpRate);
		}
	}

	[Command]
	void CmdProvideRotations(Quaternion bodyRotation, Quaternion camRotation){
	
		syncBodyRotation = bodyRotation;
		syncCamRotation = camRotation;
	}

	[Client]
	void TransmitRotations(){

		if (isLocalPlayer) {
		
			CmdProvideRotations (camTransform.rotation, camTransform.rotation);
		
		}

	}

	/*void LocalRot(){

		bodyTransform.rotation = camTransform.rotation;
	}*/
}
