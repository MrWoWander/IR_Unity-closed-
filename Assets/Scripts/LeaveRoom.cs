using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class LeaveRoom : MonoBehaviour {
	private NetworkManager networkManager;
	// Use this for initialization
	void Start () {
		networkManager = NetworkManager.singleton;
	}
	
	public void LeaveTheRoom(){

		MatchInfo matchInfo = networkManager.matchInfo;
		networkManager.matchMaker.DropConnection(matchInfo.networkId,matchInfo.nodeId,0,networkManager.OnDropConnection);
		networkManager.StopHost();
		Application.Quit ();

	}
}
