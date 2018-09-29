using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public List<Turn> turns;
	public List<Warp> warps;

	[SerializeField]
	public float snapThreshold = 0.1f;

	public static GameManager gm;

	private void Start()
	{
		foreach (Turn turn in turns) turn.gameObject.GetComponent<SpriteRenderer>().enabled = false;
		foreach (Warp warp in warps) warp.gameObject.GetComponent<SpriteRenderer>().enabled = false;
		gm = this;
	}

	public bool CheckSnapThreshold(Transform t1, Transform t2)
	{
		return t1.position.x <= t2.position.x + snapThreshold && t1.position.x >= t2.position.x - snapThreshold
				&& t1.position.y <= t2.position.y + snapThreshold && t1.position.y >= t2.position.y - snapThreshold
		? true : false;
	}

}

public enum Direction { LEFT, RIGHT, UP, DOWN };
