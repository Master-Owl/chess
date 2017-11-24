using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TilePicker : MonoBehaviour {

	public GameObject tileContent;
	private static GameObject selectedTile;

	private Button[] tileButtons;

	public void Start() {
        tileButtons = tileContent.GetComponentsInChildren<Button>();
		foreach (Button tileButton in tileButtons) {
            tileButton.onClick.AddListener(OnTilePicked);
        }
		EnableTilePicking(false);
		selectedTile = null;
    }

	public void SetSelectedTile(GameObject tile) {
		selectedTile = tile;
	}

	public void EnableTilePicking(bool isEnabled) {
		foreach (Button tileButton in tileButtons) {
			tileButton.interactable = isEnabled;
		}
	}

	private void OnTilePicked() {
		if (selectedTile == null) return;
		GameObject clicked = EventSystem.current.currentSelectedGameObject;
		selectedTile.GetComponent<Image>().sprite = clicked.GetComponent<Image>().sprite;
		EnableTilePicking(false);
	}
}
