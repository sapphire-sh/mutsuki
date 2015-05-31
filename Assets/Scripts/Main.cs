using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SocketIO;

public class Main : MonoBehaviour {
	private static PacketGateway gateway;

	public static Map map;

	public static int playerId;

	public GameObject player;
	public GameObject enemy;
	
	public Dictionary<int, MObject> objectDict;

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

	public void Login(JSONObject jsonObject) {
		gateway.requestMap ();
		playerId = (int)jsonObject.GetField ("movableId").n;
	}

	public void ResponseMap(JSONObject jsonObject) {
		GameObject mapObject = GameObject.Find ("Map");
		map = mapObject.GetComponent<Map> ();
		map.SetUp (jsonObject);
	}

	public void NewObject(JSONObject jsonObject) {
		int id = (int)jsonObject.GetField ("movableId").n;
		if (!objectDict.ContainsKey (id)) {
			string name = jsonObject.GetField ("categoryName").str;
			int x = (int)jsonObject.GetField ("x").n;
			int y = (int)jsonObject.GetField ("y").n;
			
			GameObject gameObject = null;
			switch (MObject.GetCategory (name)) {
			case MObject.Category.PLAYER:
				gameObject = Instantiate (player);
				break;
			case MObject.Category.ENEMY:
				gameObject = Instantiate (enemy);
				break;
			}

			if (gameObject != null) {
				MObject mObject = gameObject.GetComponent<MObject> ();
				mObject.SetUp (jsonObject);
			
				objectDict.Add (id, mObject);

				if(id == playerId) {
					GameObject camera = GameObject.Find ("Main Camera");
					camera.transform.parent = gameObject.transform;
					camera.transform.localPosition = new Vector3(0.0f, 1.0f, -3.0f);
				}
			}
		}
	}

	public void RemoveObject(JSONObject jsonObject) {
		int id = (int)jsonObject.GetField ("movableId").n;
		MObject mObject = objectDict [id];
		Destroy (mObject);
		objectDict.Remove (id);
	}

	public void MoveNotify(JSONObject jsonObject) {
		int objectId = (int)jsonObject.GetField ("movableId").n;
		MObject mObject = objectDict[objectId];
		int x = (int)jsonObject.GetField ("x").n;
		int y = (int)jsonObject.GetField ("y").n;
		mObject.updatePos (x, y);
	}
}
