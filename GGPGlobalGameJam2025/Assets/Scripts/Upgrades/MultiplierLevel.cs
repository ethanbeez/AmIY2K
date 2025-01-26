[System.Serializable]
public class MultiplierLevel { 
    public int level;
    public int multiplier;
    public int cost;

    public MultiplierLevel(int level, int multiplier, int cost) {
        this.level = level;
        this.multiplier = multiplier;
        this.cost = cost;
    }
}