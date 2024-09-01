
[System.Serializable]
public class BattleMove {

    public enum BattleMoveType { Fire, Ice, Water, Wind, Earth, Normal, lightning };

    public string moveName;
    public int movePower;
    public int moveCost;
    public AttackEffect theEffect;
    public BattleMoveType battleType;
}
