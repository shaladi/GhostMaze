using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Maze mazeInstance;

	private void Start () {
		BeginGame();
	}
	
	private void Update () {
//		if (Input.GetKeyDown(KeyCode.Space)) {
//			RestartGame();
//		}
	}

	private void BeginGame () {
		StartCoroutine(mazeInstance.Generate());

	}

	private void RestartGame () {
		StopAllCoroutines();
		Destroy(mazeInstance.gameObject);
		BeginGame();
	}
}