[System.Serializable]
public class SpeedTypeUpgrade { 
    public int level;
    public int wordsPerKeystroke;
    public int cost;

    public SpeedTypeUpgrade(int level, int wordsPerKeystroke, int cost) {
        this.level = level;
        this.wordsPerKeystroke = wordsPerKeystroke;
        this.cost = cost;
    }
}