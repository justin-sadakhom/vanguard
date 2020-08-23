using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour {

	private GameObject menus;
	private Component[] menusArray;
	private Canvas escapeMenu;
	private Canvas optionsMenu;
	private Canvas graphicsMenu;
	private Canvas scenariosMenu;

	private Component[] escapeButtons;
	private Button options;
	private Button quit;
	private Button scenarios;
	private Button back1;

	private Component[] optionsButtons;
	private Button graphics;
	private Button back2;

	private Component[] scenariosButtons;
	private Button scene1;
	private Button scene2;
	private Button scene3;
	private Button scene4;
	private Button back3;

	private Button back4;

	void Start() {

		menus = GameObject.Find("Menus");
		menusArray = menus.GetComponentsInChildren<Canvas>();
		escapeMenu = GetComponentInChildren<Canvas>();
		optionsMenu = (Canvas)menusArray[1];
		graphicsMenu = (Canvas)menusArray[2];
		scenariosMenu = (Canvas)menusArray[3];

		escapeMenu.enabled = false;
		optionsMenu.enabled = false;
		scenariosMenu.enabled = false;
		graphicsMenu.enabled = false;
		Cursor.visible = false;

		escapeButtons = escapeMenu.GetComponentsInChildren <Button>();
		options = (Button)escapeButtons[0];
		scenarios = (Button)escapeButtons[1];
		quit = (Button)escapeButtons[2];
		back1 = (Button)escapeButtons[3];

		optionsButtons = optionsMenu.GetComponentsInChildren <Button>();
		graphics = (Button)optionsButtons[0];
		back2 = (Button)optionsButtons[2];

		scenariosButtons = scenariosMenu.GetComponentsInChildren <Button>();
		scene1 = (Button)scenariosButtons[0];
		scene2 = (Button)scenariosButtons[1];
		scene3 = (Button)scenariosButtons[2];
		scene4 = (Button)scenariosButtons[3];
		back3 = (Button)scenariosButtons[4];

		back4 = graphicsMenu.GetComponentInChildren<Button>();

		options.onClick.AddListener(toOptions);
		graphics.onClick.AddListener(toGraphics);
		scenarios.onClick.AddListener(toScenarios);
		quit.onClick.AddListener(terminate);
		back1.onClick.AddListener(exitMenu);
		back2.onClick.AddListener(toEscapeMenu);
		back3.onClick.AddListener(toEscapeMenu);
		back4.onClick.AddListener(toOptions);

		scene1.onClick.AddListener(toFirstScene);
		scene2.onClick.AddListener(toSecondScene);
		scene3.onClick.AddListener(toThirdScene);
		scene4.onClick.AddListener(toFourthScene);
	}

	void Update() {

		if (Input.GetKeyDown(KeyCode.Escape)) {
			escapeMenu.enabled = !escapeMenu.enabled;
			optionsMenu.enabled = false;
			graphicsMenu.enabled = false;
			scenariosMenu.enabled = false;
		}

		if (escapeMenu.enabled || optionsMenu.enabled || graphicsMenu.enabled || scenariosMenu.enabled)
			Cursor.visible = true;
		else
			Cursor.visible = false;
	}

	void exitMenu() {
		escapeMenu.enabled = false;
	}

	void toEscapeMenu() {
		optionsMenu.enabled = false;
		scenariosMenu.enabled = false;
		escapeMenu.enabled = true;
	}

	void toOptions() {
		escapeMenu.enabled = false;
		graphicsMenu.enabled = false;
		optionsMenu.enabled = true;
	}

	void toGraphics() {
		optionsMenu.enabled = false;
		graphicsMenu.enabled = true;
	}

	void toScenarios() {
		escapeMenu.enabled = false;
		scenariosMenu.enabled = true;
	}

	void toFirstScene() {
		SceneManager.LoadScene("Maze");
	}

	void toSecondScene() {
		SceneManager.LoadScene("Scene 2");
	}

	void toThirdScene() {
		SceneManager.LoadScene("Scene 3");
	}

	void toFourthScene() {
		SceneManager.LoadScene("Scene 4");
	}

	void terminate() {
		Application.Quit();
	}
}