using UnityEngine;
using System.Collections;
using SocketIO;

public class PacketGateway {
	// socket io 사용시 주의사항
	// 씬이 시작하자마자 통신 시도시 에러 발생 (예 : 처음에 존재하는 객체의 Start()에서 호출)

	private SocketIOComponent socket;

	public delegate void OnLoginDelegate (JSONObject data);
	public delegate void OnResponseMapDelegate (JSONObject data);
	public delegate void OnNewObjectDelegate (JSONObject data);
	public delegate void OnRemoveObjectDelegate (JSONObject data);
	public delegate void OnMoveNotifyDelegate (JSONObject data);

	public OnLoginDelegate onLoginDelegate;
	public OnResponseMapDelegate onResponseMapDelegate;
	public OnNewObjectDelegate onNewObjectDelegate;
	public OnRemoveObjectDelegate onRemoveObjectDelegate;
	public OnMoveNotifyDelegate onMoveNotifyDelegate;

	public PacketGateway(SocketIOComponent socket) 
	{
		this.socket = socket;

		// base socket handler
		socket.On("open", onSocketOpen);
		socket.On("error", onSocketError);
		socket.On("close", onSocketClose);
		
		// kisaragi socket handler
		socket.On ("s2c_login", onLogin);
		socket.On ("s2c_responseMap", onResponseMap);
		socket.On ("s2c_newObject", onNewObject);
		socket.On ("s2c_removeObject", onRemoveObject);
		socket.On ("s2c_moveNotify", onMoveNotify);
	}

	// client to server request
	public void login() {
		socket.Emit ("login");
	}

	public void requestMap() {
		socket.Emit ("c2s_requestMap");
	}

	public void requestMove(int x, int y) {
		Debug.Log ("x: " + x + " y: " + y);
		JSONObject payload = new JSONObject (JSONObject.Type.OBJECT);
		payload.AddField ("x", x);
		payload.AddField ("y", y);
		socket.Emit ("c2s_requestMove", payload);
	}

	// server to client response
	public void onLogin(SocketIOEvent e) {
		Debug.Log ("[SVR] Login = " + e.data);
		onLoginDelegate (e.data);
	}
	
	public void onResponseMap(SocketIOEvent e) {
		Debug.Log ("[SVR] ResponseMap = " + e.data);
		onResponseMapDelegate (e.data);
	}

	public void onNewObject(SocketIOEvent e) {
		Debug.Log ("[SVR] NewObject = " + e.data);
		onNewObjectDelegate (e.data);
	}

	public void onRemoveObject(SocketIOEvent e) {
		Debug.Log ("[SVR] RemoveObject = " + e.data);
		onRemoveObjectDelegate (e.data);
	}

	public void onMoveNotify(SocketIOEvent e) {
		Debug.Log ("[SVR] MoveNotify = " + e.data);
		onMoveNotifyDelegate (e.data);
	}
	
	// socket base handler
	public void onSocketOpen(SocketIOEvent e)
	{
		Debug.Log("[SocketIO] Open received: " + e.name + " " + e.data);
	}
	
	public void onSocketError(SocketIOEvent e)
	{
		Debug.Log("[SocketIO] Error received: " + e.name + " " + e.data);
	}
	
	public void onSocketClose(SocketIOEvent e)
	{	
		Debug.Log("[SocketIO] Close received: " + e.name + " " + e.data);
	}
}
