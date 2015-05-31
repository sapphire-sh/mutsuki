using UnityEngine;

public class MObject : MonoBehaviour {
	public Category category;
	public int id;
	public int x;
	public int y;

	private float prevX;
	private float prevY;
	
	public enum Category {
		NONE,
		PLAYER,
		ENEMY
	}

	public void SetUp(JSONObject jsonObject) {
		category = GetCategory (jsonObject.GetField ("category").str);
		id = (int)jsonObject.GetField ("movableId").n;
		prevX = jsonObject.GetField ("x").n;
		prevY = jsonObject.GetField ("y").n;
		x = (int)prevX;
		y = (int)prevY;

		transform.position = new Vector3 (x + 0.5f, 0.5f, y + 0.5f);
	}

	public void updatePos (int _x, int _y) {
		x = _x;
		y = _y;
	}

	void Update() {
		Vector3 direction = new Vector3 (x - prevX, 0.0f, y - prevY);
		direction *= Time.deltaTime;
		Debug.Log (direction);
		transform.Translate (direction);

		prevX = transform.position.x;
		prevY = transform.position.y;
	}
	
	public static Category GetCategory(string category) {
		switch (category) {
		case "user":
			return Category.PLAYER;
		case "player":
			return Category.PLAYER;
		case "enemy":
			return Category.ENEMY;
		default:
			return Category.NONE;
		}
	}
}
