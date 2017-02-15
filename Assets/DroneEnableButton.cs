using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DroneEnableButton : MonoBehaviour {


	public GameObject plane;
	public void DroneTrigger(){
		gameObject.SetActive (false);
		plane.SetActive (true);
	}
}
