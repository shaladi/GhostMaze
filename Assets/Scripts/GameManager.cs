using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Maze mazeObject;

	private Maze mazeInstance;

	private void Start () {
		BeginGame();
	}
	
	private void Update () {
//		if (Input.GetKeyDown(KeyCode.Space)) {
//			RestartGame();
//		}
	}

	private void BeginGame () {
		mazeInstance = Instantiate(mazeObject) as Maze;
		Coroutine generator = StartCoroutine(mazeInstance.Generate());

	}

	private void RestartGame () {
		StopAllCoroutines();
		Destroy(mazeInstance.gameObject);
		BeginGame();
	}
}