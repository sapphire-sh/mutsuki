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

	void Update() {
		MObject user = objectDict [playerId];

		bool up, down, left, right;
		up = Input.GetKeyDown ("up");
		down = Input.GetKeyDown ("down");
		left = Input.GetKeyDown ("left");
		right = Input.GetKeyDown ("right");

		if (up ^ down) {
			if (up) {
				gateway.requestMove (user.x, user.y + 1);
			}
			if (down) {
				gateway.requestMove (user.x, user.y - 1);
			}
		}
		if (left ^ right) {
			if (left) {
				gateway.requestMove (user.x - 1, user.y);
			}
			if (right) {
				gateway.requestMove (user.x + 1, user.y);
			}
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
