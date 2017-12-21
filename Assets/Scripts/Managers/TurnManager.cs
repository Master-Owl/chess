using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

	private static Player player1;
	private static Player player2;
	private static bool p1Turn;
	private static bool gameIsRunning;
	private static UIManager uIManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameIsRunning) {

		}
	}

	public static void InitTurnManager(Player p1, Player p2) {
		player1 = p1;
		player2 = p2;
		p1Turn = p1.IsTurn();
		gameIsRunning = false;
	}

	public static void SetUIManager(UIManager manager) {
		uIManager = manager;
	}

	public static void PlayGame() {
		gameIsRunning = true;
		ShowWhoTurn();
	}

    public static void ChangeTurn() {
        player1.ChangeTurn();
        player2.ChangeTurn();
		p1Turn = player1.IsTurn();
		ShowWhoTurn();
    }

	public static bool IsValidPieceSelection(Piece piece) {
		Player.PlayerColor pieceColor = piece.GetColor();
		return pieceColor == GetPlayerTurn().GetColor();
	}

	private static void ShowWhoTurn() {
        if (p1Turn) uIManager.ShowWhoTurn(player1);
        else uIManager.ShowWhoTurn(player2);
	}

    public static Player GetPlayerTurn() {
        if (p1Turn)
            return player1;
        return player2;
    }
}
