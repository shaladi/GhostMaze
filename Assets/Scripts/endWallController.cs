using UnityEngine;
using System.Collections;

public class endWallController : MonoBehaviour {
	
	void OnTriggerEnter2D (Collider2D hitInfo) {
		print ("something hit me");
		if (hitInfo.name == "Player") {
			print ("Game complete!");
		}
	}
}
