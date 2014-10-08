using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	private Vector2 horizontal;
	private Vector2 vertical;
	public float sanity = 100;
	private bool sanityOff = false;
	private bool sanityCheck = false;
	private float nextCheckTime = 0;
	private float sanityCooldownTime = 5;
	private float sanityTime = 2;
	private float sanityEnd = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Horizontal Movement Vector
		if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)){
			horizontal = Vector2.right * speed * Time.deltaTime;
		}
		else if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)){
			horizontal = -Vector2.right * speed * Time.deltaTime;
		}
		else {
			horizontal = Vector2.right * 0f * Time.deltaTime;
		}

		//Vertical Movement Vector
		if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.W)){
			vertical = Vector2.up * speed * Time.deltaTime;
		}
		else if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.S)){
			vertical = -Vector2.up * speed * Time.deltaTime;
		}
		else {
			vertical = Vector2.up * 0f * Time.deltaTime;
		}

		if (!sanityCheck && nextCheckTime < Time.time) {
			sanityCheck = Random.Range(0,101) > sanity;
			nextCheckTime = Time.time + sanityCooldownTime;
		}

		//Sanity Random Movement Vector
		if (sanityCheck && sanityOff){
			sanityOff = false;
			sanityEnd = sanityTime + Time.time;
			rigidbody2D.velocity = Random.insideUnitCircle * speed * Time.deltaTime;
		}
		else if (sanityOff){
			Vector2 sumVector = horizontal + vertical;
			rigidbody2D.velocity = sumVector;
			if (sumVector.y != 0 || sumVector.x != 0) {
				transform.rotation = Quaternion.AngleAxis (Mathf.Atan2 (sumVector.y, sumVector.x) * Mathf.Rad2Deg, Vector3.forward);
			}
		}
		else if (sanityEnd < Time.time){
			sanityEnd = 0;
			sanityOff = true;
			sanityCheck = false;
		}
	
	}

}
