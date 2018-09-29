using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPellet : Edible {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void GetConsumed()
	{
		active = false;
		this.gameObject.SetActive(false);
	}
}
