using UnityEngine;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class roomList : MonoBehaviour {

	private MatchInfoSnapshot match;

	public delegate void JoinRoomDelegate(MatchInfoSnapshot _match);
	private JoinRoomDelegate joinRoomCallback;
	[SerializeField] private Text roomNameText;

	public void Setup (MatchInfoSnapshot _match, JoinRoomDelegate _joinRoomCallback){

		match = _match;
		joinRoomCallback = _joinRoomCallback;
		roomNameText.text = match.name + "(" + match.currentSize + "/" + match.maxSize + ")";


	}

	public void JoinRoom(){

		joinRoomCallback.Invoke (match);

	}
}
