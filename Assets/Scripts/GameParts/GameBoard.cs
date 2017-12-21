using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBoard : MonoBehaviour {

	private Dictionary<Location, GameObject> tiles;
	private AudioClip capturePiece = null;
	private List<AudioClip> invalidMove = new List<AudioClip>();
	private Piece activePiece = null;
	private System.Random rand = new System.Random();
	private MouseMovement mouseMovement = null;
	private AudioSource audioSource = null;

	// Use this for initialization
	void Start () {
		gameObject.name = "Game Board";
		gameObject.tag = "Game Board";
		gameObject.layer = 8;
		gameObject.transform.parent = Camera.main.transform;
		mouseMovement = gameObject.AddComponent<MouseMovement>();
		audioSource = gameObject.AddComponent<AudioSource>();
		capturePiece = Resources.Load<AudioClip>("Sounds/test_click");
		invalidMove.Add(Resources.Load<AudioClip>("Sounds/no_1"));
		invalidMove.Add(Resources.Load<AudioClip>("Sounds/no_2"));
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(1)) DeselectPiece(); // Right click to deselect a piece
	}

	public AudioSource GetAudioSource() { return audioSource; }

	public void TileClicked(Tile tile) {
		if (activePiece == null && !TurnManager.IsValidPieceSelection(tile.GetPiece())) 
			return;

		if (tile.HasPiece()) {
			if (activePiece == null) {
                // Debug.Log(tile.ToString());
                activePiece = tile.GetPiece();
                mouseMovement.SetSelectedPiece(activePiece);
			}
			else {

				// If the same tile was clicked
				if (activePiece.GetTile().Equals(tile)) {
                    DeselectPiece();
                }

                // If the piece color of the active piece matches the clicked piece, swap active pieces
                else if (activePiece.GetColor() == tile.GetPiece().GetComponent<Piece>().GetColor()) {
                    // Debug.Log(tile.ToString());
                    mouseMovement.RemoveSelectedPiece();
                    activePiece = tile.GetPiece();
					mouseMovement.SetSelectedPiece(activePiece);
                }

				// If the clicked tile is a valid movement for piece
				else if (activePiece.IsValidMove(tile) && OpenPathTo(tile)) {
                    if (tile.GetPiece().GetPieceType() != Piece.PieceType.KING)
						audioSource.PlayOneShot(capturePiece);
					GameManager.PieceCaptured(tile.GetPiece());
                    activePiece.MovePiece(tile);
                    DeselectPiece();
					TurnManager.ChangeTurn();
                }

				// Tile clicked is not valid movement for piece
				else {
					audioSource.PlayOneShot(invalidMove[rand.Next(0, invalidMove.Count)]);
                    DeselectPiece();
                }
			}
		}
		else {
			if (activePiece != null) {
				if (activePiece.IsValidMove(tile) && OpenPathTo(tile)) {
                    // audioSource.PlayOneShot(validMove);
                    activePiece.MovePiece(tile);
                    DeselectPiece();
					TurnManager.ChangeTurn();
                }
				else {
					audioSource.PlayOneShot(invalidMove[rand.Next(0, invalidMove.Count)]);
					DeselectPiece();
				}
			}
		}
	}

	// Checks that pieces aren't moving through other pieces
	private bool OpenPathTo(Tile tile) {
		if (activePiece == null) return false;
        Piece.PieceType type = activePiece.GetPieceType();
		Location from = activePiece.GetTile().GetLocation();
		Location to   = tile.GetLocation();
        if (Logic.IsOneTileAway(from, to)) return true;

        bool canMoveTo;
        bool horizontalMove = from.letter - to.letter != 0;

        switch(type) {
			case Piece.PieceType.PAWN:
			case Piece.PieceType.ROOK:
				canMoveTo = Logic.CanMoveStraight(from, to, horizontalMove, tiles);
				break;

			case Piece.PieceType.BISHOP:
				canMoveTo = Logic.CanMoveDiagonal(from, to, tiles);
				break;

			case Piece.PieceType.QUEEN:
				bool diagonal = Math.Abs(from.letter - to.letter) == Math.Abs(from.number - to.number);
				if (diagonal) canMoveTo = Logic.CanMoveDiagonal(from, to, tiles);
				else canMoveTo = Logic.CanMoveStraight(from, to, horizontalMove, tiles);
				break;
			
			default:
				canMoveTo = true;
				break;
		}

		return canMoveTo;
	}

	private void DeselectPiece() {
        mouseMovement.RemoveSelectedPiece();
        activePiece = null;
	}

	public void InitBoard() {
		tiles = new Dictionary<Location, GameObject>();
		InitTiles();
	}

	public Dictionary<Location, GameObject> GetTiles() { return tiles; }

	private void InitTiles() {
		float x = -20;
		float y = -20;
		bool dark = true;
		Sprite light_tile = Menus.GetLightTile();
		Sprite dark_tile  = Menus.GetDarkTile();

		foreach (Letter letter in Enum.GetValues(typeof(Letter))) {
			for (int i = 1; i <= 8; ++i) {
				Location location = new Location(letter, i);
				Tile tile = new GameObject().AddComponent<Tile>();
				tile.InitTile(location, this);
				tile.SetCoordinates(x, y);
				if (dark) tile.AddSprite(dark_tile);
				else	  tile.AddSprite(light_tile);				
				dark = !dark;
				
				if (i == 8) {
					y = -20;
					x += 5;
					dark = !dark;
				}
				else {
					y += 5;
				}
				
				tiles.Add(location, tile.gameObject);
			}
		}
	}

	public void PlacePieces(ArrayList allPieces) {
		for (int i = 0; i < allPieces.Count; ++i) {
			Piece piece = (Piece)allPieces[i];
			Location location = piece.GetLocation();
			tiles[location].GetComponent<Tile>().SetPiece(piece);
		}
	}
}
