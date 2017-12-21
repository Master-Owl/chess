using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public GameObject player1_Name;
	public GameObject player1_CapturedPiecesBox;
	public GameObject player2_Name;
	public GameObject player2_CapturedPiecesBox;

	private Dictionary<Piece.PieceType, int> player1_CapturedPieces = new Dictionary<Piece.PieceType, int>();
	private Dictionary<Piece.PieceType, int> player2_CapturedPieces = new Dictionary<Piece.PieceType, int>();
	
	private const string CAPTURED_PIECES_TEXT = "Captured Pieces:";

	// Use this for initialization
	void Start () {
		foreach (Piece.PieceType type in System.Enum.GetValues(typeof(Piece.PieceType))){
			player1_CapturedPieces[type] = 0;
			player2_CapturedPieces[type] = 0;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetPlayer1Name(string name) {
        player1_Name.GetComponent<Text>().text = name;
	}

	public void SetPlayer2Name(string name) {
        player2_Name.GetComponent<Text>().text = name;
	}

	public void ShowWhoTurn(Player p) {
		string p1_Name = player1_Name.GetComponent<Text>().text;
		if (p1_Name == p.GetPlayerName()){
			player1_Name.GetComponent<Text>().fontStyle = FontStyle.Bold;
			player2_Name.GetComponent<Text>().fontStyle = FontStyle.Normal;
		}
		else {
			player2_Name.GetComponent<Text>().fontStyle = FontStyle.Bold;
			player1_Name.GetComponent<Text>().fontStyle = FontStyle.Normal;
		}
	}

	public void AddCapturedPiece_P1(Piece piece) {
		player1_CapturedPieces[piece.GetPieceType()]++;
		string[] capturedList = new string[5];
        int idx = -1;

        foreach (Piece.PieceType type in System.Enum.GetValues(typeof(Piece.PieceType))) {
			if (player1_CapturedPieces[type] != 0){
                capturedList[++idx] = type.ToString() + " (" + player1_CapturedPieces[type] + ")";
            }
		}

        Text boxText = player1_CapturedPiecesBox.GetComponent<Text>();
        boxText.text = CAPTURED_PIECES_TEXT;
        for (int i = 0; i <= idx; ++i) {
            boxText.text += '\n' + capturedList[i];
        }
    }

    public void AddCapturedPiece_P2(Piece piece) {
        player2_CapturedPieces[piece.GetPieceType()]++;
        string[] capturedList = new string[5];
        int idx = -1;

        foreach (Piece.PieceType type in System.Enum.GetValues(typeof(Piece.PieceType))) {
            if (player2_CapturedPieces[type] != 0) {
                capturedList[++idx] = type.ToString() + " (" + player2_CapturedPieces[type] + ")";
            }
        }

		Text boxText = player2_CapturedPiecesBox.GetComponent<Text>();
		boxText.text = CAPTURED_PIECES_TEXT;
        for (int i = 0; i <= idx; ++i) {
            boxText.text += '\n' + capturedList[i];
        }
    }
}
