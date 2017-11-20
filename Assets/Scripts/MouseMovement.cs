using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour {

	private Piece selectedPiece = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (selectedPiece != null) {
			selectedPiece.transform.position = 
				Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
	}

	public void SetSelectedPiece(Piece piece) {
		selectedPiece = piece;
		selectedPiece.transform.SetParent(null);
	}

	public void RemoveSelectedPiece() { this.selectedPiece = null; }
}
