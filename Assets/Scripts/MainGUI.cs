using UnityEngine;
using System.Collections;

public class MainGUI : MonoBehaviour {
	public Vector2 pos = new Vector2(10,40);
	public Vector2 size = new Vector2(300,300);
	public PlayerController playerController;
	public bool gameHasStarted = false;
	// Use this for initialization
	void Start() {
//		GUI.BeginGroup (new Rect (0, 0, Screen.width, Screen.height));
//		GUI.backgroundColor = Color.black;
//				GUI.EndGroup ();
	}
	void OnGUI () {
		if (!gameHasStarted) {
			GUI.ModalWindow (0, new Rect (0, 0, Screen.width, Screen.height), SpawnInstructionsWindow, "");
		} else {
			var centeredStyle = GUI.skin.GetStyle("Label");
			centeredStyle.alignment = TextAnchor.UpperLeft;
			GUI.skin.label.fontSize = 12;
			GUI.BeginGroup (new Rect (pos.x, pos.y, size.x, size.y));
			GUI.backgroundColor = playerController.sanityOff ? Color.green : Color.red;
			GUI.Label (new Rect (0,0,200,30), "Sanity");
			GUI.HorizontalScrollbar(new Rect (50,3,200,20), 0, playerController.sanity,0, 100);
			GUI.Label (new Rect (0,20,200,30), "Remaining Beacons:");
			GUI.Label (new Rect (150, 20, 200, 30), (playerController.MAX_BEACON_COUNT - playerController.current_beacon_count).ToString ());
			GUI.EndGroup();
		}

    }
	void SpawnInstructionsWindow(int windowID) {
		Time.timeScale = 0; //pauses the game

		var centeredStyle = GUI.skin.GetStyle("Label");
		centeredStyle.alignment = TextAnchor.UpperCenter;
		GUI.Box (new Rect (0, 0, Screen.width, Screen.height), new GUIContent ());

		GUI.skin.label.fontSize = 40;
		GUI.Label (new Rect (0, 100, Screen.width, Screen.height), "GHOST MAZE");

		GUI.skin.label.fontSize = 20;
		GUI.Label (new Rect (0, 200, Screen.width, Screen.height), 
		           "You've awoken in the middle of a dark maze with nothing but a lantern.\n" +
						"As you explore the maze, you realize you are slowly losing your <color=red><b>Sanity</b></color>!\n" + 
		           "You sometimes get so scared that you <color=yellow><b>lose control of where you're walking</b></color>!\n" +
						"You need to escape the maze quickly before you go completely insane!\n\n" +
						"Luckily, you find 10 <color=cyan><b>Beacons</b></color> in your pocket which you can place down\n " +
						"to help you remain sane and give you vision of the maze.\n\n" +
		           "Controls: \n");

		centeredStyle.alignment = TextAnchor.UpperLeft;
		GUI.Label (new Rect (290, 410, Screen.width, Screen.height),
			       "Arrow Keys/WASD : Move\n"+
		           "              Spacebar : Place a beacon\n"+
		           "                          P : Pick up a beacon\n" +
		           "                          Z : Zoom camera out\n"
		           );

		GUI.skin.button.fontSize = 20;
		if (GUI.Button(new Rect(Screen.width/2 - 100, 600, 200, 30), "Can you escape?")){
			Time.timeScale = 1; // starts game
		    gameHasStarted = true;
		}  
		
	}
	
	// Update is called once per frame
void Update () {
}
}
