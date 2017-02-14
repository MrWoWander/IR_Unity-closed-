using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class rotateEarth : NetworkBehaviour
{
    public int degrees = 100;//при 100 работает лучше всего 
	float b;
	public GameObject Earth;
	//public Text text;
    public GameObject slider;//сюда тащить drag-n-drop из иерархии slider 
	[SyncVar] public float z;

	[Command]
	public void CmdEarth(float b){
		//z = slider.GetComponent<Slider> ().value;
			z = b;
			slider.GetComponent<Slider> ().value = z;
		
	}

	[Client]
	void hmm(){
			b = slider.GetComponent<Slider> ().value;
			CmdEarth (b);
	}


	void krr(){
		transform.Rotate(Vector3.up, z*degrees * Time.deltaTime);
	}


	void Update(){
		hmm ();
		krr ();
	}
	//transform.Rotate(Vector3.up, z*degrees * Time.deltaTime);//меняем тут положение 
}