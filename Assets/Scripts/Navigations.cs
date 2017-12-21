using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigations : MonoBehaviour {

	public GameObject playerPanel;
	public GameObject tilePanel;

	public void LoadSceneByIndex(int index) {
		SceneManager.LoadScene(index);
	}

	public static void LoadSceneByName(string name) {
		SceneManager.LoadScene(name);
	}

	public void Quit() {
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}

	public void EnablePlayerPanel(bool enabled) {
		playerPanel.SetActive(enabled);
	}

	public void EnableTilePanel(bool enabled) {
		tilePanel.SetActive(enabled);
	}
}
