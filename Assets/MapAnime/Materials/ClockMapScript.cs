using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.Globalization;

public class ClockMapScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {

			//string time = System.DateTime.Now.ToString ("HH:mm");
			GameObject.Find("ClockMinus1").GetComponent<TextMesh>().text = System.DateTime.Now.AddHours (-1).ToString("HH:mm");
			GameObject.Find("Clock").GetComponent<TextMesh>().text = System.DateTime.Now.ToString("HH:mm");
			GameObject.Find("Clock1").GetComponent<TextMesh>().text = System.DateTime.Now.AddHours (1).ToString("HH:mm");
			GameObject.Find("Clock2").GetComponent<TextMesh>().text = System.DateTime.Now.AddHours (2).ToString("HH:mm");
			GameObject.Find("Clock3").GetComponent<TextMesh>().text = System.DateTime.Now.AddHours (3).ToString("HH:mm");
			GameObject.Find("Clock4").GetComponent<TextMesh>().text = System.DateTime.Now.AddHours (4).ToString("HH:mm");
			GameObject.Find("Clock5").GetComponent<TextMesh>().text = System.DateTime.Now.AddHours (5).ToString("HH:mm");
			GameObject.Find("Clock6").GetComponent<TextMesh>().text = System.DateTime.Now.AddHours (6).ToString("HH:mm");
			GameObject.Find("Clock7").GetComponent<TextMesh>().text = System.DateTime.Now.AddHours (7).ToString("HH:mm");
			GameObject.Find("Clock8").GetComponent<TextMesh>().text = System.DateTime.Now.AddHours (8).ToString("HH:mm");
			GameObject.Find("Clock9").GetComponent<TextMesh>().text = System.DateTime.Now.AddHours (9).ToString("HH:mm");



		}
}
