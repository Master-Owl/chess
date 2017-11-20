using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour {

	private Piece selectedPiece = null;
	private float pieceSpeed = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (selectedPiece != null) {
            selectedPiece.transform.position = 
				Vector2.Lerp(
					selectedPiece.transform.position, 
					Camera.main.ScreenToWorldPoint(Input.mousePosition), 
					Time.deltaTime * pieceSpeed);
		}
	}

	public void SetSelectedPiece(Piece piece) {
		selectedPiece = piece;
	}

	public void RemoveSelectedPiece() { 
		selectedPiece.transform.localPosition = new Vector2(0, 0); // Put piece back on previous tile in case of piece swap/invalid move
		selectedPiece = null; 
	}
}
