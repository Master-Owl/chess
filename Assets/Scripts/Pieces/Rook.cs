using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Piece {

	public void InitRook(GameObject tile, Player.PlayerColor color) {
		InitPiece(tile, Piece.PieceType.ROOK);
		this.pieceColor = color;
		if (sprites != null) {
			if (color == Player.PlayerColor.DARK) {
				spriteRenderer.sprite = sprites[5];
			}
			else {
				spriteRenderer.sprite = sprites[11];
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

		int letterDiff = destLetter - curLetter;
		int numberDiff = destNumber - curNumber;

		return letterDiff == 0 && numberDiff != 0
			|| letterDiff != 0 && numberDiff == 0;
	}
}
