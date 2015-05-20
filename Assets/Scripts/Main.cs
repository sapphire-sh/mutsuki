using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SocketIO;
using Mutsuki;

public class Main : MonoBehaviour {
	private static PacketGateway gateway;

	public static MMap map;

	public static int playerId;
	public static Dictionary<int, MObject> objectDict;

	void Start () 
	{
		GameObject go = GameObject.Find ("SocketIO");
		SocketIOComponent socket = go.GetComponent <SocketIOComponent>();
		gateway = new PacketGateway (socket);

		objectDict = new Dictionary<int, MObject>();

		gateway.onLoginDelegate = Login;
		gateway.onResponseMapDelegate = ResponseMap;
		gateway.onNewObjectDelegate = NewObject;
		gateway.onRemoveObjectDelegate = RemoveObject;
		gateway.onMoveNotifyDelegate = MoveNotify;

		gateway.login ();
	}

	void OnGUI() 
	{
		if (GUI.Button (new Rect (60, 10, 50, 50), "up")) {
			MObject user = objectDict [playerId];
			gateway.requestMove(user.x, user.y + 1);
		}
		if (GUI.Button (new Rect (60, 60, 50, 50), "down")) {
			MObject user = objectDict [playerId];
			gateway.requestMove(user.x, user.y - 1);
		}
		if (GUI.Button (new Rect (10, 60, 50, 50), "left")) {
			MObject user = objectDict [playerId];
			gateway.requestMove(user.x - 1, user.y);
		}
		if (GUI.Button (new Rect (110, 60, 50, 50), "right")) {
			MObject user = objectDict [playerId];
			gateway.requestMove(user.x + 1, user.y);
		}
	}
	
	public void Login(JSONObject jsonObject) {
		gateway.requestMap ();
		playerId = (int)jsonObject.GetField ("id").n;
	}

	public void ResponseMap(JSONObject jsonObject) {
		map = MMap.CreateComponent (gameObject, jsonObject);
	}

	public void NewObject(JSONObject jsonObject) {
		MObject mObject = MObject.CreateComponent (gameObject, jsonObject);
		objectDict.Add (mObject.id, mObject);
	}

	public void RemoveObject(JSONObject jsonObject) {
		objectDict.Remove((int)jsonObject.GetField("id").n);
	}

	public void MoveNotify(JSONObject jsonObject) {
		int objectId = (int)jsonObject.GetField ("id").n;
		MObject mObject = objectDict[objectId];
		int x = (int)jsonObject.GetField ("pos").list [0].n;
		int y = (int)jsonObject.GetField ("pos").list [1].n;
		mObject.updatePos (x, y);
	}
}
