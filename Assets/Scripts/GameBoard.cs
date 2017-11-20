using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour {

	private Dictionary<Location, GameObject> tiles;
	private AudioClip validMove = null;
	private List<AudioClip> invalidMove = new List<AudioClip>();
	private Piece activePiece = null;
	private System.Random rand = new System.Random();
	private MouseMovement mouseMovement = null;
	private AudioSource audioSource = null;

	// Use this for initialization
	void Start () {
		gameObject.name = "Game Board";
		gameObject.tag = "Game Board";
		gameObject.transform.parent = Camera.main.transform;
		mouseMovement = gameObject.AddComponent<MouseMovement>();
		audioSource = gameObject.AddComponent<AudioSource>();
		validMove = Resources.Load<AudioClip>("Sounds/test_click");
		invalidMove.Add(Resources.Load<AudioClip>("Sounds/no_1"));
		invalidMove.Add(Resources.Load<AudioClip>("Sounds/no_2"));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TileClicked(Tile tile) {
		Debug.Log(tile.ToString());
		if (tile.HasPiece()) {
			if (activePiece == null) {
				activePiece = tile.GetPiece();
				mouseMovement.SetSelectedPiece(activePiece);
				Debug.Log(activePiece);
			}
			else {
                // If the piece color of the active piece matches the clicked piece, swap active pieces
                if (activePiece.GetColor() == tile.GetPiece().GetComponent<Piece>().GetColor()) {
					mouseMovement.RemoveSelectedPiece();
                    activePiece = tile.GetPiece();
					mouseMovement.SetSelectedPiece(activePiece);
                    Debug.Log("Changed Active: " + activePiece.ToString());
                }
				else if (activePiece.IsValidMove(tile)) {
					mouseMovement.RemoveSelectedPiece();
					activePiece.MovePiece(tile);
                    activePiece = null;
                    audioSource.PlayOneShot(validMove);
                }
				else {
					audioSource.PlayOneShot(invalidMove[rand.Next(0, invalidMove.Count)]);
				}
			}
		}
		else {
			if (activePiece != null) {
				if (activePiece.IsValidMove(tile)) {
					mouseMovement.RemoveSelectedPiece();
					activePiece.MovePiece(tile);
					Debug.Log("Moved " + activePiece.ToString() + " to " + tile.ToString());
                    activePiece = null;
                    audioSource.PlayOneShot(validMove);
                }
				else {
					audioSource.PlayOneShot(invalidMove[rand.Next(0, invalidMove.Count)]);
				}
			}
		}
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
		Sprite light_tile = Resources.Load<Sprite>("Graphics/Tiles/tile-6");
		Sprite dark_tile  = Resources.Load<Sprite>("Graphics/Tiles/tile-2");

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
