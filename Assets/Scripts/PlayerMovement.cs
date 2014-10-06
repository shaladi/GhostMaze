using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.D)){
			transform.Translate(Vector2.right * 4f * Time.deltaTime);
		}
	
	}

}
