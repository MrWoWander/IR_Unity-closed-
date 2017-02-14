using UnityEngine;
using System.Collections;

public class rotrotrot : MonoBehaviour {

	public float z = 0.50f;
	float degrees = 100;
	void Update () {
		transform.Rotate(Vector3.up, z*degrees * Time.deltaTime);
	}
}
