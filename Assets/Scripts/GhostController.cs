using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GhostController : MonoBehaviour {
	
	// Ghost location
	public Maze maze;
	public int numCells;
	public List<MazeCell> hauntedCells;
	
	public float speed =  100f;
	public float refreshRate = 4f;
	//    private float nextCommand = 0f;
	public int currCellIndex = 1;
	
	// Update is called once per frame
	void FixedUpdate () {
		
//		Vector3 vel = hauntedCells[currCellIndex].transform.position - transform.position + new Vector3 (0.0f, 0.0f, 0.5f);
//		rigidbody2D.velocity = vel.normalized * speed * Time.deltaTime;
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
	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Cell") {
			currCellIndex = (currCellIndex + 1) % numCells;
		}
	}
	
	public void SetInitialCell (MazeCell cell) {
		transform.position = cell.transform.position;
		for (int i = 0; i < numCells; ++i) {
			hauntedCells.Add (cell);
			MazeCellEdge edge = null;
			int dir = Random.Range(0, 3);
			bool valid_dir = false;
			int retries = 4;
			while(!valid_dir && (retries > 0)) {
				try {
					edge = cell.GetEdge((MazeDirection)dir);
					if (edge.otherCell != null)
						valid_dir = true;
					else {
						dir = (dir + 1) % 4;
						--retries;
					}
				} catch (System.Exception) {
					--retries;
				}
			}
			if (hauntedCells.IndexOf(edge.otherCell) == -1) {
				cell = edge.otherCell;
			} else {
				--i;
			}
		}
		for (int i = 1; i < hauntedCells.Count; ++i) {
			if (hauntedCells[i] == hauntedCells[i-1]) {
				hauntedCells.RemoveAt(i);
				--i;
			}
		}
	}
	
}
