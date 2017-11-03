using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour {

	private Dictionary<Location, GameObject> tiles;

	// Use this for initialization
	void Start () {
		gameObject.name = "Game Board";
		gameObject.tag = "Game Board";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void InitBoard() {
		tiles = new Dictionary<Location, GameObject>();
		InitTiles();
	}

	public Dictionary<Location, GameObject> GetTiles() { return tiles; }

	private void InitTiles() {
		int x = -20;
		int y = -20;
		bool dark = true;
		Sprite light_tile = Resources.Load<Sprite>("Graphics/Tiles/tile-6");
		Sprite dark_tile  = Resources.Load<Sprite>("Graphics/Tiles/tile-2");

		foreach (Letter letter in Enum.GetValues(typeof(Letter))) {
			for (int i = 1; i <= 8; ++i) {
				Location location = new Location(letter, i);
				Tile tile = new GameObject().AddComponent<Tile>();
				tile.InitTile(location);
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
