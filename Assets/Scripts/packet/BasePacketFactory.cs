namespace Mutsuki {
	public class BasePacketFactory {
		public static string toCommand(PacketType packetType) {
			string command;
			if (BasePacket.commands.TryGetValue (packetType, out command)) {
				return command;
			}
			else {
				return null;
			}
		}

		public static BasePacket createFromJson(JSONObject data) {
			var packetType = (PacketType)data.GetField("packetType").n;
			var packet = PacketFactory.create (packetType);
			if (packet == null) {
				return PacketFactory.create (PacketType.Disconnect);
			}
			else {
				packet.loadJson (data);
				return packet;
			}
		}

		public static BasePacket create(PacketType packetType) {
			switch(packetType) {
			case PacketType.Ping:
				return new PingPacket();
			case PacketType.Echo:
				return new EchoPacket();
			case PacketType.EchoAll:
				return new EchoAllPacket();
			case PacketType.Connect:
				return new ConnectPacket();
			case PacketType.Disconnect:
				return new DisconnectPacket();
			case PacketType.RequestMove:
				return new RequestMovePacket();
			case PacketType.MoveNotify:
				return new MoveNotifyPacket();
			case PacketType.RequestAttack:
				return new RequestAttackPacket();
			case PacketType.AttackNotify:
				return new AttackNotifyPacket();
			case PacketType.NewObject:
				return new NewObjectPacket();
			case PacketType.RemoveObject:
				return new RemoveObjectPacket();
			case PacketType.Login:
				return new LoginPacket();
			case PacketType.RequestMap:
				return new RequestMapPacket();
			case PacketType.ResponseMap:
				return new ResponseMapPacket();
			case PacketType.RequestJumpZone:
				return new RequestJumpZonePacket();
			case PacketType.GameRestart:
				return new GameRestartPacket();
			default:
				return null;
			}
		}
	}
}

