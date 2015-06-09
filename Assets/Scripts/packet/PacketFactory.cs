using System.Collections.Generic;

namespace Mutsuki {
	public class PacketFactory : BasePacketFactory {
		public static PingPacket ping() {
			var packet = new PingPacket ();
			packet.timestamp = 0;
			return packet;
		}

		public static EchoPacket echo(JSONObject data) {
			var packet = new EchoPacket ();
			packet.data = data;
			return packet;
		}

		public static EchoAllPacket echoAll(JSONObject data) {
			var packet = new EchoAllPacket ();
			packet.data = data;
			return packet;
		}

		public static RequestMovePacket requestMove(int movableId, int x, int y) {
			var packet = new RequestMovePacket ();
			packet.movableId = movableId;
			packet.x = x;
			packet.y = y;
			return packet;
		}

		public static MoveNotifyPacket moveNotify(int movableId, int x, int y) {
			var packet = new MoveNotifyPacket ();
			packet.movableId = movableId;
			packet.x = x;
			packet.y = y;
			return packet;
		}

		public static RequestAttackPacket requestAttack(int movableId) {
			var packet = new RequestAttackPacket ();
			packet.movableId = movableId;
			return packet;
		}

		public static RequestEntityStatusPacket requestEntityStatus(int movableId) {
			var packet = new RequestEntityStatusPacket ();
			packet.movableId = movableId;
			return packet;
		}

		public static ResponseEntityStatusPacket responseEntityStatus(int x, int y, int zoneId, Category category, int hp) {
			var packet = new ResponseEntityStatusPacket ();
			packet.x = x;
			packet.y = y;
			packet.zoneId = zoneId;
			packet.category = category;
			packet.hp = hp;
			return packet;
		}

		public static AttackNotifyPacket attackNotify(int attackerId, int attackedId, int damage) {
			var packet = new AttackNotifyPacket ();
			packet.attackerMovableId = attackerId;
			packet.attackedMovableId = attackedId;
			packet.damage = damage;
			return packet;
		}

		public static NewObjectPacket newObject(int movableId, Category category, int x, int y, int zoneId) {
			var packet = new NewObjectPacket ();
			packet.movableId = movableId;
			packet.category = category;
			packet.x = x;
			packet.y = y;
			packet.zoneId = zoneId;
			return packet;
		}

		public static RemoveObjectPacket removeObject(int movableId) {
			var packet = new RemoveObjectPacket ();
			packet.movableId = movableId;
			return packet;
		}

		public static LoginPacket login(int movableId, int x, int y, int zoneId, int width, int height) {
			var packet = new LoginPacket ();
			packet.movableId = movableId;
			packet.x = x;
			packet.y = y;
			packet.zoneId = zoneId;
			packet.width = width;
			packet.height = height;
			return packet;
		}

		public static RequestMapPacket requestMap(int zoneId) {
			var packet = new RequestMapPacket ();
			packet.zoneId = zoneId;
			return packet;
		}

		public static ResponseMapPacket responseMap(TileCode[,] data, int width, int height, int zoneId) {
			var packet = new ResponseMapPacket ();
			packet.data = data;
			packet.width = width;
			packet.height = height;
			packet.zoneId = zoneId;
			return packet;
		}

		public static RequestJumpZonePacket requestJumpZone() {
			var packet = new RequestJumpZonePacket ();
			return packet;
		}

		public static GameRestartPacket gameRestart() {
			var packet = new GameRestartPacket ();
			return packet;
		}
	}
}

