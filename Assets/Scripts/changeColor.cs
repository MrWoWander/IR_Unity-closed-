using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class changeColor : NetworkBehaviour {

	public string color;

	public void change(string _color){
		if (isLocalPlayer) {
			color = _color;
		}
	}
}
