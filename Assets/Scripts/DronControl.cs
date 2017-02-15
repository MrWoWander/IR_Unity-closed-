using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using System.Net;
using System.IO;

public class DronControl : MonoBehaviour {
	private float degreeX, degreeY, degreeZ ;


	DroneControlInfo DroidInfo;

	// Use this for initialization
	void Start () {
		StartCoroutine (MonitoringDrone ());
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey ("y")) {
			transform.position = new Vector3 (transform.position.x, transform.position.y+1, transform.position.z);
		}
		if (Input.GetKey ("h")) {
			transform.position = new Vector3 (transform.position.x, transform.position.y-1, transform.position.z);
		}
		if (Input.GetKey ("[")) {
			degreeY -= 1f;
			//transform.Rotate (0,transform.rotation.y-1f,0);
		}
		if (Input.GetKey ("]")) {
			degreeY += 1f;
		}
		//transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (-30f,0,0), Time.deltaTime);
		//if(Input.GetKeyUp("i")){
		if(DroidInfo.ForwardUp){
			degreeX -= 20f;
		}
		//if(Input.GetKeyUp("j")){
		if(DroidInfo.LeftUp){
			degreeZ -= 20f;
		}
		//if(Input.GetKeyUp("l")){
		if(DroidInfo.RightUp){
			degreeZ += 20f;
		}
		//if(Input.GetKeyUp("k")){
		if(DroidInfo.BackUp){
			degreeX += 20f;
		}
		//gameObject.transform.position = new Vector3 (gameObject.transform.position.x + 0.2f, gameObject.transform.position.y, gameObject.transform.position.z);
		//if(Input.GetKey("i")){
		if (DroidInfo.Forward)
		{
			transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z+1);
			//if(/*gameObject.transform.rotation == new Quaternion (0,0,0,1)*/ Input.GetKeyDown("i")){
			if(/*gameObject.transform.rotation == new Quaternion (0,0,0,1)*/ DroidInfo.ForwardDown){
				degreeX += 20f;
			}
		}
		//if(Input.GetKey("j")){
		if(DroidInfo.Left){
			transform.position = new Vector3 (transform.position.x-1, transform.position.y, transform.position.z);
			//if(Input.GetKeyDown("j")){
			if(DroidInfo.LeftDown){
				degreeZ += 20f;
			}
		}
		//if(Input.GetKey("l")){
		if(DroidInfo.Right){
			transform.position = new Vector3 (transform.position.x+1, transform.position.y, transform.position.z);
		//	if(Input.GetKeyDown("l")){
			if(DroidInfo.RightDown){
				degreeZ -= 20f;
			}
		}
		//if(Input.GetKey("k")){
		if(DroidInfo.Back){
			transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z-1);
		//	if(Input.GetKeyDown("k")){
			if(DroidInfo.BackDown){
				print (degreeX);
				degreeX -= 20f;
			}
		}
		/*if(Input.GetKey("i")||Input.GetKey("j")||Input.GetKey("l")||Input.GetKey("k")){
			DronLerpRotation (degreeX,degreeY,degreeZ);
		}*/
		//if (!Input.GetKey ("[") || !Input.GetKey ("]")) {
			DronLerpRotation (degreeX, degreeY, degreeZ);
		//}
	}
	void DronLerpRotation(float degreeX, float degreeY, float degreeZ){
		transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (degreeX,degreeY,degreeZ), Time.deltaTime/0.5f);
	}
	/*IEnumerator JsonReciever(){
		var www = new WWW ("n91608b1.beget.tech/droncontrols.txt");
		yield return www;
		var json = www.text;
		if (json == " "){
			
		}
		var commands = JsonConvert.DeserializeObject<Command []> (json);
	}*/



	IEnumerator MonitoringDrone(){
		while (true) {
			if (Random.Range (0, 5) != 1)
				continue;
			var request = WebRequest.Create ("http://n91608b1.beget.tech/droncontrols.txt");
			yield return request;
			var response = request.GetResponse();
			using (StreamReader reader = new StreamReader(response.GetResponseStream()))
				DroidInfo = JsonConvert.DeserializeObject<DroneControlInfo> (reader.ReadToEnd());
			//print (JsonConvert.SerializeObject(DroidInfo));
		}
	}
}
class DroneControlInfo
{
	private static  bool f;
	private static bool b;
	private static bool r;
	private static bool l;

	private static  bool fup;
	private static bool bup;
	private static bool rup;
	private static bool lup;

	private static  bool pf;
	private static bool pb;
	private static bool pr;
	private static bool pl;

	private static  bool fval;
	private static bool bval;
	private static bool rval;
	private static bool lval;

	public bool Forward { get{ return fval;} set
		{	
			bool loc = fval;
		    fval = value;
			if (!pf && value)
				f = true;
			if (pf && !value)
				fup = true;
			pf = loc;
		}}
	public bool Back { get{ return bval;} set
		{ 
			bool loc = bval;
			bval = value;
			if (!pb && value)
				b = true;
			if (pb && !value)
				bup = true;
			pb = loc;
		}}
	public bool Right{ get{ return rval;} set
		{ 
			bool loc = rval;
			rval = value;
			if (!pr && value)
				r = true;
			if (pr && !value)
				rup = true;
			pr = loc;
		}}
	public bool Left{ get{ return lval;} set
		{ 
			bool loc = lval;
			lval = value;
			if (!pl && value)
				l = true;
			if (pl && !value)
				lup = true;
			pl = loc;
		}}



	public bool ForwardDown { get
		{
			if (f) {
				f = false;
				return true;
			}
			return false;
		}
	}
	public bool BackDown { get
		{
			if (b) {
				b = false;
				return true;
			}
			return false;
		}  }
	public bool RightDown{ get
		{
			if (r) {
				r = false;
				return true;
			}
			return false;
		}  }
	public bool LeftDown{ get
		{
			if (l) {
				l = false;
				return true;
			}
			return false;
		}  }






	public bool ForwardUp{
		get {
			if (fup) {
				fup = false;
				return true;
			}
			return false;
		}
	}

	public bool BackUp{
		get {
			if (bup) {
				bup = false;
				return true;
			}
			return false;
		}
	}
	public bool RightUp{
		get {
			if (rup) {
				rup = false;
				return true;
			}
			return false;
		}
	}
	public bool LeftUp{
		get {
			if (lup) {
				lup = false;
				return true;
			}
			return false;
		}
	}
}