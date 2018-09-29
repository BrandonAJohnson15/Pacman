using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Edible : MonoBehaviour {

	public int scoreAmount = 10;
	public bool active = true;

	public abstract void GetConsumed();
}
