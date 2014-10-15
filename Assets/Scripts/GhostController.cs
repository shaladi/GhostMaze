using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GhostController : MonoBehaviour {
	
	// Ghost location
	public Maze maze;
	public int numCells;
	public List<Vector3> hauntedCells;
	
	public float speed;
	public float refreshRate;
	//    private float nextCommand = 0f;
	public int currCellIndex;

	private int resetCounter = 0;

	private IntVector2[] translations;

	// Update is called once per frame
	void Update () {
		
		Vector3 vel = hauntedCells[currCellIndex] - transform.position;
//		float mag = vel.magnitude;
//		if (mag <= 0.1) {
//			currCellIndex = (currCellIndex + 1) % numCells;
//		}
		Vector3 sumVector = vel.normalized * speed * Time.deltaTime;

		rigidbody2D.velocity = sumVector;
		if ((sumVector.y != 0 || sumVector.x != 0 )&& resetCounter < 1) {
			transform.rotation = Quaternion.AngleAxis (Mathf.Atan2 (sumVector.y, sumVector.x) * Mathf.Rad2Deg, Vector3.forward);
		}
		resetCounter++;
		//
		////        /* 
		////         * Ghost movement Logic
		////         */
		////        if (Time.time > nextCommand) {
		////            nextCommand = Time.time + refreshRate;
		////            //Horizontal Movement Vector
		////            Vector2 hor = (-(Random.Range (-1, 2))) * Vector2.right;
		////            Vector2 ver = (-(Random.Range (-1, 2))) * Vector2.up;
		////            rigidbody2D.velocity = (hor + ver) * speed * Time.deltaTime;
		////        }
	}
	
	/* Collisions */
	void OnTriggerStay2D (Collider2D other) {
		if (other.gameObject.tag == "Cell" && resetCounter > 40) {
			currCellIndex = (currCellIndex + 1) % numCells;
			//currCellIndex =  Random.Range(1,numCells);

			resetCounter = 0;
		}
	}

	private Vector3 ToWorldCoordinates (IntVector2 c) {
		return new Vector3(4*(c.x - maze.size.x * 0.5f + 0.5f), 4*(c.y - maze.size.y * 0.5f + 0.5f), 0.0f);
	}
	
	public void SetInitialCell (MazeCell cell) {
		IntVector2 coords = cell.coordinates;
		translations = new IntVector2[4];
		translations [0] = new IntVector2 (0, 1);
		translations [1] = new IntVector2 (1, 0);
		translations [2] = new IntVector2 (0, -1);
		translations [3] = new IntVector2 (-1, 0);
		for (int i = 0; i < numCells; ++i) {
			int dir = Random.Range (0, 4);
			IntVector2 newCoords = coords + translations[dir];
			if (maze.ContainsCoordinates(newCoords)) {
				hauntedCells.Add(ToWorldCoordinates(coords));
				coords = newCoords;
			} else {
				--i;
			}

		}
		currCellIndex = 1;
	}
	
}
