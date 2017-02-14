using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.IO;

public class rayCas : NetworkBehaviour
{
    Transform prefabView;
    Transform prefab;
	RaycastHit hit;
    bool _testing;
    int time;
    GameObject point;
	public GameObject greenPoint;
	public GameObject redPoint;
	public GameObject blackPoint;
	public GameObject yellowPoint;
	public string defColor = "red";
	public TrailRenderer lineObject;
	private TrailRenderer lineObjectInstance;
	bool trail;
	bool line;
	GameObject Book; 
	Transform pagepref;
	Text text;
	string path;


    void Start()
    {
		prefabView = gameObject.transform.GetChild (0).gameObject.GetComponent<Transform> ();
		prefab = gameObject.transform.GetChild (0).gameObject.transform.GetChild (0).gameObject.transform;
		pagepref = gameObject.transform.GetComponent<Transform> ().gameObject.transform.GetChild (4).gameObject.transform;
    }

	[Command]
	void CmdPoint(){
		defColor = gameObject.GetComponent<changeColor> ().color;
		switch (defColor) {
		case "red":
			point = redPoint;
			break;
		case "green":
			point = greenPoint;
			break;
		case "black":
			point = blackPoint;
			break;
		case "yellow":
			point = yellowPoint;
			break;
		}
		GameObject pnt =  Instantiate(point, hit.point, Quaternion.identity) as GameObject;
		if (pnt.transform.position.x < 5.479) {
			Destroy (pnt);
		
		}
		NetworkServer.Spawn (pnt);
	}

	[Command]
	void CmdDest(){
		NetworkServer.Destroy (hit.collider.gameObject);
	}


    void Update()
    {
        time++;
        if (time == 4) time = 0;
        if (time == 2) _testing = true; // маленкая фича , для того, чтобы не обрабатывать несколько коллизий за кадр
		Ray ray = new Ray (prefab.position, prefab.forward);
		Debug.DrawRay (ray.origin, ray.direction * 10, Color.magenta);
        if (Physics.Raycast(ray, out hit))
        {			

			if((hit.collider.CompareTag("Book")) && (FibrumInput.GetJoystickButtonUp(FibrumInput.Button.A))){ 
				StartBook ();

			}
				
			//GetText ();

			if (Book) {
				if ((!Book.activeSelf) && (FibrumInput.GetJoystickButton (FibrumInput.Button.A))) { 
					CloseBook ();
				}
			}


        }
		if (Input.GetKey ("o")) {
			ScrollControlUp ();
		}
		if (Input.GetKey ("i")) {
			ScrollControlDown ();
		}
    }
	void TrailRenderStart (Vector3 point, Vector3 normal)
	{
		lineObjectInstance = Instantiate (lineObject, point + normal * 0.01f, Quaternion.identity) as TrailRenderer;
	}
	void TrailRender (Vector3 point, Vector3 normal)
	{
		lineObjectInstance.transform.position = point + normal * 0.01f;
	}
	void TrailRenderStop ()
	{
		lineObjectInstance = null;
	}
	void StartBook()
	{
		Book = GameObject.FindWithTag ("Book"); 
		pagepref.gameObject.SetActive (true); 
		Book.gameObject.SetActive (false); 

	}
	void CloseBook()
	{
		Book.transform.position = hit.point + new Vector3(0,0,0);
		Book.gameObject.SetActive (true); 
		pagepref.gameObject.SetActive (false); 
	}
	void GetText()
	{
		path = @"D://Dota2/text.txt";
		gameObject.transform.GetChild (4).GetChild (0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text> ().text = File.ReadAllText (path);
	}

	void ScrollControlUp(){
		ScrollRect scr =  gameObject.transform.GetChild (4).GetChild (0).GetChild (0).GetComponent<ScrollRect> ();
		Scrollbar bar = gameObject.transform.GetChild (4).GetChild (0).GetChild (0).GetChild (1).GetComponent<Scrollbar> ();
		float currentValue = bar.value;
		scr.verticalNormalizedPosition = currentValue + 0.005f;
	}
	void ScrollControlDown(){
		ScrollRect scr =  gameObject.transform.GetChild (4).GetChild (0).GetChild (0).GetComponent<ScrollRect> ();
		Scrollbar bar = gameObject.transform.GetChild (4).GetChild (0).GetChild (0).GetChild (1).GetComponent<Scrollbar> ();
		float currentValue = bar.value;
		scr.verticalNormalizedPosition = currentValue - 0.005f;
	}
}

