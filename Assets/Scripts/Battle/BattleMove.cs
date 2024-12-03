
[System.Serializable]
public class BattleMove {

    public enum BattleMoveType { Fire, Ice, Water, Wind, Earth, Normal, lightning, light };

    public string moveName;
    public int movePower;
    public int moveCost;
    public AttackEffect theEffect;
    public BattleMoveType battleType;
    public bool isMagic;
    public bool heal;
    public bool isPlayer;
}
