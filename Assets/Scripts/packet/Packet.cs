using System.Collections.Generic;

namespace Mutsuki {
	public class PingPacket : BasePacket {
		public long timestamp { get; set; }

		public PingPacket() : base(PacketType.Ping) {
			this.timestamp = 0;
		}

		internal override JSONObject _generateJson() {
			var data = new JSONObject ();
			data.AddField("timestamp", this.timestamp);
			return data;
		}
		public override void loadJson(JSONObject data) {
			this.timestamp = (long)data.GetField("timestamp").n;
		}
	}

	public class EchoPacket : BasePacket {
		public JSONObject data { get; set; }

		public EchoPacket() : base(PacketType.Echo) {
			this.data = null;
		}

		internal override JSONObject _generateJson() {
			var data = new JSONObject ();
			data.AddField ("data", this.data);
			return data;
		}
		public override void loadJson(JSONObject data) {
			// UNDONE
			this.data = data.GetField ("data");
		}
	}

	public class EchoAllPacket : BasePacket {
		public JSONObject data { get; set; }

		public EchoAllPacket() : base(PacketType.EchoAll) {
			this.data = null;
		}

		internal override JSONObject _generateJson() {
			var data = new JSONObject ();
			data.AddField ("data", this.data);
			return data;
		}
		public override void loadJson(JSONObject data) {
			// UNDONE
			this.data = data.GetField ("data");
		}
	}

	public class ConnectPacket : BasePacket {
		public ConnectPacket () : base(PacketType.Connect) {}

		internal override JSONObject _generateJson() {
			var data = new JSONObject ();
			return data;
		}
		public override void loadJson(JSONObject data) {}
	}

	public class DisconnectPacket : BasePacket {
		public DisconnectPacket () : base(PacketType.Disconnect) {}

		internal override JSONObject _generateJson() {
			var data = new JSONObject();
			return data;
		}
		public override void loadJson(JSONObject data) {}
	}

	public class RequestMovePacket : BasePacket {
		public int movableId { get; set; }
		public int x { get; set; }
		public int y { get; set; }

		public RequestMovePacket() : base(PacketType.RequestMove) {
			this.movableId = 0;
			this.x = 0;
			this.y = 0;
		}

		internal override JSONObject _generateJson() {
			var data = new JSONObject ();
			data.AddField ("movableId", this.movableId);
			data.AddField ("x", this.x);
			data.AddField ("y", this.y);
			return data;
		}
		public override void loadJson(JSONObject data) {
			this.movableId = (int)data.GetField ("movableId").n;
			this.x = (int)data.GetField ("x").n;
			this.y = (int)data.GetField ("y").n;
		}
	}

	public class MoveNotifyPacket : BasePacket {
		public int movableId { get; set; }
		public int x { get; set; }
		public int y { get; set; }
		
		public MoveNotifyPacket() : base(PacketType.MoveNotify) {
			this.movableId = 0;
			this.x = 0;
			this.y = 0;
		}

		internal override JSONObject _generateJson() {
			var data = new JSONObject ();
			data.AddField ("movableId", this.movableId);
			data.AddField ("x", this.x);
			data.AddField ("y", this.y);
			return data;
		}
		public override void loadJson(JSONObject data) {
			this.movableId = (int)data.GetField ("movableId").n;
			this.x = (int)data.GetField ("x").n;
			this.y = (int)data.GetField ("y").n;
		}
	}

	public class RequestAttackPacket : BasePacket {
		public int movableId { get; set; }

		public RequestAttackPacket() : base(PacketType.RequestAttack) {
			this.movableId = 0;
		}

		internal override JSONObject _generateJson() {
			var data = new JSONObject ();
			data.AddField ("movableId", this.movableId);
			return data;
		}
		public override void loadJson(JSONObject data) {
			this.movableId = (int)data.GetField ("movableId").n;
		}
	}

	public class RequestEntityStatusPacket : BasePacket {
		public int movableId { get; set; }

		public RequestEntityStatusPacket() : base(PacketType.RequestEntityStatus) {
			this.movableId = 0;
		}

		internal override JSONObject _generateJson() {
			var data = new JSONObject ();
			data.AddField ("movableId", this.movableId);
			return data;
		}
		public override void loadJson(JSONObject data) {
			this.movableId = (int)data.GetField ("movableId").n;
		}
	}

	public class ResponseEntityStatusPacket : BasePacket {
		public int x { get; set; }
		public int y { get; set; }
		public int zoneId { get; set; }
		public Category category { get; set; }
		public int hp { get; set; }

		public ResponseEntityStatusPacket() : base(PacketType.ResponseEntityStatus) {
			this.x = 0;
			this.y = 0;
			this.zoneId = 0;
			this.category = Category.None;
			this.hp = 0;
		}

		internal override JSONObject _generateJson() {
			var data = new JSONObject ();
			data.AddField ("x", this.x);
			data.AddField ("y", this.y);
			data.AddField ("zoneId", this.zoneId);
			data.AddField ("category", (int)this.category);
			data.AddField ("hp", this.hp);
			return data;
		}
		public override void loadJson(JSONObject data) {
			this.x = (int)data.GetField ("x").n;
			this.y = (int)data.GetField ("y").n;
			this.zoneId = (int)data.GetField ("zoneId").n;
			this.category = (Category)data.GetField ("category").n;
			this.hp = (int)data.GetField ("hp").n;
		}
	}

	public class AttackNotifyPacket : BasePacket {
		public int attackerMovableId { get; set; }
		public int attackedMovableId { get; set; }
		public int damage { get; set; }

		public AttackNotifyPacket() : base(PacketType.AttackNotify) {
			this.attackerMovableId = 0;
			this.attackedMovableId = 0;
			this.damage = 0;
		}

		internal override JSONObject _generateJson() {
			var data = new JSONObject ();
			data.AddField ("attackerMovableId", this.attackerMovableId);
			data.AddField ("attackedMovableId", this.attackedMovableId);
			data.AddField ("damage", this.damage);
			return data;
		}
		public override void loadJson(JSONObject data) {
			this.attackerMovableId = (int)data.GetField ("attackerMovableId").n;
			this.attackedMovableId = (int)data.GetField ("attackedMovableId").n;
			this.damage = (int)data.GetField ("damage").n;
		}
	}

	public class NewObjectPacket : BasePacket {
		public int movableId { get; set; }
		public Category category { get; set; }
		public int x { get; set; }
		public int y { get; set; }
		public int zoneId { get; set; }

		public NewObjectPacket() : base(PacketType.NewObject) {
			this.movableId = 0;
			this.category = Category.None;
			this.x = 0;
			this.y = 0;
			this.zoneId = 0;
		}

		internal override JSONObject _generateJson() {
			var data = new JSONObject ();
			data.AddField ("movableId", this.movableId);
			data.AddField ("category", (int)this.category);
			data.AddField ("x", this.x);
			data.AddField ("y", this.y);
			data.AddField ("zoneId", this.zoneId);
			return data;
		}
		public override void loadJson(JSONObject data) {
			this.movableId = (int)data.GetField ("movableId").n;
			// TODO
			this.category = (Category)data.GetField ("category").n;
			this.x = (int)data.GetField ("x").n;
			this.y = (int)data.GetField ("y").n;
			this.zoneId = (int)data.GetField ("zoneId").n;
		}
	}

	public class RemoveObjectPacket : BasePacket {
		public int movableId { get; set; }

		public RemoveObjectPacket () : base(PacketType.RemoveObject) {
			this.movableId = 0;
		}

		internal override JSONObject _generateJson() {
			var data = new JSONObject ();
			data.AddField ("movableId", this.movableId);
			return data;
		}
		public override void loadJson(JSONObject data) {
			this.movableId = (int)data.GetField ("movableId").n;
		}
	}

	public class LoginPacket : BasePacket {
		public int movableId { get; set; }
		public int x { get; set; }
		public int y { get; set; }
		public int zoneId { get; set; }
		public int width { get; set; }
		public int height { get; set; }

		public LoginPacket() : base(PacketType.Login) {
			this.movableId = 0;
			this.x = 0;
			this.y = 0;
			this.zoneId = 0;
			this.width = 0;
			this.height = 0;
		}

		internal override JSONObject _generateJson() {
			var data = new JSONObject ();
			data.AddField ("movableId", this.movableId);
			data.AddField ("x", this.x);
			data.AddField ("y", this.y);
			data.AddField ("zoneId", this.zoneId);
			data.AddField ("width", this.width);
			data.AddField ("height", this.height);
			return data;
		}
		public override void loadJson(JSONObject data) {
			this.movableId = (int)data.GetField ("movableId").n;
			this.x = (int)data.GetField ("x").n;
			this.y = (int)data.GetField ("y").n;
			this.zoneId = (int)data.GetField ("zoneId").n;
			this.width = (int)data.GetField ("width").n;
			this.height = (int)data.GetField ("height").n;
		}
	}

	public class RequestMapPacket : BasePacket {
		public int zoneId { get; set; }

		public RequestMapPacket() : base(PacketType.RequestMap) {
			this.zoneId = 0;
		}

		internal override JSONObject _generateJson() {
			var data = new JSONObject ();
			data.AddField ("zoneId", this.zoneId);
			return data;
		}
		public override void loadJson(JSONObject data) {
			this.zoneId = (int)data.GetField ("zoneId").n;
		}
	}

	public class ResponseMapPacket : BasePacket {
		public TileCode[,] data { get; set; }
		public int width { get; set; }
		public int height { get; set; }
		public int zoneId { get; set; }

		public ResponseMapPacket() : base(PacketType.ResponseMap) {
			this.data = new TileCode[0, 0];
			this.width = 0;
			this.height = 0;
			this.zoneId = 0;
		}

		internal override JSONObject _generateJson() {
			var data = new JSONObject ();
			var _data = new JSONObject ();
			for(int i = 0; i < this.height; ++i) {
				var _row = new JSONObject();
				for(int j = 0; j < this.width; ++j) {
					var cell = this.data[j, i];
					_row.Add((int)cell);
				}
				_data.Add(_row);
			}
			data.AddField ("data", _data);
			data.AddField ("width", this.width);
			data.AddField ("height", this.height);
			data.AddField ("zoneId", this.zoneId);
			return data;
		}
		public override void loadJson(JSONObject data) {
			this.width = (int)data.GetField ("width").n;
			this.height = (int)data.GetField ("height").n;
			this.zoneId = (int)data.GetField ("zoneId").n;
			this.data = new TileCode[this.width, this.height];

			var rows = data.GetField ("data");
			for (int i = 0; i < this.height; ++i) {
				var cols = rows [i];
				for (int j = 0; j < this.width; ++j) {
					var cell = cols [j];
					this.data[j, i] = (TileCode)cell.n;
				}
			}
		}
	}

	public class RequestJumpZonePacket : BasePacket {
		public RequestJumpZonePacket() : base(PacketType.RequestJumpZone) {}

		internal override JSONObject _generateJson() {
			var data = new JSONObject ();
			return data;
		}
		public override void loadJson(JSONObject data) {}
	}

	public class GameRestartPacket : BasePacket {
		public GameRestartPacket() : base(PacketType.GameRestart) {}

		internal override JSONObject _generateJson() {
			var data = new JSONObject ();
			return data;
		}
		public override void loadJson(JSONObject data) {}
	}
}
