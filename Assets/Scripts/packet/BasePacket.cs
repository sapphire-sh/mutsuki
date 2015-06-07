using System.Collections.Generic;

namespace Mutsuki {
	public abstract class BasePacket {
		private PacketType packetType;
		
		public static Dictionary<PacketType, string> commands = new Dictionary<PacketType, string> {
			{ PacketType.Ping, "ping" },
			{ PacketType.Echo, "echo" },
			{ PacketType.EchoAll, "echoAll" },
			{ PacketType.Connect, "connect" },
			{ PacketType.Disconnect, "disconnect" },
			{ PacketType.RequestMove, "c2s_requestMove" },
			{ PacketType.MoveNotify, "s2c_moveNotify" },
			{ PacketType.RequestAttack, "c2s_requestAttack" },
			{ PacketType.RequestEntityStatus, "c2s_requestEntityStatus" },
			{ PacketType.ResponseEntityStatus, "s2c_responseEntityStatus" },
			{ PacketType.AttackNotify, "s2c_attackNotify" },
			{ PacketType.NewObject, "s2c_newObject" },
			{ PacketType.RemoveObject, "s2c_removeObject" },
			{ PacketType.Login, "s2c_login" },
			{ PacketType.RequestMap, "c2s_requestMap" },
			{ PacketType.ResponseMap, "s2c_requestMap" },
			{ PacketType.RequestJumpZone, "c2s_requestJumpZone" },
			{ PacketType.GameRestart, "c2s_gameRestart"},
		};

		public BasePacket(PacketType packetType) {
			this.packetType = packetType;
		}

		public string command() {
			string command;
			if (commands.TryGetValue (packetType, out command)) {
				return command;
			}
			else {
				return null;
			}
		}
		internal abstract JSONObject _generateJson ();
		public JSONObject toJson() {
			JSONObject data = this._generateJson();
			data.AddField("packetType", (int)this.packetType);
			return data;
		}
		public abstract void loadJson (JSONObject data);
	}
}
