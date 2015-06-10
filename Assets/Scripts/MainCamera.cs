using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {
	MObject player;
	Vector3 playerPos;

	void Start () {
		transform.rotation = Quaternion.Euler (new Vector3 (35.0f, 315.0f, 0.0f));
		transform.localPosition = new Vector3 (5.0f, 5.0f, -5.0f);
	}

	void Update () {
		if (Main.objectDict.ContainsKey(Main.playerId)) {
			player = Main.objectDict [Main.playerId];
			playerPos = player.transform.position;
		}
		else {
			player = null;
			playerPos = new Vector3(Map.width / 2.0f, 0.0f, Map.height / 2.0f);
		}

		var cameraPos = transform.localPosition;
		var targetPos = playerPos + new Vector3 (5.0f, 5.0f, -5.0f);
		if (!cameraPos.Equals (targetPos)) {
			transform.Translate ((targetPos - cameraPos) * Time.deltaTime * 5.0f);
		}
	}
}
