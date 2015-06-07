using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using SocketIO;
using Mutsuki;

public class Main : MonoBehaviour {
	private static PacketGateway gateway;

	public static Map map;

	public static int playerId;

	public GameObject player;
	public GameObject enemy;
	
	public Dictionary<int, MObject> objectDict;

	void Start () {
		GameObject go = GameObject.Find ("SocketIO");
		SocketIOComponent socket = go.GetComponent <SocketIOComponent>();
		gateway = new PacketGateway (socket);

		objectDict = new Dictionary<int, MObject>();

		gateway.onLoginDelegate = Login;
		gateway.onResponseMapDelegate = ResponseMap;
		gateway.onNewObjectDelegate = NewObject;
		gateway.onRemoveObjectDelegate = RemoveObject;
		gateway.onMoveNotifyDelegate = MoveNotify;
	}

	void Update() {
		if (map != null) {
			int x = objectDict[playerId].x;
			int y = objectDict[playerId].y;

			bool up, down, left, right;
			up = Input.GetKey ("up");
			down = Input.GetKey ("down");
			left = Input.GetKey ("left");
			right = Input.GetKey ("right");

			if (up ^ down) {
				if (up) {
					gateway.requestMove (x, y + 1);
				}
				if (down) {
					gateway.requestMove (x, y - 1);
				}
			}
			if (left ^ right) {
				if (left) {
					gateway.requestMove (x - 1, y);
				}
				if (right) {
					gateway.requestMove (x + 1, y);
				}
			}
		}
	}

	public void Login(LoginPacket packet) {
		gateway.requestMap ();
		playerId = packet.movableId;
	}

	public void ResponseMap(ResponseMapPacket packet) {
		GameObject mapObject = GameObject.Find ("Map");
		map = mapObject.GetComponent<Map> ();
		map.SetUp (packet.data);
	}

	public void NewObject(NewObjectPacket packet) {
		int id = packet.movableId;
		if (!objectDict.ContainsKey (id)) {
			GameObject gameObject = null;

			switch (packet.category) {
			case Category.Player:
				gameObject = Instantiate (player);
				break;
			case Category.Enemy:
				gameObject = Instantiate (enemy);
				break;
			}

			if (gameObject != null) {
				MObject mObject = gameObject.GetComponent<MObject> ();
				mObject.SetUp (packet);
			
				objectDict.Add (id, mObject);

				if(id == playerId) {
					GameObject camera = GameObject.Find ("Main Camera");
					camera.transform.parent = gameObject.transform;
					camera.transform.localPosition = new Vector3(0.0f, 1.0f, -3.0f);
				}
			}
		}
	}

	public void RemoveObject(RemoveObjectPacket packet) {
		int id = packet.movableId;
		MObject mObject = objectDict [id];
		Destroy (mObject);
		objectDict.Remove (id);
	}

	public void MoveNotify(MoveNotifyPacket packet) {
		int objectId = packet.movableId;
		MObject mObject = objectDict[objectId];
		int x = packet.x;
		int y = packet.y;
		mObject.updatePos (x, y);
	}
}
