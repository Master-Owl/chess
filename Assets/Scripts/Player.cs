using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public enum PlayerColor { DARK, LIGHT };
	private bool isTurn;
	private string playerName;
	private GameBoard board;
	public PlayerColor playerColor;
	public ArrayList pieces;

	public void InitPlayer(string name, bool isLight, GameBoard board) {
		this.playerColor = isLight ? PlayerColor.LIGHT : PlayerColor.DARK;
		this.playerName = name;
		this.board = board;
		isTurn = false;
		InitPieces();
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool RemovePiece(Piece piece) {
		if (pieces.Contains(piece)) {
			pieces.Remove(piece);
			Destroy(piece.gameObject);
			return true;
		}
		return false;
	}

	public void ChangeTurn() { isTurn = !isTurn; }

	public bool IsTurn() { return isTurn; }

	public PlayerColor GetColor() { return playerColor; }

	public ArrayList GetPieces() { return pieces; }

	private void InitPieces() {
		int backRow, pawnRow;
		pieces = new ArrayList();
		Dictionary<Location, GameObject> tiles = board.GetTiles();

		pieces = new ArrayList();
		if (playerColor == PlayerColor.DARK) {
            backRow = 8;
			pawnRow = 7;
        }
		else {
            backRow = 1;
			pawnRow = 2;
        }

		King K = new GameObject().AddComponent<King>();
		Queen Q = new GameObject().AddComponent<Queen>();
		Bishop b1 = new GameObject().AddComponent<Bishop>();
		Bishop b2 = new GameObject().AddComponent<Bishop>();
		Knight k1 = new GameObject().AddComponent<Knight>();
		Knight k2 = new GameObject().AddComponent<Knight>();
		Rook r1 = new GameObject().AddComponent<Rook>();
		Rook r2 = new GameObject().AddComponent<Rook>();

		r1.InitRook(tiles[new Location(Letter.A, backRow)], playerColor);
		k1.InitKnight(tiles[ new Location(Letter.B, backRow)], playerColor);
		b1.InitBishop(tiles[new Location(Letter.C, backRow)], playerColor);
		Q.InitQueen(tiles[new Location(Letter.D, backRow)], playerColor);
		K.InitKing(tiles[new Location(Letter.E, backRow)], playerColor);
		b2.InitBishop(tiles[new Location(Letter.F, backRow)], playerColor);
		k2.InitKnight(tiles[new Location(Letter.G, backRow)], playerColor);
		r2.InitRook(tiles[new Location(Letter.H, backRow)], playerColor);

		pieces.Add(K);
		pieces.Add(Q);
		pieces.Add(b1);
		pieces.Add(b2);
		pieces.Add(k1);
		pieces.Add(k2);
		pieces.Add(r1);
		pieces.Add(r2);

		foreach (Letter letter in Enum.GetValues(typeof(Letter))) {
			Pawn p = new GameObject().AddComponent<Pawn>();
			p.InitPawn(tiles[new Location(letter, pawnRow)], playerColor);
			pieces.Add(p);
		}
	
	}

	public override string ToString() {
		return this.playerName;
	}
}
