using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public List<Turn> turns;
	public List<Warp> warps;
	public List<Pellet> pellets;
	public List<PowerPellet> powerPellets;
	public List<Ghost> ghosts;
	public Pacman pacman;

	public float sentinal = 10000000;

	[SerializeField]
	public float snapThreshold = 0.1f;

	public static GameManager gm;

	private void Awake()
	{
		gm = this;
	}

	private void Start()
	{
		foreach (Turn turn in turns) turn.gameObject.GetComponent<SpriteRenderer>().enabled = false;
		foreach (Warp warp in warps) warp.gameObject.GetComponent<SpriteRenderer>().enabled = false;
	}

	public bool CheckSnapThreshold(Vector3 pos1, Vector3 pos2)
	{
		return pos1.x <= pos2.x + snapThreshold && pos1.x >= pos2.x - snapThreshold
				&& pos1.y <= pos2.y + snapThreshold && pos1.y >= pos2.y - snapThreshold
		? true : false;
	}

	public Vector3 GetVector3Direction(Direction d)
	{
		switch (d)
		{
			case Direction.LEFT:
				return Vector3.left;
			case Direction.RIGHT:
				return Vector3.right;
			case Direction.UP:
				return Vector3.up;
			case Direction.DOWN:
				return Vector3.down;
		}
		return Vector3.zero;
	}

	public Direction GetOppositeDirection(Direction d)
	{
		switch (d)
		{
			case Direction.LEFT:
				return Direction.RIGHT;
			case Direction.RIGHT:
				return Direction.LEFT;
			case Direction.UP:
				return Direction.DOWN;
			case Direction.DOWN:
				return Direction.UP;
			default:
				return d;
		}
	}

	public Ghost GetGhost(string name)
	{
		foreach(Ghost g in ghosts)
		{
			if (g.gameObject.name == name)
				return g;
		}

		return ghosts[0];
	}

}

public enum Direction { LEFT, RIGHT, UP, DOWN };
