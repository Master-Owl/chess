using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    private Location location = null;
	private int x;
	private int y;
	public Piece currentPiece = null;

	public void InitTile(Location location) {
		this.location = location;
	}

	public void SetCoordinates(int x, int y) {
		transform.position = new Vector2(x, y);
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
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetPiece(Piece piece) { this.currentPiece = piece; }

	public bool HasPiece() { return currentPiece != null; }

	public Piece GetPiece() { return currentPiece; }

	public Location GetLocation() { return location; }

	override public string ToString() {
		return "Tile [" + location.ToString() + "] | " +
			"X: " + transform.position.x + "  Y: " + transform.position.y;
	}
}