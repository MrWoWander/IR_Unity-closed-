using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

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
	public AudioSource test;
	GameObject Book; 
	Transform pagepref; 
	AudioClip clip;
	bool bly = true;
	bool bly1 = true;
	bool bly2 = true;
	bool bly3 = true;
	bool checkAudio = true;
	[SerializeField] private AudioClip clipBook;
	[SerializeField] private AudioClip clipTable;
	[SerializeField] private AudioClip clipShelf;
	[SerializeField] private AudioClip metreOne;
	[SerializeField] private AudioClip metreTwo;
	[SerializeField] private AudioClip metreThree;
	[SerializeField] private AudioClip metreFour;
	[SerializeField] private AudioClip metreFive;
	[SerializeField] private AudioClip metreMore;


    void Start()
    {

			prefabView = gameObject.transform.GetChild (0).gameObject.GetComponent<Transform> ();
			prefab = gameObject.transform.GetChild (0).gameObject.transform.GetChild (0).gameObject.transform;

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
		pagepref = gameObject.transform.GetComponent<Transform> ().gameObject.transform.GetChild (4).gameObject.transform;
        time++;
        if (time == 4) time = 0;
        if (time == 2) _testing = true; // маленкая фича , для того, чтобы не обрабатывать несколько коллизий за кадр
		Ray ray = new Ray (prefab.position, prefab.forward);
		Debug.DrawRay (ray.origin, ray.direction * 10, Color.magenta);
        if (Physics.Raycast(ray, out hit))
        {
			if(hit.collider.CompareTag("Book")||hit.collider.CompareTag("Shelf")||hit.collider.CompareTag("Table")){
				if (!test.isPlaying) {
					if (hit.collider.CompareTag ("Shelf") && bly == true) {
						test.PlayOneShot (clipShelf);
						AudioClip dist = DistAudio (hit.distance);
						test.clip = dist;
						test.PlayDelayed (clipShelf.length);
						bly = false;


					}

					if (!hit.collider.CompareTag ("Shelf")) {
						bly = true;
					}
					if (hit.collider.CompareTag ("Book") && bly3 == true) {
						test.PlayOneShot (clipBook);
						AudioClip dist = DistAudio (hit.distance);
						test.clip = dist;
						test.PlayDelayed (clipBook.length);
						bly3 = false;
					}

					if (!hit.collider.CompareTag ("Book")) {
						bly3 = true;
					}
					if (hit.collider.CompareTag ("Table") && bly2 == true) {
						test.PlayOneShot (clipTable);
						AudioClip dist = DistAudio (hit.distance);
						test.clip = dist;
						test.PlayDelayed (clipTable.length);
						bly2 = false;


					}

					if (!hit.collider.CompareTag ("Table")) {
						bly2 = true;
					}
				}
			}

			
			if((hit.collider.CompareTag("Book")) && (FibrumInput.GetJoystickButtonUp(FibrumInput.Button.A))){ 
				Book = GameObject.FindWithTag ("Book"); 
				pagepref.gameObject.SetActive (true); 
				Book.gameObject.SetActive (false); 
			}
			if (Book) {
				if ((!Book.activeSelf) && (FibrumInput.GetJoystickButton (FibrumInput.Button.A))) { 

					Book.gameObject.SetActive (true); 
					pagepref.gameObject.SetActive (false); 
				}
			}
			/*if (_testing)
            {
				if (isLocalPlayer) {
					if ((hit.collider.CompareTag ("point")) && (FibrumInput.GetJoystickButton(FibrumInput.Button.B))) 
					{
						Destroy (hit.collider.gameObject);
					}
					if ((hit.collider.CompareTag ("Map")) && (FibrumInput.GetJoystickButton(FibrumInput.Button.B)))
					{
						CmdPoint ();
					}
				}
                _testing = false;
            }
			if (FibrumInput.GetJoystickButtonDown(FibrumInput.Button.A)) {
				line = true;
			} else {
				line = false;
			}
			if ((hit.collider.CompareTag("Map"))&(line))
			{
				trail = true;
				TrailRenderStart (hit.point, hit.normal);
			}
			if (!FibrumInput.GetJoystickButton(FibrumInput.Button.A)) {
				trail = false;
				TrailRenderStop ();
			}
			if (trail & hit.collider.CompareTag("Map")) 
			{
				TrailRender (hit.point, hit.normal);
			}*/
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


	AudioClip DistAudio(float dist){
		
		if (dist > 0.5f && dist <= 1f) {
			clip = metreOne;
		}
		if (dist > 1f && dist < 1.9f)
		{
			clip = metreTwo;
		}
		if (dist > 2f && dist < 2.9f)
		{
			clip = metreThree;
		}
		if (dist > 3f && dist < 3.9f)
		{
			clip = metreFour;
		}
		if (dist > 4f && dist < 5f)
		{
			clip = metreFive;
		}
		if (dist > 5f)
		{
			clip = metreMore;
		}
		return clip;
		}
}

