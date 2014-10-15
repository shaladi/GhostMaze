using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze : MonoBehaviour {

	public IntVector2 size;

	public MazeCell cellPrefab;

	public float generationStepDelay;

	public MazePassage passagePrefab;
	public MazeWall wallPrefab;
	public List<MazeWall> edgeWalls = new List<MazeWall>();	
	public BoxCollider2D endWall;

	public GameObject dynamic;
	public MainGUI gui;

	private MazeCell[,] cells;
	private int cellSize = 4;

	public int numGhosts;
	public GhostController Ghosts;

	public IntVector2 RandomCoordinates {
		get {
			return new IntVector2(Random.Range(0, size.x), Random.Range(0, size.y));
		}
	}

	public Vector3 RandomCoordinates3 {
		get {
			return new Vector3(Random.Range(-size.x/2 + 1, size.x/2 - 1)*cellSize, 
			                   Random.Range(-size.y/2 + 1, size.y/2 - 1)*cellSize, 0.0f); 
		}
	}

	public bool ContainsCoordinates (IntVector2 coordinate) {
		return coordinate.x >= 0 && coordinate.x < size.x && coordinate.y >= 0 && coordinate.y < size.y;
	}

	public MazeCell GetCell (IntVector2 coordinates) {
		return cells[coordinates.x, coordinates.y];
	}

	public IEnumerator Generate () {
		dynamic.SetActive (false);
		WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
		cells = new MazeCell[size.x, size.y];
		List<MazeCell> activeCells = new List<MazeCell>();
		DoFirstGenerationStep(activeCells);
		while (activeCells.Count > 0) {
			yield return delay;
			DoNextGenerationStep(activeCells);
		}
		/*
		 * Remove a random wall from the scene
		 **/
		var randomEdgeWallIndex = Random.Range (0, edgeWalls.Count);
		var thisWall = edgeWalls [randomEdgeWallIndex];
		Destroy (thisWall.gameObject);

		// Add the collider for completing the level

		var x = thisWall.transform.position.x;
		var y = thisWall.transform.position.y;

		if (thisWall.direction == (MazeDirection)0) {
			y += 2;
			endWall.size = new Vector2(4f, 0.25f);
		} else if (thisWall.direction == (MazeDirection)1) {
			x += 2;
			endWall.size = new Vector2(0.25f, 4f);
		} else if (thisWall.direction == (MazeDirection)2) {
			y -= 2;
			endWall.size = new Vector2(4f, 0.25f);
		} else if (thisWall.direction == (MazeDirection)3) {
			x -= 2;
			endWall.size = new Vector2(0.25f, 4f);
		}

		Instantiate (endWall, new Vector3 (x, y, 0), Quaternion.identity);

		for (int i = 0; i < numGhosts; i++) {
			MazeCell cell = GetCell(RandomCoordinates);
			Vector3 pos = cell.transform.position;
			GhostController gh = Instantiate(Ghosts, pos - new Vector3(0.0f, 0.0f, 0.5f), Quaternion.identity) as GhostController;
			gh.SetInitialCell(cell);
		}
		dynamic.SetActive (true);
	}

	private void DoFirstGenerationStep (List<MazeCell> activeCells) {
		activeCells.Add(CreateCell(RandomCoordinates));
	}

	private void DoNextGenerationStep (List<MazeCell> activeCells) {
		int currentIndex = activeCells.Count - 1;
		MazeCell currentCell = activeCells[currentIndex];
		if (currentCell.IsFullyInitialized) {
			activeCells.RemoveAt(currentIndex);
			return;
		}
		MazeDirection direction = currentCell.RandomUninitializedDirection;
		IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();
		if (ContainsCoordinates(coordinates)) {
			MazeCell neighbor = GetCell(coordinates);
			if (neighbor == null) {
				neighbor = CreateCell(coordinates);
				CreatePassage(currentCell, neighbor, direction);
				activeCells.Add(neighbor);
			}
			else {
				CreateWall(currentCell, neighbor, direction);
			}
		}
		else {
			CreateWall(currentCell, null, direction);
		}
	}

	private MazeCell CreateCell (IntVector2 coordinates) {
		MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
		cells[coordinates.x, coordinates.y] = newCell;
		newCell.coordinates = coordinates;
		newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.y;
		newCell.transform.parent = transform;
		newCell.transform.localPosition = new Vector3(coordinates.x - size.x * 0.5f + 0.5f, coordinates.y - size.y * 0.5f + 0.5f, 0.5f);
		return newCell;
	}

	private void CreatePassage (MazeCell cell, MazeCell otherCell, MazeDirection direction) {
		MazePassage passage = Instantiate(passagePrefab) as MazePassage;
		passage.Initialize(cell, otherCell, direction);
		passage = Instantiate(passagePrefab) as MazePassage;
		passage.Initialize(otherCell, cell, direction.GetOpposite());
	}

	private void CreateWall (MazeCell cell, MazeCell otherCell, MazeDirection direction) {
		MazeWall wall = Instantiate(wallPrefab) as MazeWall;
		wall.Initialize(cell, otherCell, direction);
		if (otherCell != null) {
			wall = Instantiate(wallPrefab) as MazeWall;
			wall.Initialize(otherCell, cell, direction.GetOpposite());
		} else {
			edgeWalls.Add (wall);
		}
	}
}