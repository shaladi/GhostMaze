using UnityEngine;
using System.Collections;

public class endWallController : MonoBehaviour {

	public bool gameHasEnded = false;
	public PlayerController playerController;

	void OnTriggerEnter2D (Collider2D hitInfo) {
		if (hitInfo.name == "Player") {
			gameHasEnded = true;
		}
	}
}
