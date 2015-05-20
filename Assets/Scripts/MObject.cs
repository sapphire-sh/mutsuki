using UnityEngine;

namespace Mutsuki {
	public class MObject : MonoBehaviour {
		public static MObject CreateComponent(GameObject gameObject, string category, int id, int x, int y) {
			MObject mObject = gameObject.AddComponent<MObject> ();
			mObject.category = category;
			mObject.id = id;
			mObject.x = x;
			mObject.y = y;

			mObject.gameObject = GameObject.CreatePrimitive (PrimitiveType.Sphere);
			mObject.gameObject.transform.position = new Vector3 (mObject.x + 0.5f, 0.0f, mObject.y + 0.5f);

			return mObject;
		}

		public static MObject CreateComponent(GameObject gameObject, JSONObject jsonObject) {
			string category = jsonObject.GetField ("category").str;
			int id = (int)jsonObject.GetField ("id").n;
			int x = (int)jsonObject.GetField ("pos").list [0].n;
			int y = (int)jsonObject.GetField ("pos").list [1].n;

			MObject mObject = CreateComponent (gameObject, category, id, x, y);

			return mObject;
		}

		public void updatePos (int _x, int _y) {
			x = _x;
			y = _y;
		}

		void Update() {
			gameObject.transform.position = new Vector3 (x + 0.5f, 0.0f, y + 0.5f);
		}

		public string category;
		public int id;
		public int x;
		public int y;

		public GameObject gameObject;
	}

	public class MMap : MonoBehaviour {
		public static MMap CreateComponent(GameObject gameObject, JSONObject jsonObject) {
			MMap mMap = gameObject.AddComponent<MMap> ();
			mMap.width = (int)jsonObject.GetField("width").n;
			mMap.height = (int)jsonObject.GetField("height").n;

			mMap.data = new int[mMap.width, mMap.height];
			JSONObject map = jsonObject.GetField ("data");
			for (int i = 0; i < map.Count; ++i) {
				JSONObject row = map.list [i];
				for (int j = 0; j < row.Count; ++j) {
					int cell = (int)row.list [j].n;
					mMap.data[i, j] = cell;
				}
			}

			mMap.CreateMap ();

			return mMap;
		}

		public int width;
		public int height;

		public int[,] data;

		public void CreateMap() {
			GameObject plane = GameObject.CreatePrimitive (PrimitiveType.Plane);
//			plane.transform.localScale = new Vector3 (width, 1, height);
			plane.transform.Translate(new Vector3(width / 2.0f, 0.0f, height / 2.0f));

			for (int i = 0; i < width; ++i) {
				for (int j = 0; j < height; ++j) {
					switch(data[j, i]) {
					case 0:
//						GameObject obstacle = GameObject.CreatePrimitive (PrimitiveType.Cube);
						break;
					case 1:
						GameObject obstacle = GameObject.CreatePrimitive (PrimitiveType.Cube);
						obstacle.transform.position = new Vector3(0.5f, 0.0f, 0.5f);
						obstacle.transform.Translate(new Vector3(i, 0.0f, j));
						break;
					}
				}
			}
		}
	}
}
