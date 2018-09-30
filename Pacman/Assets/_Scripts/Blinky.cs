using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinky : Ghost {

	Pacman pacman;
	Turn curTurn;
	GameObject target;

	// Use this for initialization
	void Start()
	{
		CurVelocity = Vector2.zero;
		anim = this.GetComponent<Animator>();
		gm = GameManager.gm;
		ChangeDir(Direction.LEFT);
		canChangeDir = false;
		pacman = gm.pacman;
		target = pacman.gameObject;
	}

	public override void Move()
	{
		Turn t = GetTurn();
		canChangeDir = t != null ? true : false;

		if (canChangeDir && gm.CheckSnapThreshold(t.transform.position, this.transform.position) && curTurn != t)
		{
			this.transform.position = t.transform.position;
			nextDir = GetMove(t);
			ChangeDir(nextDir);
			curTurn = t;
		}
	}

	Direction GetMove(Turn t)
	{
		Direction direct = CurDir;
		float bestVal = gm.sentinal;
		foreach (Direction d in t.availableDirs)
		{
			if (d != GetOppositeDir(CurDir))
			{
				float val = GetNextTurnVal(d, t);
				if (val <= bestVal)
				{
					direct = d;
					bestVal = val;
				}
			}
		}
		return direct;
	}

	public Direction GetRandomMove(Turn t)
	{
		Direction dir;
		
		if (t.availableDirs.Count > 0)
		{
			while (true)
			{
				dir = t.availableDirs[Random.Range(0, t.availableDirs.Count)];
				if (dir != CurDir && dir != GetOppositeDir(dir))
					return dir;
			}
		}

		return CurDir;
	}

	public override void CheckCollisions()
	{

	}

	float GetNextTurnVal(Direction d, Turn t)
	{
		Vector3 v3Direction = gm.GetVector3Direction(d);
		float xDiff = v3Direction.x * 0.1f;
		float yDiff = v3Direction.y * 0.1f;
		Vector3 cPos = this.transform.position;

		bool outOfBounds = false;
		while (!outOfBounds)
		{
			// found target
			if (gm.CheckSnapThreshold(cPos, target.transform.position))
				return -100000;
			
			cPos.x += xDiff;
			cPos.y += yDiff;
			foreach (Turn turn in gm.turns)
			{
				if (gm.CheckSnapThreshold(cPos, turn.transform.position) && turn != t)
				{
					return Vector2.Distance(turn.transform.position, target.transform.position);
				}
			}

			if (cPos.x > 100 || cPos.x < -100 || cPos.y > 100 || cPos.y < -100)
			{
				outOfBounds = true;
			}
			
		}
		
		return gm.sentinal;
	}

}
