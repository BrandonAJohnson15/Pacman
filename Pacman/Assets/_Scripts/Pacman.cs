using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman : GameEntity {

	public int score = 0;

	// Use this for initialization
	void Start () {
		CurVelocity = Vector2.zero;
		anim = this.GetComponent<Animator>();
		gm = GameManager.gm;
		ChangeDir(Direction.LEFT);
		canChangeDir = false;
	}
	
	public override void Move()
	{
		Turn t = GetTurn();
		canChangeDir = t != null ? true : false;
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
		{
			if (curDir == Direction.RIGHT)
			{
				ChangeDir(Direction.LEFT);
				return;
			}

			nextDir = Direction.LEFT;
			if (canChangeDir && this.transform.position == t.gameObject.transform.position && t.availableDirs.Contains(nextDir))
			{
				ChangeDir(Direction.LEFT);
				return;
			}
		}
		else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		{
			if (curDir == Direction.LEFT)
			{
				ChangeDir(Direction.RIGHT);
				return;
			}

			nextDir = Direction.RIGHT;
			if (canChangeDir && this.transform.position == t.gameObject.transform.position && t.availableDirs.Contains(nextDir))
			{
				ChangeDir(Direction.RIGHT);
				return;
			}
		}
		else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
		{
			if (curDir == Direction.DOWN)
			{
				ChangeDir(Direction.UP);
				return;
			}

			nextDir = Direction.UP;
			if (canChangeDir && this.transform.position == t.gameObject.transform.position && t.availableDirs.Contains(nextDir))
			{
				ChangeDir(Direction.UP);
				return;
			}
		}
		else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
		{
			if (curDir == Direction.UP)
			{
				ChangeDir(Direction.DOWN);
				return;
			}

			nextDir = Direction.DOWN;
			if (canChangeDir && this.transform.position == t.gameObject.transform.position && t.availableDirs.Contains(nextDir))
			{
				ChangeDir(Direction.DOWN);
				return;
			}
		}

		if (nextDir != curDir && canChangeDir && gm.CheckSnapThreshold(t.transform, this.transform) && t.availableDirs.Contains(nextDir))
		{
			this.transform.position = t.transform.position;
			ChangeDir(nextDir);
		}
	}

	public override void CheckCollisions()
	{
		foreach (Pellet p in gm.pellets)
		{
			if (gm.CheckSnapThreshold(p.gameObject.transform,this.transform) && p.active)
			{
				score += p.scoreAmount;
				p.GetConsumed();
			}
		}

		foreach (PowerPellet p in gm.powerPellets)
		{
			if (gm.CheckSnapThreshold(p.gameObject.transform, this.transform) && p.active)
			{
				score += p.scoreAmount;
				p.GetConsumed();
			}
		}
	}

}
