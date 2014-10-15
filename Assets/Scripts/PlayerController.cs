using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	// Player location
	public float speed;
	private Vector2 horizontal;
	private Vector2 vertical;
	
	// Sanity Utils
	public float sanity = 100;
	public bool sanityOff = false;
	private bool sanityCheck = false;
	private float nextCheckTime = 0;
	private float sanityCooldownTime = 5;
	private float sanityTime = 2;
	private float sanityEnd = 0;
	private float sanityDecreaseRate = 0.01f;
	
	// Beacon Utils
	public GameObject beacon;
	public uint MAX_BEACON_COUNT = 10;
	public uint current_beacon_count = 0;
	public float dropRate = 1f;
	private float nextDrop = 0f;
	private bool isNearBeacon = false;
	private bool beaconDestroyed = false;
	Animator anim;
	
	
	// Update is called once per frame
	void Update () {
		
		/* 
         * Player movement Logic
         */
		
		//Horizontal Movement Vector
		if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) {
			horizontal = Vector2.right * speed * Time.deltaTime;
		} else if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) {
			horizontal = -Vector2.right * speed * Time.deltaTime;
		} else {
			horizontal = Vector2.right * 0f * Time.deltaTime;
		}
		
		//Vertical Movement Vector
		if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.W)) {
			vertical = Vector2.up * speed * Time.deltaTime;
		} else if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.S)) {
			vertical = -Vector2.up * speed * Time.deltaTime;
		} else {
			vertical = Vector2.up * 0f * Time.deltaTime;
		}
		
		//Animator Controller
		if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D) ||
		    Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A) ||
		    Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.W) || 
		    Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.S)
		    ) {
			anim = GetComponent<Animator> ();
			anim.SetBool ("Walk", true);
		} else {
			anim = GetComponent<Animator> ();
			anim.SetBool ("Walk", false);
		}
		
		/* 
         * Sanity Logic 
         */
		
		if (!sanityCheck && nextCheckTime < Time.time) {
			sanityCheck = Random.Range (0, 101) > sanity;
			nextCheckTime = Time.time + sanityCooldownTime;
		}
		
		//Sanity Random Movement Vector
		if (sanityCheck && sanityOff && !isNearBeacon) {
			sanityOff = false;
			sanityEnd = sanityTime + Time.time;
			rigidbody2D.velocity = Random.insideUnitCircle * speed * Time.deltaTime;
		} else if (sanityOff) {
			Vector2 sumVector = horizontal + vertical;
			rigidbody2D.velocity = sumVector;
			if (sumVector.y != 0 || sumVector.x != 0) {
				transform.rotation = Quaternion.AngleAxis (Mathf.Atan2 (sumVector.y, sumVector.x) * Mathf.Rad2Deg, Vector3.forward);
			}
		} else if (sanityEnd < Time.time) {
			sanityEnd = 0;
			sanityOff = true;
			sanityCheck = false;
		}
		
		/*
         * Beacon Logic 
         */
		
		// Drop a beacon.
		if ((Input.GetKey (KeyCode.Space) || Input.GetKey (KeyCode.B)) && (Time.time > nextDrop)) {
			if (current_beacon_count < MAX_BEACON_COUNT) {
				nextDrop = Time.time + dropRate;
				current_beacon_count++;
				Instantiate (beacon, transform.position, Quaternion.identity);
			} 
		}
		
		if (beaconDestroyed) {
			beaconDestroyed = false;
			isNearBeacon = false;
		}
		
		if (sanityOff && !isNearBeacon) {
			sanity -= sanityDecreaseRate;
		}
		
	}
	
	/* Collisions */
	void OnTriggerStay2D (Collider2D other) {
		if (other.gameObject.tag == "BeaconLight") {
			isNearBeacon = true;	
		}
		if (other.gameObject.tag == "Beacon") {
			if (Input.GetKey (KeyCode.P)) {
				Destroy (other.gameObject.transform.parent.gameObject);
				current_beacon_count--;
				beaconDestroyed = true;
			}
		}
	}
	
	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "BeaconLight") {
			isNearBeacon = false;	
		}
	}
	
}