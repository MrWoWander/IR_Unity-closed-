using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Pulse : MonoBehaviour {
	private RawImage redCity;
	public float drawSpeed = 1;
	// Use this for initialization
	void Start () {
		redCity = GetComponent<RawImage> ();

	}
	
	// Update is called once per frame
	void Update () {
		redCity.color = new Color{
			r = redCity.color.r,
			g = redCity.color.g,
			b = redCity.color.b,
			a = (float)((float)System.DateTime.Now.Millisecond / 1000f/**Time.deltaTime*drawSpeed*/)
		};
		//print (redCity.color);
	}
}
