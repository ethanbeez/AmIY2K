[System.Serializable]
public class AutoSendUpgrade {
    public int level;
    public int sendIntervalSeconds;
    public int sendBurstCount;
    public int cost;

    public AutoSendUpgrade(int level, int sendIntervalSeconds, int sendBurstCount, int cost) {
        this.level = level;
        this.sendIntervalSeconds = sendIntervalSeconds;
        this.sendBurstCount = sendBurstCount;
        this.cost = cost;
    }
}