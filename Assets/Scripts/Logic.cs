using System;
using System.Collections.Generic;
using UnityEngine;

public class Logic {

	public static bool CanMoveStraight(Location from, Location to, bool horizontalMove, Dictionary<Location, GameObject> tiles) {
        if (horizontalMove) {
            int start = Math.Min((int)to.letter, (int)from.letter);
            int end   = Math.Max((int)to.letter, (int)from.letter);
            for (int idx = start + 1; idx < end; ++idx) {
                Location loc = new Location((Letter)idx, from.number);
                Tile t = tiles[loc].GetComponent<Tile>();
                if (t.HasPiece()) return false;
                
            }
        }
        else {
            int start = Math.Min(to.number, from.number);
            int end   = Math.Max(to.number, from.number);
            for (int idx = start + 1; idx < end; ++idx) {
                Location loc = new Location(from.letter, idx);
                Tile t = tiles[loc].GetComponent<Tile>();
                if (t.HasPiece()) return false;
            }
        }
		return true;
	}

	public static bool CanMoveDiagonal(Location from, Location to, Dictionary<Location, GameObject> tiles) {
		bool up 	= to.number - from.number > 0;
		bool right  = to.letter - from.letter > 0;

		if (up) {
			if (right) {
				int letterIdx = (int)from.letter + 1;
				for (int idx = from.number + 1; idx < to.number; ++idx) {
                    Location loc = new Location((Letter)letterIdx++, idx);
                    Tile t = tiles[loc].GetComponent<Tile>();
                    if (t.HasPiece()) return false;
				}
			}
			else {
				int letterIdx = (int)from.letter - 1;
				for (int idx = from.number + 1; idx < to.number; ++idx) {
                    Location loc = new Location((Letter)letterIdx--, idx);
                    Tile t = tiles[loc].GetComponent<Tile>();
                    if (t.HasPiece()) return false;
				}
			}
		} 
		else {
            if (right) {
                int letterIdx = (int)from.letter + 1;
                for (int idx = from.number - 1; idx > to.number; --idx) {
                    Location loc = new Location((Letter)letterIdx++, idx);
                    Tile t = tiles[loc].GetComponent<Tile>();
                    if (t.HasPiece()) return false;
                }
            }
            else {
                int letterIdx = (int)from.letter - 1;
                for (int idx = from.number - 1; idx > to.number; --idx) {
                    Location loc = new Location((Letter)letterIdx--, idx);
                    Tile t = tiles[loc].GetComponent<Tile>();
                    if (t.HasPiece()) return false;
                }
            }
		}
		return true;
	}

	public static bool IsOneTileAway(Location from, Location to) {
        int letterDist = Math.Abs(to.letter - from.letter);
        int numberDist = Math.Abs(to.number - from.number);
        return (letterDist <= 1 && numberDist <= 1);
	}
}
