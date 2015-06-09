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

		gateway.onResponseDelegate = OnResponse;
	}

	void Update() {
		if (map != null) {
			int x = objectDict[playerId].x;
			int y = objectDict[playerId].y;

			bool up, down, left, right;
			up = Input.GetKeyDown ("up");
			down = Input.GetKeyDown ("down");
			left = Input.GetKeyDown ("left");
			right = Input.GetKeyDown ("right");

			if (up ^ down) {
				RequestMovePacket packet;
				if (up) {
					packet = PacketFactory.requestMove(playerId, x, y + 1);
				}
				else {
					packet = PacketFactory.requestMove (playerId, x, y - 1);
				}
				gateway.request (packet);
			}
			if (left ^ right) {
				RequestMovePacket packet;
				if (left) {
					packet = PacketFactory.requestMove(playerId, x - 1, y);
				}
				else {
					packet = PacketFactory.requestMove(playerId, x + 1, y);
				}
				gateway.request (packet);
			}

			if(Input.GetKeyDown ("space")) {
//				gateway.requestMap (zoneId);
			}
		}
	}

	public void OnResponse(BasePacket packet) {
		var command = packet.command ();
		if (command == BasePacket.commands [PacketType.Connect]) {
			Connect ((ConnectPacket)packet);
		} else if (command == BasePacket.commands [PacketType.Login]) {
			Login ((LoginPacket)packet);
		} else if (command == BasePacket.commands [PacketType.ResponseMap]) {
			ResponseMap ((ResponseMapPacket)packet);
		} else if (command == BasePacket.commands [PacketType.NewObject]) {
			NewObject ((NewObjectPacket)packet);
		} else if (command == BasePacket.commands [PacketType.RemoveObject]) {
			RemoveObject ((RemoveObjectPacket)packet);
		} else if (command == BasePacket.commands [PacketType.MoveNotify]) {
			MoveNotify ((MoveNotifyPacket)packet);
		}
	}

	public void Connect(ConnectPacket packet) {
		Debug.Log ("onConnect: " + packet.toJson ());
	}

	public void Login(LoginPacket packet) {
		gateway.request (PacketFactory.requestMap (0));
		playerId = packet.movableId;
	}

	public void ResponseMap(ResponseMapPacket packet) {
		GameObject mapObject = GameObject.Find ("Map");
		map = mapObject.GetComponent<Map> ();
		map.SetUp (packet.width, packet.height, packet.zoneId, packet.data);
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
