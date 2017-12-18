using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public GameObject player1_Name;
	public GameObject player1_CapturedPiecesBox;
	public GameObject player2_Name;
	public GameObject player2_CapturedPiecesBox;

	// Use this for initialization
	void Start () {
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

	public void AddCapturedPiece_P1(string piece) {
		player1_CapturedPiecesBox.GetComponent<Text>().text += '\n' + piece;
	}

    public void AddCapturedPiece_P2(string piece) {
        player2_CapturedPiecesBox.GetComponent<Text>().text += '\n' + piece;
    }
}
