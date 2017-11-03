using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameBoard gameBoard;
	private Player player_1;
	private Player player_2;

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
		

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
