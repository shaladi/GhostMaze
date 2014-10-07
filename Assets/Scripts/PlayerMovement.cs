using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float speed;
	private Vector2 horizontal;
	private Vector2 vertical;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)){
			horizontal = Vector2.right * speed * Time.deltaTime;
		}
		else if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)){
			horizontal = -Vector2.right * speed * Time.deltaTime;
		}
		else {
			horizontal = Vector2.right * 0f * Time.deltaTime;
		}

		if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.W)){
			vertical = Vector2.up * speed * Time.deltaTime;
		}
		else if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.S)){
			vertical = -Vector2.up * speed * Time.deltaTime;
		}
		else {
			vertical = Vector2.up * 0f * Time.deltaTime;
		}

		Vector2 sumVector = horizontal + vertical;
		rigidbody2D.velocity = sumVector;
		if (sumVector.y != 0 || sumVector.x != 0) {
						transform.rotation = Quaternion.AngleAxis (Mathf.Atan2 (sumVector.y, sumVector.x) * Mathf.Rad2Deg, Vector3.forward);
				}
	
	}

}
