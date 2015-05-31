using System.Collections.Generic;

namespace Mutsuki {
	public class PingPacket : BasePacket {
		public long timestamp { get; }

		public PingPacket() : base(PacketType.Ping) {
			this.timestamp = 0;
		}

		public string command() {
			return "ping";
		}
		private JSONObject _generateJson() {
			var data = new JSONObject ();
			data.AddField("timestamp", this.timestamp);
			return data;
		}
		public void loadJson(var data) {
			this.timestamp = data.GetField("timestamp").n;
		}
	}

	public class EchoPacket : BasePacket {
		public var data { get; }

		public EchoPacket() : base(PacketType.Echo) {
			this.data = null;
		}

		public string command() {
			return "echo";
		}
		public JSONObject _generateJson() {
			var data = new JSONObject ();
			data.AddField ("data", this.data);
		}
		public void loadJson(var data) {
			// UNDONE
			this.data = data.GetField ("data").str;
		}
	}

	public class EchoAllPacket : BasePacket {
		public var data { get; }

		public EchoAllPacket() : base(PacketType.EchoAll) {
			this.data = null;
		}
		
		public string command() {
			return "echoAll";
		}
		public JSONObject _generateJson() {
			var data = new JSONObject ();
			data.AddField ("data", this.data);
		}
		public void loadJson(var data) {
			// UNDONE
			this.data = data.GetField ("data").str;
		}
	}

	public class ConnectPacket : BasePacket {
		public ConnectPacket () : base(PacketType.Connect);

		public string command() {
			return "connect";
		}
		public JSONObject _generateJson() {
			var data = new JSONObject ();
			return data;
		}
		public void loadJson(var data) {}
	}

	public class DisconnectPacket : BasePacket {
		public DisconnectPacket () : base(PacketType.Disconnect);

		public string command() {
			return "disconnect";
		}
		public JSONObject _generateJson() {
			var data = new JSONObject();
			return data;
		}
		public void loadJson(var data) {}
	}

	public class RequestMovePacket : BasePacket {
		public int movableId { get; }
		public int x { get; }
		public int y { get; }

		public RequestMovePacket() : base(PacketType.RequestMove) {
			this.movableId = 0;
			this.x = 0;
			this.y = 0;
		}

		public string command() {
			return "c2s_requestMove";
		}
		public JSONObject _generateJson() {
			var data = new JSONObject ();
			data.AddField ("movableId", this.movableId);
			data.AddField ("x", this.x);
			data.AddField ("y", this.y);
			return data;
		}
		public void loadJson(var data) {
			this.movableId = data.GetField ("movableId").n;
			this.x = data.GetField ("x").n;
			this.y = data.GetField ("y").n;
		}
	}

	public class MoveNotifyPacket : BasePacket {
		public int movableId { get; }
		public int x { get; }
		public int y { get; }
		
		public MoveNotifyPacket() : base(PacketType.MoveNotify) {
			this.movableId = 0;
			this.x = 0;
			this.y = 0;
		}
		
		public string command() {
			return "s2c_moveNotify";
		}
		public JSONObject _generateJson() {
			var data = new JSONObject ();
			data.AddField ("movableId", this.movableId);
			data.AddField ("x", this.x);
			data.AddField ("y", this.y);
			return data;
		}
		public void loadJson(var data) {
			this.movableId = data.GetField ("movableId").n;
			this.x = data.GetField ("x").n;
			this.y = data.GetField ("y").n;
		}
	}

	public class NewObjectPacket : BasePacket {
		public int movableId { get; }
		public Category category { get; }
		public int x { get; }
		public int y { get; }
		public int floor { get; }

		public NewObjectPacket() : base(PacketType.NewObject) {
			this.movableId = 0;
			this.category = null;
			this.x = 0;
			this.y = 0;
			this.floor = 0;
		}

		public string command() {
			return "s2c_newObject";
		}
		public JSONObject _generateJson() {
			var data = new JSONObject ();
			data.AddField ("movableId", this.movableId);
			data.AddField ("category", this.category);
			data.AddField ("x", this.x);
			data.AddField ("y", this.y);
			data.AddField ("floor", this.floor);
			return data;
		}
		public void loadJson(JSONObject data) {
			this.movableId = data.GetField ("movableId").n;
			// TODO
			this.category = data.GetField ("category").n;
			this.x = data.GetField ("x").n;
			this.y = data.GetField ("y").n;
			this.floor = data.GetField ("floor").n;
		}
	}

	public class RemoveObjectPacket : BasePacket {
		public int movableId { get; }

		public RemoveObjectPacket () : base(PacketType.RemoveObject) {
			this.movableId = 0;
		}

		public string command() {
			return "s2c_removeObject";
		}
		public JSONObject _generateJson() {
			var data = new JSONObject ();
			data.AddField ("movableId", this.movableId);
			return data;
		}
		public void loadJson(JSONObject data) {
			this.movableId = data.GetField ("movableId").n;
		}
	}

	public class LoginPacket : BasePacket {
		public int movableId { get; }
		public int x { get; }
		public int y { get; }
		public int floor { get; }
		public int width { get; }
		public int height { get; }

		public LoginPacket() : base(PacketType.Login) {
			this.movableId = 0;
			this.x = 0;
			this.y = 0;
			this.floor = 0;
			this.width = 0;
			this.height = 0;
		}

		public string command() {
			return "s2c_login";
		}
		public JSONObject _generateJson() {
			var data = new JSONObject ();
			data.AddField ("movableId", this.movableId);
			data.AddField ("x", this.x);
			data.AddField ("y", this.y);
			data.AddField ("floor", this.floor);
			data.AddField ("width", this.width);
			data.AddField ("height", this.height);
			return data;
		}
		public void loadJson(JSONObject data) {
			this.movableId = data.GetField ("movableId").n;
			this.x = data.GetField ("x").n;
			this.y = data.GetField ("y").n;
			this.floor = data.GetField ("floor").n;
			this.width = data.GetField ("width").n;
			this.height = data.GetField ("height").n;
		}
	}

	public class RequestMapPacket : BasePacket {
		public int floor { get; }

		public RequestMapPacket() : base(PacketType.RequestMap) {
			this.floor = 0;
		}

		public string command() {
			return "c2s_requestMap";
		}
		public JSONObject _generateJson() {
			var data = new JSONObject ();
			data.AddField ("floor", this.floor);
			return data;
		}
		public void loadJson(JSONObject data) {
			this.floor = data.GetField ("floor").n;
		}
	}

	public class ResponseMapPacket : BasePacket {
		public List<List<TileCode>> data { get; }
		public int width { get; }
		public int height { get; }
		public int floor { get; }

		public ResponseMapPacket() : base(PacketType.ResponseMap) {
			this.data = new List<List<TileCode>> ();
			this.width = 0;
			this.height = 0;
			this.floor = 0;
		}

		public string command() {
			return "s2c_requestMap";
		}
		public JSONObject _generateJson() {
			var data = new JSONObject ();
			// UNDONE
			data.AddField ("data", this.data);
			data.AddField ("width", this.width);
			data.AddField ("height", this.height);
			data.AddField ("floor", this.floor);
			return data;
		}
		public void loadJson(JSONObject data) {
			var rows = data.GetField ("data");
			for (int i = 0; i < rows.Count; ++i) {
				var cols = rows [i];
				this.data.Add (new List<TileCode> ());
				for (int j = 0; j < cols.Count; ++j) {
					var cell = cols [j];
					// UNDONE
					this.data [i].Add (cell.n);
				}
			}
			this.width = data.GetField ("width").n;
			this.height = data.GetField ("height").n;
			this.floor = data.GetField ("floor").n;
		}
	}
}
