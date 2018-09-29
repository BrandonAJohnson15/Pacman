using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinky : GameEntity {

	Pacman pacman;
	Turn curTurn;

	// Use this for initialization
	void Start()
	{
		CurVelocity = Vector2.zero;
		anim = this.GetComponent<Animator>();
		gm = GameManager.gm;
		ChangeDir(Direction.LEFT);
		canChangeDir = false;
		pacman = gm.pacman;
	}

	public override void Move()
	{
		Turn t = GetTurn();
		canChangeDir = t != null ? true : false;

		if (canChangeDir && gm.CheckSnapThreshold(t.transform, this.transform) && curTurn != t)
		{
			this.transform.position = t.transform.position;
			nextDir = GetNextDirection(pacman, t);
			ChangeDir(nextDir);
			curTurn = t;
		}
	}

	public Direction GetNextDirection(GameEntity target, Turn t)
	{
		Direction dir;
		
		if (t.availableDirs.Count > 0)
		{
			while (true)
			{
				dir = t.availableDirs[Random.Range(0, t.availableDirs.Count)];
				if (dir != curDir && dir != GetOppositeDir(dir)) return dir;
			}
		}

		return curDir;
	}

	public override void CheckCollisions()
	{

	}
}
