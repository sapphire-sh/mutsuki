using UnityEngine;
using Mutsuki;

public class MObject : MonoBehaviour {
	public Category category;
	public int id;

	public Vector3 pos;

	private int hp;
	private float cooltime;

	public Vector3 targetPos;

	public enum Status {
		Stop,
		Move,
	}

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

		transform.localPosition = (pos + new Vector3 (0.5f, 0.5f, 0.5f));
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
