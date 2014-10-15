using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Maze maze;

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

	// Key Utils
	public GameObject key;
	public bool hasKey = false;
	private bool keyInit = false;
	public bool gameHasEnded = false;
	public CameraController cc;

	public Vector3 RandomCoordinates3 {
		get {
			int x,y;
			do{
			x = Random.Range(-20,20);
			y = Random.Range(-20,20);
			}while( x*x + y*y < 100);
			return new Vector3(x, 
			                   y, transform.position.z); 
		}
	}

	// Update is called once per frame
	void Update () {

		if (keyInit == false) {
						keyInit = true;
						Vector3 pos = RandomCoordinates3;
						Instantiate (key, pos, Quaternion.identity);
				}

		/* 
         * Player movement Logic
         */
		if (!Input.GetKey (KeyCode.Z)) {
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
		} else {
			horizontal = Vector2.right * 0f * Time.deltaTime;
			vertical = Vector2.up * 0f * Time.deltaTime;
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
			sanity -= sanityDecreaseRate * Time.timeScale;
		}
		
	}
	
	/* Collisions */
	void OnTriggerStay2D (Collider2D other) {
		if (other.gameObject.tag == "Beacon-Light") {
			isNearBeacon = true;	
		}
		if (other.gameObject.tag == "Beacon") {
			if (Input.GetKey (KeyCode.P)) {
				Destroy (other.gameObject.transform.parent.gameObject);
				current_beacon_count--;
				beaconDestroyed = true;
			}
		}
		if (other.gameObject.tag == "Ghost") {
			sanityDecreaseRate = 0.03f;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
				if (other.gameObject.tag == "Key") {
						hasKey = true;
						Destroy (other.gameObject);

						/*
					     * Remove a random wall from the scene
					     **/
						var randomEdgeWallIndex = Random.Range (0, maze.edgeWalls.Count);
						var thisWall = maze.edgeWalls [randomEdgeWallIndex];
						Destroy (thisWall.gameObject);
						
						// Add the collider for completing the level
						
						var x = thisWall.transform.position.x;
						var y = thisWall.transform.position.y;
						
						if (thisWall.direction == (MazeDirection)0) {
							y += 2;
							maze.endWall.size = new Vector2(4f, 0.25f);
						} else if (thisWall.direction == (MazeDirection)1) {
							x += 2;
							maze.endWall.size = new Vector2(0.25f, 4f);
						} else if (thisWall.direction == (MazeDirection)2) {
							y -= 2;
							maze.endWall.size = new Vector2(4f, 0.25f);
						} else if (thisWall.direction == (MazeDirection)3) {
							x -= 2;
							maze.endWall.size = new Vector2(0.25f, 4f);
						}
						
						Instantiate (maze.endWall, new Vector3 (x, y, 0), Quaternion.identity);
				}
		
				if (other.gameObject.tag == "endWall") {
						if (hasKey){
							gameHasEnded = true;
							}
						else{
							
						}
				}
		}
	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "Beacon-Light") {
			isNearBeacon = false;	
		}
		if (other.gameObject.tag == "Ghost") {
			sanityDecreaseRate = 0.01f;
		}
	}
	
}