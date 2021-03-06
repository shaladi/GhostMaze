﻿using UnityEngine;
using System.Collections;

public class PointLightMovement : MonoBehaviour {

	public GameObject player;
	public Vector3 offset = new Vector3(0,0,-2);
	public float dampTime = 0.1f;
	private Vector3 velocity = Vector3.zero;
	
	// Use this for initialization
	void Start () {
		//offset = transform.position; 
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 curPosition = transform.position;
		Vector3 nextPosition = player.transform.position + offset;
		transform.position = Vector3.SmoothDamp (curPosition, nextPosition, ref velocity, dampTime);
	}
}
