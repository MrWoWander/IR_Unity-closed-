using UnityEngine;
using UnityEngine.Networking;

public class JoystickSetupDemo : NetworkBehaviour {


	public VRCamera vrCamera;
	public float speed=3f;
	CharacterController cc;
	public GameObject bulletPrefab;
	[Command]
	void CmdFire(){
		GameObject bullet = Instantiate(bulletPrefab,
			vrCamera.vrCameraHeading.transform.position+vrCamera.vrCameraHeading.transform.TransformDirection(Vector3.forward*0.5f-Vector3.up*0.5f),
			vrCamera.vrCameraHeading.transform.rotation) as GameObject;
		bullet.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward*20f,ForceMode.Impulse);
		Destroy (bullet,10f);
		NetworkServer.Spawn (bullet);
	}
	// Use this for initialization
	void Start () {
		if (isLocalPlayer) {
			cc = gameObject.GetComponent<CharacterController> ();
		}
	}


	
	// Update is called once per frame
	void Update () {
		
		if(!isLocalPlayer){
			
			return;
		}
		cc.SimpleMove(speed*vrCamera.vrCameraHeading.TransformDirection(Vector3.forward*FibrumInput.GetJoystickAxis(FibrumInput.Axis.Vertical1)+Vector3.right*FibrumInput.GetJoystickAxis(FibrumInput.Axis.Horizontal1)));
		if (isLocalPlayer) {
			if (FibrumInput.GetJoystickButtonDown (FibrumInput.Button.Y)) {
				CmdFire ();
			}
		}
	}

	public void SetupJoystick()
	{

		if( FibrumInput.InitializeJoystickSetup() )
		{
			FibrumInput.joystickSetupGO.StartSetup();
		}
	}

	public void SetupJoystickScripted()
	{
		if( FibrumInput.InitializeJoystickSetup() )
		{
			FibrumInput.joystickSetupGO.ClearControls();
			FibrumInput.joystickSetupGO.SetTitle("Setup joystick input");
			FibrumInput.joystickSetupGO.SetControl(FibrumInput.Axis.Vertical1,true,"FORWARD\nChoose joystick axis to move forward",null);
			FibrumInput.joystickSetupGO.SetControl(FibrumInput.Axis.Horizontal1,false,"RIGHT\nChose joystick axis to move right",null);
			FibrumInput.joystickSetupGO.SetControl(FibrumInput.Button.A,"Chose button for fire",null);
			FibrumInput.joystickSetupGO.StartSetup();
		}
	}
}
