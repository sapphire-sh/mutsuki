namespace Mutsuki {
	public enum Role {
		Server,
		Client,
	}

	public enum Category {
		Enemy,
		Player,
		Item,
		None,
	}

	public enum TileCode {
		Empty,
		Obstacle,
		FloorUp,
		FloorDown,
		FloorLeft,
		FloorRight,
		FloorTop,
		FloorBottom,
		LevelStart,
		LevelGoal,
	}

	public class Constants {
		public const float ENEMY_COOLTIME_THINK = 0.5f;
		public const float ENEMY_COOLTIME_MOVE = 0.3f;
		public const int ENEMY_HP = 2;
		public const float PLAYER_COOLTIME_MOVE = 0.1f;
		public const int PLAYER_HP = 3;
		public const int PLAYER_ATTACK = 1;
		public const int PLAYER_ATTACK_RANGE = 5;
	}
}
