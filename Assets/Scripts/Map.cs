using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {
	public GameObject obstacle;
	
	private int width;
	private int height;
	
	private int[,] data;

	public void SetUp (JSONObject jsonObject) {
		this.width = (int)jsonObject.GetField("width").n;
		this.height = (int)jsonObject.GetField("height").n;
		
		this.data = new int[width, height];
		JSONObject data = jsonObject.GetField ("data");
		for (int i = 0; i < data.Count; ++i) {
			JSONObject row = data.list [i];
			for (int j = 0; j < row.Count; ++j) {
				int cell = (int)row.list [j].n;
				this.data[i, j] = cell;
			}
		}

		GameObject plane = GameObject.CreatePrimitive (PrimitiveType.Plane);
		plane.transform.parent = this.transform;
		plane.transform.Translate(new Vector3(width / 2.0f, 0.0f, height / 2.0f));
		for (int i = 0; i < this.width; ++i) {
			for (int j = 0; j < this.height; ++j) {
				switch(this.data[j, i]) {
				case 0:
					break;
				case 1:
					GameObject instance = (GameObject)Instantiate(obstacle, new Vector3(i + 0.5f, 0.5f, j + 0.5f), Quaternion.identity);
					instance.transform.parent = plane.transform;
					break;
				}
			}
		}
	}
}
