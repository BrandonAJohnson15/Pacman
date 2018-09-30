using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class GameEntity : MonoBehaviour {
	[SerializeField]
	protected float speed = 4f;

	protected Vector2 CurVelocity;
	private Direction curDir;
	protected Direction nextDir;
	protected bool canChangeDir;
	protected Animator anim;

	protected bool IsInGhostHouse = false;

	protected GameManager gm;

	public Direction CurDir
	{
		get
		{
			return curDir;
		}

		set
		{
			curDir = value;
		}
	}

	public abstract void Move();

	public abstract void CheckCollisions();

	protected void ChangeDir(Direction dir)
	{
		switch (dir)
		{
			case Direction.LEFT:
				CurVelocity = Vector2.left * speed;
				anim.SetFloat("DirX", -1);
				anim.SetFloat("DirY", 0);
				CurDir = Direction.LEFT;
				nextDir = Direction.LEFT;

				break;

			case Direction.RIGHT:
				CurVelocity = Vector2.right * speed;
				anim.SetFloat("DirX", 1);
				anim.SetFloat("DirY", 0);
				CurDir = Direction.RIGHT;
				nextDir = Direction.RIGHT;

				break;


			case Direction.UP:
				CurVelocity = Vector2.up * speed;
				anim.SetFloat("DirY", -1);
				anim.SetFloat("DirX", 0);
				CurDir = Direction.UP;
				nextDir = Direction.UP;

				break;

			case Direction.DOWN:
				CurVelocity = Vector2.down * speed;
				anim.SetFloat("DirY", 1);
				anim.SetFloat("DirX", 0);
				CurDir = Direction.DOWN;
				nextDir = Direction.DOWN;

				break;
		}

	}

	// Update is called once per frame
	void Update()
	{
		Move();
		CheckWarps();
		CheckCollisions();
	}

	private void FixedUpdate()
	{
		this.transform.Translate(CurVelocity * Time.deltaTime);
	}

	protected Turn GetTurn()
	{
		if (gm.turns == null) return null;
		foreach (Turn turn in gm.turns)
		{
			if (gm.CheckSnapThreshold(turn.transform.position, this.transform.position))
			{
				return turn;
			}
		}

		return null;
	}

	protected void CheckWarps()
	{
		if (gm.warps == null) return;

		foreach (Warp warp in gm.warps)
		{
			if (gm.CheckSnapThreshold(warp.transform.position, this.transform.position) && warp.warpDir == CurDir)
			{
				this.transform.position = warp.destWarp.transform.position;
				return;
			}
		}
	}

	protected Direction GetOppositeDir(Direction dir)
	{
		switch (dir)
		{
			case Direction.LEFT:
				return Direction.RIGHT;
			case Direction.RIGHT:
				return Direction.LEFT;
			case Direction.UP:
				return Direction.DOWN;
			case Direction.DOWN:
				return Direction.UP;
		}
		return dir;
	}
}
