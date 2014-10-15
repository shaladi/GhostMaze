using UnityEngine;
using System.Collections;

public class MainGUI : MonoBehaviour {
	public Vector2 pos = new Vector2(0,40);
	public Vector2 size = new Vector2(60,20);
	public Texture2D emptyTex;
	public Texture2D fullTex;
	public PlayerController playerController;
	// Use this for initialization
	void Start() {
//		emptyTex = new Texture2D(128, 128);
//		fullTex = new Texture2D(128, 128);
//		Color[] red = new Color[1];
//		red[0] = Color.red;
//		Color[] green = new Color[1];
//		green[0] = Color.green;
//		emptyTex.SetPixels (red);
//		fullTex.SetPixels (green);
		//renderer.material.mainTexture = texture;
	}
	void OnGUI () {
		GUI.BeginGroup (new Rect (pos.x, pos.y, size.x, size.y));
		GUI.backgroundColor = playerController.sanityOff ? Color.green : Color.red;
		GUI.Label (new Rect (0,0,200,30), "Sanity");
		GUI.HorizontalScrollbar(new Rect (50,3,200,20), 0, playerController.sanity,0, 100);
		GUI.Label (new Rect (0,20,200,30), "Remaining Beacons:");
		GUI.Label (new Rect (150, 20, 200, 30), (playerController.MAX_BEACON_COUNT - playerController.current_beacon_count).ToString ());
//		GUI.Box (new Rect (10, 10, size.x, size.y), emptyTex);
//		GUI.EndGroup();
//			GUI.BeginGroup(new Rect(0,0, size.x * barDisplay, size.y));
//			GUI.Box(new Rect(10,10, size.x, size.y), fullTex);

		GUI.EndGroup();


	// Make a background box
//		GUILayout.BeginArea(new Rect(5,5,400, Screen.width/2)); 
//		GUILayout.Button("Click me");
//		GUILayout.Button("Or me");
//		GUILayout.Box (emptyTex);
//		GUILayout.EndArea();
//		GUILayout.BeginHorizontal();
//	GUILayout.Label("Loader Menu");
//	
//	// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
//	if(GUI.Button(new Rect(20,40,80,20), "Level 1")) {
//		Application.LoadLevel(1);
//	}
//	
//	// Make the second button.
//	if(GUI.Button(new Rect(20,70,80,20), "Level 2")) {
//		Application.LoadLevel(2);
//	}
//		GUILayout.EndHorizontal();


}

// Update is called once per frame
void Update () {
}
}
