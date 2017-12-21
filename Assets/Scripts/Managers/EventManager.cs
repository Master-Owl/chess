using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    public delegate void ClickAction();
    public static event ClickAction OnTileClicked;

    void Update() {
		if (Input.GetMouseButtonDown(0) && MouseInBounds() && OnTileClicked != null)
			OnTileClicked();		
    }

	private bool MouseInBounds() {
		return Input.mousePosition.x >= 0
			&& Input.mousePosition.y >= 0
			&& Input.mousePosition.x < Screen.width
			&& Input.mousePosition.y < Screen.height;
	}

}
