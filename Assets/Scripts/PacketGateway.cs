using UnityEngine;
using System;
using System.Collections;
using SocketIO;
using Mutsuki;

public class PacketGateway {
	private SocketIOComponent socket;

	public delegate void OnResponseDelegate(BasePacket packet);

	public OnResponseDelegate onResponseDelegate;

	public PacketGateway(SocketIOComponent socket) {
		this.socket = socket;

		// base socket handler
		socket.On("open", onSocketOpen);
		socket.On("error", onSocketError);
		socket.On("close", onSocketClose);
		
		// kisaragi socket handler;
		foreach (var entry in BasePacket.commands) {
			socket.On (entry.Value, onResponse);
		}
	}

	// client to server request
	public void request(BasePacket packet) {
		Debug.Log ("request " + packet.command () + ": " + packet.toJson ());
		socket.Emit (packet.command (), packet.toJson ());
	}

	// server to client response
	public void onResponse(SocketIOEvent e) {
		var packet = PacketFactory.createFromJson (e.data);
		Debug.Log ("response " + packet.command() + ": " + packet.toJson ());
		onResponseDelegate (packet);
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
