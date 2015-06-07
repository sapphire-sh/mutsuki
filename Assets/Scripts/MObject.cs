using UnityEngine;
using Mutsuki;

public class MObject : MonoBehaviour {
	public Category category;
	public int id;
	public int x;
	public int y;

	private float prevX;
	private float prevY;

	public void SetUp(NewObjectPacket packet) {
		category = packet.category;
		id = packet.movableId;
		prevX = packet.x;
		prevY = packet.y;
		x = (int)prevX;
		y = (int)prevY;

		transform.position = new Vector3 (x + 0.5f, 0.5f, y + 0.5f);
	}

	public void updatePos (int _x, int _y) {
		x = _x;
		y = _y;
	}

	void Update() {
/*		Vector3 direction = new Vector3 (x - prevX, 0.0f, y - prevY);
		direction *= Time.deltaTime;
		Debug.Log (direction);
		transform.Translate (direction);*/
		transform.position = new Vector3 (x + 0.5f, 0.5f, y + 0.5f);

		prevX = transform.position.x;
		prevY = transform.position.y;
	}
}
