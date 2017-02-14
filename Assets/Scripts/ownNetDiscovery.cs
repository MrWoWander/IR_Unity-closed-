using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class ownNetDiscovery : NetworkDiscovery {
		void Start ()
		{
			#if UNITY_EDITOR
			Initialize();
			StartAsServer();
			Debug.Log("Started as server");
			#elif UNITY_ANDROID
			Initialize();
			StartAsClient();
			#endif
		}

		public override void OnReceivedBroadcast(string fromAddress, string data)
		{
			NetworkManager.singleton.networkAddress = fromAddress;
			NetworkManager.singleton.StartClient();
		}
	}
