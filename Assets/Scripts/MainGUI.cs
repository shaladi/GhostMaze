﻿using UnityEngine;
using System.Collections;

public class MainGUI : MonoBehaviour {
	public Vector2 pos = new Vector2(20,40);
	public Vector2 size = new Vector2(60,20);
	public Texture2D emptyTex;
	public Texture2D fullTex;
	public float barDisplay = 100;
	// Use this for initialization
	void OnGUI () {
		Color[] red = new Color[1];
		red[0] = Color.red;
		Color[] green = new Color[1];
		green[0] = Color.green;
		emptyTex.SetPixels (red);
		fullTex.SetPixels (green);
		GUI.BeginGroup (new Rect (pos.x, pos.y, size.x, size.y));
			GUI.Box (new Rect (0, 0, size.x, size.y), emptyTex);
			GUI.BeginGroup(new Rect(0,0, size.x * barDisplay, size.y));
				GUI.Box(new Rect(0,0, size.x, size.y), fullTex);
			GUI.EndGroup();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
