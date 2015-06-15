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
	public enum Status {
		Stop,
		Move,
		Attack,
	}
	
	[HideInInspector]
	public Status status;
	
	[HideInInspector]
	public Animator animator;

	public void SetUp(NewObjectPacket packet) {
		category = packet.category;
		id = packet.movableId;
		pos = new Vector3 (packet.x, 0.0f, packet.y);

		if (category == Category.Player) {
			animator = GetComponent<Animator> ();
		}

		status = Status.Stop;

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
		if (category == Category.Player) {
			Debug.Log(status + " " + Time.time);
			switch(status) {
			case Status.Move:
				animator.SetBool("move", true);
				status = Status.Stop;
				break;
			case Status.Attack:
				animator.SetBool("attack", true);
				status = Status.Stop;
				break;
			case Status.Stop:
				animator.SetBool("move", false);
				animator.SetBool("attack", false);
				break;
			}
		}

		if (cooltime > 0.0f) {
			var deltaTime = Time.deltaTime;
			if (cooltime < deltaTime) {
				deltaTime = cooltime;
			}
			var diff = new Vector3 (0.0f, 0.0f, deltaTime);
			if (category == Category.Player) {
				diff /= Constants.PLAYER_COOLTIME_MOVE;
			} else {
				diff /= Constants.ENEMY_COOLTIME_MOVE;
			}
			transform.Translate (diff);
			cooltime -= deltaTime;

			if (cooltime > 0.0f) {
				status = Status.Move;
			}
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

	private static Vector3 axis = new Vector3(0.0f, 1.0f, 0.0f);

	public bool updateTarget(Vector3 targetPos) {
		if (cooltime > 0.0f) {
			return false;
		}
		else {
			var diff = this.pos - targetPos;
			float rotation = transform.localRotation.eulerAngles.y;
			if(diff.x > 0.99f) {
				rotation = 270.0f - rotation;
				this.pos += new Vector3(-1.0f, 0.0f, 0.0f);
			}
			else if(diff.x < -0.99f) {
				rotation = 90.0f - rotation;
				this.pos += new Vector3(1.0f, 0.0f, 0.0f);
			}
			else if(diff.z > 0.99f) {
				rotation = 180.0f - rotation;
				this.pos += new Vector3(0.0f, 0.0f, -1.0f);
			} 	
			else if(diff.z < -0.99f) {
				rotation = 0.0f - rotation;
				this.pos += new Vector3(0.0f, 0.0f, 1.0f);
			}
			transform.Rotate(axis, rotation);

			if(category == Category.Player) {
				cooltime = Constants.PLAYER_COOLTIME_MOVE;
			}
			else {
				cooltime = Constants.ENEMY_COOLTIME_MOVE;
			}
			return true;
		}
	}
}
