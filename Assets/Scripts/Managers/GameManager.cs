using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private GameBoard gameBoard;
	private static Player player_1;
	private static Player player_2;

	// Use this for initialization
	void Start () {
		if (gameBoard == null)
			gameBoard = gameObject.AddComponent<GameBoard>();
		
		player_1 = gameObject.AddComponent<Player>();
		player_2 =  gameObject.AddComponent<Player>();
		
		gameBoard.InitBoard();
		player_1.InitPlayer(Player.PlayerColor.LIGHT, gameBoard);
		player_2.InitPlayer(Player.PlayerColor.DARK, gameBoard);
		ArrayList pieces = player_1.GetPieces();
		pieces.AddRange(player_2.GetPieces());
		gameBoard.PlacePieces(pieces);
        player_1.ChangeTurn(); // Make player 1 the starting player
    }
	
	// The current player has captured their opponent's piece (meaning opponent loses a piece)
	public static void PieceCaptured(Piece piece) {
		if (player_1.IsTurn()) {
			player_2.RemovePiece(piece);
		}
		else {
			player_1.RemovePiece(piece);
		}
	}

	public static void ChangeTurn() {
		player_1.ChangeTurn();
		player_2.ChangeTurn();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
