using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	private GameBoard gameBoard;
	private static UIManager uIManager;
	private static Player player_1;
	private static Player player_2;
	private static bool   gameInProgress = true;

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

	public void GameOver(Player winner) {
		EndGame.SetWinner(winner);
        // StartCoroutine(Finish());
		Navigations.LoadSceneByName("GameOver");
	}

	// private IEnumerator Finish() {
	// 	AudioClip clip = Resources.Load<AudioClip>("Sounds/cheer");
	// 	gameBoard.GetAudioSource().PlayOneShot(clip);
	// 	yield return new WaitForSeconds(clip.length);
	// 	Navigations.LoadSceneByName("GameOver");
	// }
	
	// The current player has captured their opponent's piece (meaning opponent loses a piece)
	public static void PieceCaptured(Piece piece) {
		if (player_1.IsTurn()) {
			if (!player_2.RemovePiece(piece))
				Debug.LogWarning(piece.ToString() + " doesn't exist in collection for " + player_2.ToString());
			else uIManager.AddCapturedPiece_P1(piece);
			if (piece.GetPieceType() == Piece.PieceType.KING)
				gameInProgress = false;		
		}
		else {
			if (!player_1.RemovePiece(piece))
				Debug.LogWarning(piece.ToString() + " doesn't exist in collection for " + player_1.ToString());
			else uIManager.AddCapturedPiece_P2(piece);
            if (piece.GetPieceType() == Piece.PieceType.KING)
				gameInProgress = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameInProgress) {
			if (player_1.HasLost()) GameOver(player_2);
			else GameOver(player_1);
		}
	}
}
