using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman : MonoBehaviour {
	
	[SerializeField]
	private float speed = 4f;
	
	private Vector2 CurVelocity;
	public Direction curDir;
	public Direction nextDir;
	bool canChangeDir;
	Animator anim;

	GameManager gm;

	// Use this for initialization
	void Start () {
		CurVelocity = Vector2.zero;
		anim = this.GetComponent<Animator>();
		gm = GameManager.gm;
		ChangeDir(Direction.LEFT);
		canChangeDir = false;
	}
	
	// Update is called once per frame
	void Update () {
		CheckInput();
		CheckWarps();
	}

	private void FixedUpdate()
	{	
		this.transform.Translate(CurVelocity*Time.deltaTime);
	}

	void CheckInput()
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

	void ChangeDir(Direction dir)
	{
		switch(dir)
		{
			case Direction.LEFT:
				CurVelocity = Vector2.left * speed;
				anim.SetFloat("DirX", -1);
				anim.SetFloat("DirY", 0);
				curDir = Direction.LEFT;
				nextDir = Direction.LEFT;

				break;

			case Direction.RIGHT:
				CurVelocity = Vector2.right * speed;
				anim.SetFloat("DirX", 1);
				anim.SetFloat("DirY", 0);
				curDir = Direction.RIGHT;
				nextDir = Direction.RIGHT;

				break;


			case Direction.UP:
				CurVelocity = Vector2.up * speed;
				anim.SetFloat("DirY", -1);
				anim.SetFloat("DirX", 0);
				curDir = Direction.UP;
				nextDir = Direction.UP;

				break;

			case Direction.DOWN:
				CurVelocity = Vector2.down * speed;
				anim.SetFloat("DirY", 1);
				anim.SetFloat("DirX", 0);
				curDir = Direction.DOWN;
				nextDir = Direction.DOWN;

				break;
		}
		
	}

	Turn GetTurn()
	{
		if (gm.turns == null) return null;
		foreach(Turn turn in gm.turns)
		{
			if (gm.CheckSnapThreshold(turn.transform, this.transform))
			{
				return turn;
			}
		}

		return null;
	}

	void CheckWarps()
	{
		if (gm.warps == null) return;

		foreach (Warp warp in gm.warps)
		{
			if (gm.CheckSnapThreshold(warp.transform, this.transform) && warp.warpDir == curDir)
			{
				this.transform.position = warp.destWarp.transform.position;
				return;
			}
		}
	}
}
