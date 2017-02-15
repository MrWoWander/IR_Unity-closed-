using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class SyncRotation : NetworkBehaviour {

	[SyncVar(hook="OnPlayerRotationSynced")] private float syncBodyRotation;
	[SyncVar(hook="OnCamRotationSynced")] private float syncCamRotation;

	[SerializeField] private Transform bodyTransform;
	[SerializeField] private Transform camTransform;
	private float lerpRate = 20;

	private float lastBodyRotation;
	private float lastCamRotation;
	private float threshold = 1;

	private List<float> syncPlayerRotList = new List<float>();
	private List<float> syncCamRotList = new List<float>();
	private float closeEnought = 0.4f;
	[SerializeField] private bool useHistoricalInterpolation;


	void FixedUpdate(){
	    //LocalRot ();
		TransmitRotations ();
		lerpRotations ();
	}

	void lerpRotations(){
		if (!isLocalPlayer) {
			//bodyTransform.rotation = Quaternion.Lerp (bodyTransform.rotation, syncBodyRotation, Time.deltaTime * lerpRate);
			//camTransform.rotation = Quaternion.Lerp (camTransform.rotation, syncCamRotation, Time.deltaTime * lerpRate);
			if (useHistoricalInterpolation) {
				HistoricalInterpolation ();
			} else {
				OrdinaryLerp (); 
			}

		}
	}

	void OrdinaryLerp(){
	
		LerpPlayerRot (syncBodyRotation);
		LerpCamRot (syncCamRotation);
	}

	void LerpPlayerRot(float RotAngle){
		Vector3 playerNewRot = new Vector3 (0,RotAngle,0);
		bodyTransform.rotation = Quaternion.Lerp (bodyTransform.rotation, Quaternion.Euler (playerNewRot), lerpRate * Time.deltaTime);
	}

	void LerpCamRot(float RotAngle){
		Vector3 camNewRot = new Vector3 (RotAngle, 0, 0);
		camTransform.localRotation = Quaternion.Lerp (camTransform.localRotation, Quaternion.Euler (camNewRot), lerpRate * Time.deltaTime);
	}

	void HistoricalInterpolation(){
		if (syncPlayerRotList.Count > 0) {
		
			LerpPlayerRot (syncPlayerRotList [0]);

			if (Mathf.Abs (bodyTransform.localEulerAngles.y - syncPlayerRotList [0]) < closeEnought) {
				syncPlayerRotList.RemoveAt (0);
			}

		}

		if (syncCamRotList.Count > 0) {
			LerpCamRot (syncCamRotList [0]);

			if (Mathf.Abs (camTransform.localEulerAngles.x - syncCamRotList [0]) < closeEnought) {
				syncCamRotList.RemoveAt (0);
			}
		}
	
	}


	[Command]
	void CmdProvideRotations(float bodyRotation, float camRotation){
	
		syncBodyRotation = bodyRotation;
		syncCamRotation = camRotation;
	}

	[Client]
	void TransmitRotations(){

		if (isLocalPlayer){
			//if (Quaternion.Angle (bodyTransform.rotation, lastBodyRotation) > threshold || Quaternion.Angle (camTransform.rotation, lastCamRotation) > threshold) {
			if(CheckThreshold(bodyTransform.localEulerAngles.y,lastBodyRotation)|| CheckThreshold(camTransform.localEulerAngles.x,lastCamRotation)){
				
			}
			lastBodyRotation = bodyTransform.localEulerAngles.y;
			lastCamRotation = camTransform.localEulerAngles.x;
			CmdProvideRotations (lastCamRotation, lastCamRotation);
		}

	}

	bool CheckThreshold(float rot1, float rot2){
		if (Mathf.Abs (rot1 - rot2) > threshold) {
			return true;
		}
	
	else{ 
		return false;
	}
}

	[Client]
	void OnPlayerRotationSynced(float lastPlayerRot){
	
		syncBodyRotation = lastPlayerRot;
		syncPlayerRotList.Add (syncBodyRotation);
	}

	[Client]
	void OnCamRotationSynced(float lateCamRot){
		syncCamRotation = lateCamRot;
		syncCamRotList.Add (syncCamRotation);
	}
	/*void LocalRot(){

		bodyTransform.rotation = camTransform.rotation;
	}*/
}
