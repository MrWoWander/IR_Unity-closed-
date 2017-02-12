using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SyncPosition : NetworkBehaviour {

	[SyncVar]
	private Vector3 syncPos;
	[SerializeField] Transform myTransform;
	[SerializeField] float lerpRate = 15;

	private Vector3 lastPos;
	private float threshold = 0.5f;

	void Update(){

		LerpPosition ();

	}

	void FixedUpdate(){
		TransmitPosition ();

	}

	void LerpPosition(){
	
		if (!isLocalPlayer) {
		
			myTransform.position = Vector3.Lerp (myTransform.position, syncPos, Time.deltaTime * lerpRate);
		}
	}

	[Command]
	void CmdProvidePosition(Vector3 pos){
		syncPos = pos;
	
	}

	[ClientCallback]
	void TransmitPosition(){
	
		if (isLocalPlayer && Vector3.Distance(myTransform.position,lastPos)>threshold) {
			CmdProvidePosition (myTransform.position);
			lastPos = myTransform.position;
		}
	}


}
