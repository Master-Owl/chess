using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece {

	public void InitBishop(GameObject tile, Player.PlayerColor color) {
		InitPiece(tile, Piece.PieceType.BISHOP);
		pieceColor = color;
		if (sprites != null) {
			if (color == Player.PlayerColor.DARK) {
				spriteRenderer.sprite = sprites[0];
			}
			else {
				spriteRenderer.sprite = sprites[6];
			}
		}
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	override public bool IsValidMove(Tile t){
		Location dest = t.GetLocation();
		Location cur  = tile.GetLocation();

		if (dest.Equals(cur)) return false;

		int destLetter = (int)dest.letter;
		int destNumber = (int)dest.number;
		int curLetter  = (int)cur.letter;
		int curNumber  = (int)cur.number;

		int letterDiff = Mathf.Abs(destLetter - curLetter);
		int numberDiff = Mathf.Abs(destNumber - curNumber);

		return letterDiff == numberDiff;
	}
}
