using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	void Awake () {
		DontDestroyOnLoad(transform.gameObject);
	}

	void Update () {
		
		// Reset current scene.
		if (Input.GetKeyDown (KeyCode.Delete))
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}