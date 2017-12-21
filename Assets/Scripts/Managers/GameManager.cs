using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	private GameBoard gameBoard;
	private static UIManager uIManager;
	private static Player player_1;
	private static Player player_2;

	// Use this for initialization
	void Start () {
		uIManager = gameObject.GetComponentInChildren<UIManager>();
		gameBoard = gameObject.AddComponent<GameBoard>();		
		player_1  = gameObject.AddComponent<Player>();
		player_2  = gameObject.AddComponent<Player>();
		
		gameBoard.InitBoard();

		player_1.InitPlayer(Menus.Player1Name(), Menus.Player1Light(), gameBoard);
		player_2.InitPlayer(Menus.Player2Name(), !Menus.Player1Light(), gameBoard);

		uIManager.SetPlayer1Name(player_1.GetPlayerName());
		uIManager.SetPlayer2Name(player_2.GetPlayerName());

		ArrayList pieces = player_1.GetPieces();
		pieces.AddRange(player_2.GetPieces());
		gameBoard.PlacePieces(pieces);

		TurnManager.InitTurnManager(player_1, player_2);
		TurnManager.SetUIManager(uIManager);
		TurnManager.PlayGame();
    }
	
	// The current player has captured their opponent's piece (meaning opponent loses a piece)
	public static void PieceCaptured(Piece piece) {
		if (player_1.IsTurn()) {
			if (!player_2.RemovePiece(piece))
				Debug.LogWarning(piece.ToString() + " doesn't exist in collection for " + player_2.ToString());
			else uIManager.AddCapturedPiece_P1(piece);
		}
		else {
			if (!player_1.RemovePiece(piece))
				Debug.LogWarning(piece.ToString() + " doesn't exist in collection for " + player_1.ToString());
			else uIManager.AddCapturedPiece_P2(piece);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
