using UnityEngine;
using System.Collections;
using SocketIO;

public class PacketGateway {
	// socket io 사용시 주의사항
	// 씬이 시작하자마자 통신 시도시 에러 발생 (예 : 처음에 존재하는 객체의 Start()에서 호출)

	private SocketIOComponent socket;

	public PacketGateway(SocketIOComponent socket) 
	{
		this.socket = socket;

		// base socket handler
		socket.On("open", onSocketOpen);
		socket.On("error", onSocketError);
		socket.On("close", onSocketClose);
		
		// development socket handler
		socket.On ("ping", onPing);
		socket.On ("echo", onEcho);
	}

	public void ping() {
		JSONObject payload = new JSONObject(JSONObject.Type.OBJECT);
		payload.AddField ("timestamp", (int)(Time.time * 1000));
		socket.Emit ("ping", payload);
	}
	
	public void onPing(SocketIOEvent e) 
	{
		int prev = (int)e.data ["timestamp"].n;
		int now = (int)(Time.time * 1000);
		int diff = now - prev;
		string msg = string.Format ("[SVR] Ping(now={0}) = {1}ms", now, diff);
		Debug.Log (msg);
	}
	
	public void echoDemo() 
	{
		JSONObject payload = new JSONObject(JSONObject.Type.OBJECT);
		payload.AddField ("foo", "bar");
		payload.AddField ("timestamp", (int)(Time.time * 1000));
		socket.Emit ("echo", payload);
	}
	
	public void onEcho(SocketIOEvent e)
	{
		Debug.Log ("[SVR] Echo = " + e.data);
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
