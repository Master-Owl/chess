using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece {

	public void InitQueen(GameObject tile, Player.PlayerColor color) {
		InitPiece(tile, Piece.PieceType.QUEEN);
		this.pieceColor = color;
		if (sprites != null) {
			if (color == Player.PlayerColor.DARK) {
				spriteRenderer.sprite = sprites[4];
			}
			else {
				spriteRenderer.sprite = sprites[10];
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

		return (letterDiff == numberDiff)
			|| (letterDiff == 0 && numberDiff != 0
			||  letterDiff != 0 && numberDiff == 0);
	}
}
