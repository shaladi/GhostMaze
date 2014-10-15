using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Maze mazeObject;
	
	private void Start () {
		BeginGame();
	}
	
	private void Update () {
//		if (Input.GetKeyDown(KeyCode.Space)) {
//			RestartGame();
//		}
	}

	private void BeginGame () {
		// mazeInstance = Instantiate(mazeObject) as Maze;
		StartCoroutine(mazeObject.Generate());

	}

	private void RestartGame () {
		StopAllCoroutines();
		BeginGame();
	}
}