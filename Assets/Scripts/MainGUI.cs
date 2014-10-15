using UnityEngine;
using System.Collections;

public class MainGUI : MonoBehaviour {
	public Vector2 pos = new Vector2(10,40);
	public Vector2 size = new Vector2(300,300);
	public PlayerController playerController;
	// Use this for initialization
	void Start() {

	}
	void OnGUI () {
		GUI.BeginGroup (new Rect (pos.x, pos.y, size.x, size.y));
		GUI.backgroundColor = playerController.sanityOff ? Color.green : Color.red;
		GUI.Label (new Rect (0,0,200,30), "Sanity");
		GUI.HorizontalScrollbar(new Rect (50,3,200,20), 0, playerController.sanity,0, 100);
		GUI.Label (new Rect (0,20,200,30), "Remaining Beacons:");
		GUI.Label (new Rect (150, 20, 200, 30), (playerController.MAX_BEACON_COUNT - playerController.current_beacon_count).ToString ());
		GUI.EndGroup();

}

// Update is called once per frame
void Update () {
}
}
