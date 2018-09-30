using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinky : Ghost {

	Pacman pacman;
	Turn curTurn;
	Vector3 targetPos;

	[SerializeField]
	private int offset = 4;

	private int pelletsTilExit = 0;

	// Use this for initialization
	void Start()
	{
		CurVelocity = Vector2.zero;
		anim = this.GetComponent<Animator>();
		gm = GameManager.gm;
		canChangeDir = false;
		pacman = gm.pacman;
		targetPos = pacman.transform.position + new Vector3(4,0);
		IsInGhostHouse = true;
	}

	public void updateTarget()
	{
		Vector3 targ = pacman.transform.position;
		switch (pacman.CurDir)
		{
			case Direction.LEFT:
				targ.x -= offset;
				break;
			case Direction.RIGHT:
				targ.x += offset;
				break;
			case Direction.UP:
				targ.y += offset;
				break;
			case Direction.DOWN:
				targ.y -= offset;
				break;
		}
		targetPos = targ;
	}


	public override void Move()
	{
		updateTarget();
		if (!IsInGhostHouse)
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
		else
		{
			if (pacman.numPelletsConsumed >= pelletsTilExit)
			{
				if (this.transform.position.x == 0)
				{
					ChangeDir(Direction.UP);
					if (gm.CheckSnapThreshold(this.transform.position, new Vector3(0, 4)))
					{
						this.transform.position = new Vector3(0, 4);
						ChangeDir(Direction.LEFT);
						IsInGhostHouse = false;
					}

				}
				else
				{
					if (this.transform.position.x < 0)
					{
						ChangeDir(Direction.RIGHT);
						if (gm.CheckSnapThreshold(this.transform.position, new Vector3(0, this.transform.position.y)))
						{
							this.transform.position = new Vector3(0, this.transform.position.y);
						}
					}
					else
					{
						ChangeDir(Direction.LEFT);
						if (gm.CheckSnapThreshold(this.transform.position, new Vector3(0, this.transform.position.y)))
						{
							this.transform.position = new Vector3(0, this.transform.position.y);
						}
					}
				}
				
			}
			else if (this.transform.position.y <= 0)
			{
				ChangeDir(Direction.UP);
			}
			else if (this.transform.position.y >= 2)
			{
				ChangeDir(Direction.DOWN);
			}
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
			if (gm.CheckSnapThreshold(cPos, targetPos))
				return -100000;

			cPos.x += xDiff;
			cPos.y += yDiff;
			foreach (Turn turn in gm.turns)
			{
				if (gm.CheckSnapThreshold(cPos, turn.transform.position) && turn != t)
				{
					return Vector2.Distance(turn.transform.position, targetPos);
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
