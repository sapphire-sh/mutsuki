using UnityEngine;
using System;
using System.Collections;
using SocketIO;
using Mutsuki;

public class PacketGateway {
	// socket io 사용시 주의사항
	// 씬이 시작하자마자 통신 시도시 에러 발생 (예 : 처음에 존재하는 객체의 Start()에서 호출)
	private SocketIOComponent socket;

	public delegate void OnPingDelegate(PingPacket packet);
	public delegate void OnEchoDelegate(EchoPacket packet);
	public delegate void OnEchoAllDelegate(EchoAllPacket packet);
	public delegate void OnConnectDelegate(ConnectPacket packet);
	public delegate void OnDisconnectDelegate(DisconnectPacket packet);
	public delegate void OnMoveNotifyDelegate (MoveNotifyPacket packet);
	public delegate void OnResponseEntityStatusDelegate (ResponseEntityStatusPacket packet);
	public delegate void OnAttackNotifyDelegate(AttackNotifyPacket packet);
	public delegate void OnNewObjectDelegate (NewObjectPacket packet);
	public delegate void OnRemoveObjectDelegate (RemoveObjectPacket packet);
	public delegate void OnLoginDelegate (LoginPacket packet);
	public delegate void OnResponseMapDelegate (ResponseMapPacket packet);

	public OnPingDelegate onPingDelegate;
	public OnEchoDelegate onEchoDelegate;
	public OnEchoAllDelegate onEchoAllDelegate;
	public OnConnectDelegate onConnectDelegate;
	public OnDisconnectDelegate onDisconnectDelegate;
	public OnMoveNotifyDelegate onMoveNotifyDelegate;
	public OnResponseEntityStatusDelegate onResponseEntityStatusDelegate;
	public OnAttackNotifyDelegate onAttackNotifyDelegate;
	public OnNewObjectDelegate onNewObjectDelegate;
	public OnRemoveObjectDelegate onRemoveObjectDelegate;
	public OnLoginDelegate onLoginDelegate;
	public OnResponseMapDelegate onResponseMapDelegate;

	public PacketGateway(SocketIOComponent socket) {
		this.socket = socket;

		// base socket handler
		socket.On("open", onSocketOpen);
		socket.On("error", onSocketError);
		socket.On("close", onSocketClose);
		
		// kisaragi socket handler;
		socket.On ("ping", onPing);
		socket.On ("echo", onEcho);
		socket.On ("echoAll", onEchoAll);
		socket.On ("connect", onConnect);
		socket.On ("disconnect", onDisconnect);
//		socket.On ("c2s_requestMove", onRequestMove);
		socket.On ("s2c_moveNotify", onMoveNotify);
//		socket.On ("c2s_requestAttack", onRequestAttack);
//		socket.On ("c2s_requestEntityStatus", onRequestEntityStatus);
		socket.On ("s2c_responseEntityStatus", onResponseEntityStatus);
		socket.On ("s2c_attackNotify", onAttackNotify);
		socket.On ("s2c_newObject", onNewObject);
		socket.On ("s2c_removeObject", onRemoveObject);
		socket.On ("s2c_login", onLogin);
//		socket.On ("c2s_requestMap", onRequestMap);
		socket.On ("s2c_requestMap", onResponseMap);
//		socket.On ("c2s_requestJumpZone", onRequestJumpZone);
//		socket.On ("c2s_gameRestart", onGameRestart);
	}

	// client to server request
	public void login() {
		JSONObject payload = new JSONObject (JSONObject.Type.OBJECT);
		payload.AddField ("packetType", (int)PacketType.Login);
		Debug.Log ("login : " + payload);
		socket.Emit ("login", payload);
	}

	public void requestMap() {
		JSONObject payload = new JSONObject (JSONObject.Type.OBJECT);
		payload.AddField ("packetType", (int)PacketType.RequestMap);
		payload.AddField ("zoneId", 0);
		Debug.Log ("requestMap : " + payload);
		socket.Emit ("c2s_requestMap", payload);
	}

	public void requestMove(int x, int y) {
		Debug.Log ("x: " + x + " y: " + y);
		JSONObject payload = new JSONObject (JSONObject.Type.OBJECT);
		payload.AddField ("packetType", (int)PacketType.RequestMove);
		payload.AddField ("x", x);
		payload.AddField ("y", y);
		Debug.Log ("requestMove : " + payload);
		socket.Emit ("c2s_requestMove", payload);
	}

	// server to client response
	public void onPing(SocketIOEvent e) {
		Debug.Log ("onPing " + e.data);
		var packet = PacketFactory.createFromJson (e.data);
		onPingDelegate ((PingPacket)packet);
	}

	public void onEcho(SocketIOEvent e) {
		Debug.Log ("onEcho " + e.data);
		var packet = PacketFactory.createFromJson (e.data);
		onEchoDelegate ((EchoPacket)packet);
	}

	public void onEchoAll(SocketIOEvent e) {
		Debug.Log ("onEchoAll " + e.data);
		var packet = PacketFactory.createFromJson (e.data);
		onEchoAllDelegate ((EchoAllPacket)packet);
	}

	public void onConnect(SocketIOEvent e) {
		Debug.Log ("onConnect " + e.data);
		var packet = PacketFactory.createFromJson (e.data);
		onConnectDelegate ((ConnectPacket)packet);
	}

	public void onDisconnect(SocketIOEvent e) {
		Debug.Log ("onDisconnect " + e.data);
		var packet = PacketFactory.createFromJson (e.data);
		onDisconnectDelegate ((DisconnectPacket)packet);
	}
	
	public void onMoveNotify(SocketIOEvent e) {
//		Debug.Log ("onMoveNotify " + e.data);
		var packet = PacketFactory.createFromJson (e.data);
		onMoveNotifyDelegate ((MoveNotifyPacket)packet);
	}

	public void onResponseEntityStatus(SocketIOEvent e) {
		Debug.Log ("onResponseEntityStatus " + e.data);
		var packet = PacketFactory.createFromJson (e.data);
		onResponseEntityStatusDelegate ((ResponseEntityStatusPacket)packet);
	}

	public void onAttackNotify(SocketIOEvent e) {
		Debug.Log ("onAttackNotify " + e.data);
		var packet = PacketFactory.createFromJson (e.data);
		onAttackNotifyDelegate ((AttackNotifyPacket)packet);
	}
	
	public void onNewObject(SocketIOEvent e) {
		Debug.Log ("onNewObject " + e.data);
		var packet = PacketFactory.createFromJson (e.data);
		onNewObjectDelegate ((NewObjectPacket)packet);
	}
	
	public void onRemoveObject(SocketIOEvent e) {
		Debug.Log ("onRemoveObject " + e.data);
		var packet = PacketFactory.createFromJson (e.data);
		onRemoveObjectDelegate ((RemoveObjectPacket)packet);
	}

	public void onLogin(SocketIOEvent e) {
		Debug.Log ("onLogin " + e.data);
		var packet = PacketFactory.createFromJson (e.data);
		onLoginDelegate ((LoginPacket)packet);
	}
	
	public void onResponseMap(SocketIOEvent e) {
		Debug.Log ("onResponseMap " + e.data);
		var packet = PacketFactory.createFromJson (e.data);
		onResponseMapDelegate ((ResponseMapPacket)packet);
	}

	// socket base handler
	public void onSocketOpen(SocketIOEvent e) {
		Debug.Log ("onOpen " + e.data);
	}
	
	public void onSocketError(SocketIOEvent e) {
		Debug.LogError ("onError " + e.data);
	}
	
	public void onSocketClose(SocketIOEvent e) {
		Debug.Log ("onClose " + e.data);
	}
}
