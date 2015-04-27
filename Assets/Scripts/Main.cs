using UnityEngine;
using System.Collections;
using SocketIO;


public class Main : MonoBehaviour {
	private PacketGateway gateway;
	
	// Use this for initialization
	void Start () 
	{
		GameObject go = GameObject.Find ("SocketIO");
		SocketIOComponent socket = go.GetComponent <SocketIOComponent>();
		gateway = new PacketGateway (socket);
	}

	void OnGUI() 
	{
		if (GUI.Button (new Rect (10, 10, 50, 50), "ping")) {
			gateway.ping ();
		}
		if (GUI.Button (new Rect (10, 60, 50, 50), "echo")) {
			gateway.echoDemo ();
		}
	}
}
