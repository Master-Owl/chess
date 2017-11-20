using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece {

	private bool hasMoved;

	public void InitPawn(GameObject tile, Player.PlayerColor color) {
		this.pieceColor = color; 
		hasMoved = false;
		InitPiece(tile, Piece.PieceType.PAWN);
		if (sprites != null) {
			if (color == Player.PlayerColor.DARK) {
				spriteRenderer.sprite = sprites[3];
			}
			else {
				spriteRenderer.sprite = sprites[9];
			}
		}
	}

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool HasMoved() { return hasMoved; }
	
	override public bool IsValidMove(Tile t){
		Location dest = t.GetLocation();
		Location cur  = tile.GetLocation();

		if (dest.Equals(cur)) return false;

		int destLetter = (int)dest.letter;
		int destNumber = (int)dest.number;
		int curLetter  = (int)cur.letter;
		int curNumber  = (int)cur.number;

		int letterDiff = Mathf.Abs(destLetter - curLetter);
		int numberDiff = destNumber - curNumber;

		if (letterDiff > 1) return false;

		if (pieceColor == Player.PlayerColor.DARK) {
			// Facing down
			if (!hasMoved && numberDiff == -2) 
				return letterDiff == 0;
			else 
				return numberDiff == -1; 
		} else {
			// Facing up
			if (!hasMoved && numberDiff == 2)
				return letterDiff == 0; 
			else 
				return numberDiff == 1;
		}
	}

	override public void MovePiece(Tile tile) {
        gameObject.transform.SetParent(tile.transform, false);
        gameObject.transform.localPosition = new Vector2(0, 0);
        this.tile = tile;
        this.tile.SetPiece(this);
		hasMoved = true;
	}
}
