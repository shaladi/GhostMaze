using UnityEngine;
using System.Collections;

public class MazeCreator : MonoBehaviour {

	public int width;
	public int height;

	public class MapSquare {
		private MapSquare top;
		private MapSquare left;
		private MapSquare right;
		private MapSquare bottom;

		private GameObject topWall;
		private GameObject leftWall;
		private GameObject rightWall;
		private GameObject bottomWall;

		private int x;
		private int y;

		public MapSquare (int x, int y) {
			this.x = x;
			this.y = y;
		}

		void createWall (int dir) {
			switch (dir) {
			case 0:
				this.top = null;

				return;
			case 1:
				this.right = null;
				return;;
			case 2:
				this.bottom = null;
				return;
			case 3:
				this.bottom = null;
				return;
			default:
				return;
			}
		}
	}

	// Use this for initialization
	void Start () {

		Generate_maze ();
	
	}

	void Generate_maze() {



		for (int i = 0; i < width; ++i) {

		}
	}

}
