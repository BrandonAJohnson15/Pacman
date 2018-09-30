using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman : MonoBehaviour {

	[SerializeField]
	private float speed = 4f;
	private Vector2 CurVelocity;
	private Camera cam;
	float camHeight;
	float camWidth;

	// Use this for initialization
	void Start () {
		CurVelocity = Vector2.zero;
		cam = Camera.main;
		camHeight = 2f * cam.orthographicSize;
		camWidth = camHeight * cam.aspect;
		Debug.Log("camHeight: " + camHeight + "\ncamWidth: " + camWidth);
	}
	
	// Update is called once per frame
	void Update () {
		CheckInput();
		CheckPosition();
	}

	private void FixedUpdate()
	{	
		this.transform.Translate(CurVelocity*Time.deltaTime);
	}

	void CheckInput()
	{
		if (Input.GetKey(KeyCode.A))
		{
			CurVelocity = Vector2.left * speed;
		}
		else if (Input.GetKey(KeyCode.D))
		{
			CurVelocity = Vector2.right * speed;
		}
		else if (Input.GetKey(KeyCode.W))
		{
			CurVelocity = Vector2.up * speed;
		}
		else if (Input.GetKey(KeyCode.S))
		{
			CurVelocity = Vector2.down * speed;
		}
	}

	void CheckPosition()
	{

	}
}
