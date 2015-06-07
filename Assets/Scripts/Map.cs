using UnityEngine;
using System.Collections.Generic;
using Mutsuki;

public class Map : MonoBehaviour {
	public GameObject obstacle;
	
	private int width;
	private int height;
	
	private TileCode[,] data;

	public void SetUp (List<List<TileCode>> data) {
		this.width = data.Count;
		this.height = data [0].Count;
		
		this.data = new TileCode[width, height];
		for(int i = 0; i < data.Count; ++i) {
			var row = data[i];
			for(int j = 0; j < row.Count; ++j) {
				var cell = row[j];
				this.data[i, j] = cell;
			}
		}

		GameObject plane = GameObject.CreatePrimitive (PrimitiveType.Plane);
		plane.transform.parent = this.transform;
		plane.transform.Translate(new Vector3(width / 2.0f, 0.0f, height / 2.0f));
		for (int i = 0; i < this.width; ++i) {
			for (int j = 0; j < this.height; ++j) {
				switch(this.data[j, i]) {
				case TileCode.Empty:
					break;
				case TileCode.Obstacle:
					GameObject instance = (GameObject)Instantiate(obstacle, new Vector3(i + 0.5f, 0.5f, j + 0.5f), Quaternion.identity);
					instance.transform.parent = plane.transform;
					break;
				}
			}
		}
	}
}
