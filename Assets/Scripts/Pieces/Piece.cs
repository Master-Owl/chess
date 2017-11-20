using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour {
    public enum PieceType { KING, QUEEN, BISHOP, KNIGHT, ROOK, PAWN };
   
    protected Sprite[] sprites = null;
    protected Tile tile;
    protected PieceType type;
    protected Player.PlayerColor pieceColor;
    protected SpriteRenderer spriteRenderer;

    void Awake() {
        gameObject.name = "Piece";
        gameObject.layer = 9;
        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sortingLayerName = "Piece";
        sprites = Resources.LoadAll<Sprite>("Graphics/pieces");
    }

    protected void InitPiece(GameObject tile, PieceType type) {
        this.tile = tile.GetComponent<Tile>();
        this.type = type;
        gameObject.transform.SetParent(tile.transform, false);
        gameObject.transform.localScale = new Vector2(2, 2);
        gameObject.name = type.ToString();
    }

    public PieceType GetPieceType() { return type; }

    public abstract bool IsValidMove(Tile t);

    public virtual void MovePiece(Tile tile) {
        gameObject.transform.SetParent(tile.transform, false);
        gameObject.transform.localPosition = new Vector2(0, 0);
        this.tile = tile; 
        this.tile.SetPiece(this);
    }

    public Tile GetTile() { return tile; }

    public Player.PlayerColor GetColor() { return pieceColor; }

    public Location GetLocation() { return this.tile.GetLocation(); }
}