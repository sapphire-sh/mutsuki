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
	
	public static Dictionary<int, MObject> objectDict;
	
	public static Queue<MoveNotifyPacket> movementQueue;

	void Start () {
		GameObject go = GameObject.Find ("SocketIO");
		SocketIOComponent socket = go.GetComponent <SocketIOComponent>();
		gateway = new PacketGateway (socket);

		GameObject mapObject = GameObject.Find ("Map");
		map = mapObject.GetComponent<Map> ();

		objectDict = new Dictionary<int, MObject>();
		movementQueue = new Queue<MoveNotifyPacket> ();

		gateway.onResponseDelegate = OnResponse;
	}

	void Update() {
		if (map != null && objectDict.ContainsKey(playerId)) {
			var player = objectDict[playerId];
			int x = (int)player.pos.x;
			int y = (int)player.pos.z;

			if(player.status == MObject.Status.Stop) {
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
					request (packet);
				}

				if(Input.GetKeyDown ("space")) {
					removeAllObjects();
					map.RequestJumpZone();
				}
			}

			if(Input.GetKeyDown("r")) {
				removeAllObjects();
				var packet = PacketFactory.gameRestart();
				request(packet);
			}
		}

		while(movementQueue.Count > 0) {
			var packet = movementQueue.Peek();
			if(objectDict.ContainsKey(packet.movableId)) {
				var mObject = objectDict[packet.movableId];
				var target = new Vector3(packet.x, 0.0f, packet.y);
				if(mObject.updateTarget(target)) {
					movementQueue.Dequeue();
				}
				else {
					break;
				}
			}
		}
	}

	public static void request(BasePacket packet) {
		gateway.request (packet);
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
		} else if (command == BasePacket.commands [PacketType.AttackNotify]) {
			AttackNotify ((AttackNotifyPacket)packet);
		}
	}

	public void Connect(ConnectPacket packet) {
		Debug.Log ("onConnect: " + packet.toJson ());
	}

	public void Login(LoginPacket packet) {
		var _packet = PacketFactory.requestMap (0);
		request (_packet);
		playerId = packet.movableId;

		foreach (var entry in objectDict) {
			Destroy (entry.Value);
		}
	}

	public void ResponseMap(ResponseMapPacket packet) {
		map.SetUp (packet.width, packet.height, packet.zoneId, packet.data);
	}

	public void NewObject(NewObjectPacket packet) {
		if (!objectDict.ContainsKey (packet.movableId)) {
			GameObject gameObject = null;

			switch (packet.category) {
			case Category.Player:
				gameObject = Instantiate (player);
				gameObject.transform.SetParent(map.transform);
				break;
			case Category.Enemy:
				gameObject = Instantiate (enemy);
				gameObject.transform.SetParent(map.transform);
				break;
			}

			if (gameObject != null) {
				MObject mObject = gameObject.GetComponent<MObject> ();
				mObject.name = "object_" + packet.movableId;
				mObject.SetUp (packet);

				objectDict.Add (packet.movableId, mObject);
			}
		}
	}

	public void RemoveObject(RemoveObjectPacket packet) {
		Destroy (GameObject.Find("object_" + packet.movableId));
		objectDict.Remove (packet.movableId);
	}

	public void MoveNotify(MoveNotifyPacket packet) {
		movementQueue.Enqueue (packet);
	}

	public void AttackNotify(AttackNotifyPacket packet) {

	}

	private void removeAllObjects() {
		foreach(var entry in objectDict) {
			var obj = GameObject.Find("object_" + entry.Value.id);
			Destroy(obj);
		}
	}
}
