using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour {

	public GameObject vanguard;
	public Maze mazePrefab;
	private Maze mazeInstance;

	private void Start () {
		StartCoroutine(BeginGame());
	}

	private void Update() {

		if (Input.GetKey(KeyCode.BackQuote))
			RestartGame();
	}

	private IEnumerator BeginGame() {
		mazeInstance = Instantiate (mazePrefab) as Maze;
		yield return StartCoroutine(mazeInstance.Generate());
	}

	private void RestartGame() {

		StopAllCoroutines();
		Destroy(mazeInstance.gameObject);
		StartCoroutine(BeginGame());
	}
}
