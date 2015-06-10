using UnityEngine;
using Mutsuki;

public class MObject : MonoBehaviour {
	[HideInInspector]
	public Category category;
	[HideInInspector]
	public int id;
	
	[HideInInspector]
	public Vector3 pos;
	
	[HideInInspector]
	public int hp;
	private float cooltime;
	
	[HideInInspector]
	public Vector3 targetPos;
	
	[HideInInspector]
	public enum Status {
		Stop,
		Move,
	}
	
	[HideInInspector]
	public Status status;

	public void SetUp(NewObjectPacket packet) {
		status = Status.Stop;

		category = packet.category;
		id = packet.movableId;
		pos = new Vector3 (packet.x, 0.0f, packet.y);

		if (category == Category.Player) {
			hp = Constants.PLAYER_HP;
		} else {
			hp = Constants.ENEMY_HP;
		}
		cooltime = 0.0f;

		if (category == Category.Player) {
			transform.localPosition = (pos + new Vector3 (0.5f, 0.0f, 0.5f));
		} else {
			transform.localPosition = (pos + new Vector3 (0.5f, 0.5f, 0.5f));
		}
	}

	void Update() {
		if (cooltime > 0.0f) {
			var deltaTime = Time.deltaTime;
			if (cooltime < deltaTime) {
				deltaTime = cooltime;
			}
			var diff = targetPos - pos;
			if (category == Category.Player) {
				diff *= (deltaTime / cooltime);
			} else {
				diff *= (deltaTime / cooltime);
			}
			pos += diff;
			transform.Translate (diff);
			cooltime -= deltaTime;
		} else {
			status = Status.Stop;
		}
	}

	private Vector3 nameplatePos;
	private string nameplateStr;

	void OnGUI() {
		nameplatePos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		nameplateStr = "object_" + id + "\n" + hp + "/";
		switch (category) {
		case Category.Player:
			nameplateStr += Constants.PLAYER_HP;
			break;
		case Category.Enemy:
			nameplateStr += Constants.ENEMY_HP;
			break;
		}
		GUI.Label(new Rect(nameplatePos.x + 10, (Screen.height - nameplatePos.y - 36), 100, 50), nameplateStr);
	}
	
	void OnMouseDown() {
		if (category == Category.Enemy) {
			var packet = PacketFactory.requestAttack(id);
			Main.request (packet);
		}
	}

	public bool updateTarget(Vector3 targetPos) {
		if (cooltime > 0.0f) {
			return false;
		} else {
			this.targetPos = targetPos;
			if(category == Category.Player) {
				cooltime = Constants.PLAYER_COOLTIME_MOVE;
			}
			else {
				cooltime = Constants.ENEMY_COOLTIME_MOVE;
			}
			status = Status.Move;
			return true;
		}
	}
}
