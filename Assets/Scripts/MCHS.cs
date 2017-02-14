using UnityEngine;
using System.Collections;

public class MCHS : MonoBehaviour {
	public GameObject a;
	public GameObject b;

	public void Mchs(){
		a.SetActive (false);
		b.SetActive (true);
		gameObject.SetActive (false);
	}
}
