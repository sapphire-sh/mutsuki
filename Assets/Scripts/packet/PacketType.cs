namespace Mutsuki {
	public enum PacketType {
		Ping,
		Echo,
		EchoAll,
		Connect,
		Disconnect,
		RequestMove,
		MoveNotify,
		RequestAttack,
		RequestEntityStatus,
		ResponseEntityStatus,
		AttackNotify,
		NewObject,
		RemoveObject,
		Login,
		RequestMap,
		ResponseMap,
		RequestJumpZone,
		GameRestart,
	}
}
