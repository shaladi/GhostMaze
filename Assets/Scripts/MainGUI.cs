using UnityEngine;
using System.Collections;

public class MainGUI : MonoBehaviour {
	public Vector2 pos = new Vector2(20,40);
	public Vector2 size = new Vector2(60,20);
	public Texture2D emptyTex;
	public Texture2D fullTex;
	public float barDisplay = 100;
	// Use this for initialization
	void OnGUI () {
		emptyTex.SetPixels (Color.red);
		fullTex.SetPixels (Color.green);
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
