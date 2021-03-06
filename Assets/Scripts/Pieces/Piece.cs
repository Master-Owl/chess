using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour {
    public enum PieceType { PAWN, KNIGHT, BISHOP, ROOK, QUEEN, KING };
   
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
        this.tile.RemovePiece();
        this.tile = tile; 
        this.tile.SetPiece(this);
    }

    public Tile GetTile() { return tile; }

    public Player.PlayerColor GetColor() { return pieceColor; }

    public Location GetLocation() { return this.tile.GetLocation(); }

    override public string ToString() {
        return pieceColor.ToString() + " " + type.ToString();
    }

    override public bool Equals(object obj) {
        if (!(obj is Piece)) return false;
        Piece other = (Piece)obj;

        if (!this.pieceColor.Equals(other.pieceColor)) return false;
        if (!this.type.Equals(other.type)) return false;
        if (!this.tile.Equals(other.tile)) return false;
        return true;
    }

    override public int GetHashCode(){
        return 7 * (int)type + 9 * (int)pieceColor + (tile == null ? 0 : tile.GetLocation().GetHashCode());
    }
}