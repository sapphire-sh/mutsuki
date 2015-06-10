using UnityEngine;
using System.Collections.Generic;
using Mutsuki;

public class Map : MonoBehaviour {
	public GameObject obstacle;
	public GameObject stairsUp;
	public GameObject stairsDown;
	
	public static int width;
	public static int height;

	public static int zoneId;
	
	private TileCode[,] data;

	public void SetUp (int _width, int _height, int _zoneId, TileCode[,] data) {
		width = _width;
		height = _height;
		zoneId = _zoneId;
		
		this.data = data;

		var plane = GameObject.CreatePrimitive (PrimitiveType.Plane);
		var meshRenderer = plane.GetComponent<MeshRenderer> ();
		meshRenderer.material = (Material)Resources.Load ("Materials/Plane");
		plane.transform.parent = this.transform;
		plane.transform.localScale = new Vector3 (width / 10.0f, 1.0f, height / 10.0f);
		plane.transform.Translate(new Vector3(width / 2.0f, 0.0f, height / 2.0f));
		for (int i = 0; i < width; ++i) {
			for (int j = 0; j < height; ++j) {
				switch(this.data[i, j]) {
				case TileCode.Empty:
					break;
				case TileCode.Obstacle: {
					GameObject instance = (GameObject)Instantiate(obstacle, new Vector3(i + 0.5f, 0.5f, j + 0.5f), Quaternion.identity);
					instance.transform.parent = plane.transform;
					break;
				}
				case TileCode.FloorUp:
					break;
				case TileCode.FloorDown:
					break;
				case TileCode.FloorLeft:
					break;
				case TileCode.FloorRight:
					break;
				case TileCode.FloorTop: {
					GameObject instance = (GameObject)Instantiate(stairsUp, new Vector3(i + 0.5f, 0.1f, j + 0.5f), Quaternion.identity);
					instance.transform.parent = plane.transform;
					break;
				}
				case TileCode.FloorBottom: {
					GameObject instance = (GameObject)Instantiate(stairsDown, new Vector3(i + 0.5f, 0.1f, j + 0.5f), Quaternion.identity);
					instance.transform.parent = plane.transform;
					break;
				}
				case TileCode.LevelStart:
					break;
				case TileCode.LevelGoal:
					break;
				}
			}
		}
	}

	public bool IsTileCode(int x, int y, TileCode tileCode) {
		return (data [x, y] == tileCode);
	}

	public void RequestJumpZone() {
		var packet = PacketFactory.requestJumpZone ();
		Main.request (packet);

/*		for (int i = 0; i < gameObject.transform.childCount; ++i) {
			var child = gameObject.transform.GetChild (i);
			GameObject.Destroy (child.gameObject);
		}*/
	}
}
