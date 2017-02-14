using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.Types;
using System.Collections.Generic;

public class JoinGame : MonoBehaviour {

	List<GameObject> roomList = new List<GameObject>();
	private NetworkManager networkManager;
	[SerializeField] private Text Status;
	[SerializeField] private GameObject roomListItemPrefab;
	[SerializeField] private Transform roomListParent;
	void Start(){


		networkManager = NetworkManager.singleton;
		if(networkManager.matchMaker == null){
			networkManager.StartMatchMaker (); 
		}

		RefreshRoomList ();
	}
	public void RefreshRoomList(){
	
		ClearRoomList ();
		networkManager.matchMaker.ListMatches (0,5,"",true,0,0,OnMatchList);
		Status.text = "Loading...";
	}

	public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches){

		Status.text = "";
		RectTransform parentTransform = roomListParent.GetComponent<RectTransform> ();

		foreach(MatchInfoSnapshot match in matches){
			GameObject roomListItemGO = roomListItemPrefab;
			//roomListItemGO.transform.SetParent (roomListParent);
			//roomListItemGO.GetComponent<RectTransform> ().localScale = new Vector3(1,1,1);
			//roomListItemGO.GetComponent<RectTransform> ().anchoredPosition3D = roomListParent.GetComponent<RectTransform> ().anchoredPosition3D;
			//roomListItemGO.GetComponent<RectTransform>().offsetMax = roomListParent.GetComponent<RectTransform>().offsetMax;
			//roomListItemGO.GetComponent<RectTransform> ().offsetMin = new Vector2 (roomListParent.GetComponent<RectTransform> ().offsetMin.x, roomListItemGO.GetComponent<RectTransform> ().offsetMax.y - roomListItemGO.GetComponent<RectTransform> ().rect.height);
			roomList _roomListItem = roomListItemGO.GetComponent<roomList> ();
			if (_roomListItem != null) {
			
				_roomListItem.Setup (match,JoinRoom);
			}

			roomList.Add (roomListItemGO);
		}
		if (roomList.Count == 0) {
			Status.text = "No rooms at the moment";
		}
	
	}

	void ClearRoomList(){

		for (int i = 0; i < roomList.Count; i++) {
		
			Destroy (roomList [i]);
		}

		roomList.Clear ();
	}
	public void JoinRoom(MatchInfoSnapshot _match){
		networkManager.matchMaker.JoinMatch (_match.networkId, "", "", "", 0, 0, networkManager.OnMatchJoined);
		ClearRoomList ();
		Status.text = "Joining...";

	}

	public void OnMatchJoined(bool success, string extendedInfo, MatchInfo matchInfo){


	}
}
