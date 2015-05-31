namespace Mutsuki {
	public class BasePacket {
		private PacketType packetType;

		BasePacket(PacketType packetType) {
			this.packetType = packetType;
		}
		private virtual JSONObject _generateJson ();
		public JSONObject toJson() {
			JSONObject data = this._generateJson();
			data.AddField("packetType", this.packetType);
			return data;
		}
		public virtual void loadJson(JSONObject data);

		public virtual string command();
	}
}
