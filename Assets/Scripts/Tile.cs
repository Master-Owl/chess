using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    private Location location = null;
	private GameBoard gameBoard = null;
	private Rect boundingBox;
	private int x;
	private int y;
	public Piece currentPiece = null;

	public void InitTile(Location location, GameBoard boardInstance) {
		this.location = location;
		this.gameBoard = boardInstance;
	}

	public void SetCoordinates(float x, float y) {
		transform.parent = gameBoard.transform;
		transform.position = new Vector2(x, y);
		boundingBox = new Rect(x - 2.5f, y - 2.5f, 5, 5);
	}

	public void AddSprite(Sprite sprite) {
		SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
		spriteRenderer.sprite = sprite;
		spriteRenderer.sortingLayerName = "Game Board";
	}

	void Awake() {
		gameObject.name = "Tile";
		gameObject.tag = "Tile";
	}

	void OnEnable() {
		EventManager.OnTileClicked += Clicked;
	}

	void OnDisable() {
		EventManager.OnTileClicked -= Clicked;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Clicked() {
		Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		if (boundingBox.Contains(point)) {
			gameBoard.TileClicked(this);
		}
	}

	public void SetPiece(Piece piece) {
		this.currentPiece = piece;
	}

	public void RemovePiece() {
		this.currentPiece = null;
	}

	public bool HasPiece() { return currentPiece != null; }

	public Piece GetPiece() { return currentPiece; }

	public Location GetLocation() { return location; }

	override public string ToString() {
		return "Tile [" + location.ToString() + "]";
	}
}