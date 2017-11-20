public class Location {
    public Letter letter;
    public int number;

    public Location(Letter letter, int number) {
        this.letter = letter;
        this.number = number;
    }

    override public bool Equals(object obj) {
        if (this == obj) return true;
        if (!(obj is Location)) return false;
        Location l = (Location)obj;
        return this.letter == l.letter
            && this.number == l.number;
    }

    override public int GetHashCode() {
        return 7 * (int)letter + 9 * (int)number;
    }

    override public string ToString() {
        return letter.ToString() + ", " + number.ToString();
    }
}

public enum Letter { A, B, C, D, E, F, G, H };
