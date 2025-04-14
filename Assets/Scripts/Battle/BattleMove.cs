
[System.Serializable]
public class BattleMove {

    public enum BattleMoveType { Fire, Ice, Water, Wind, Earth, Normal, lightning, light, Boost };

    public string moveCode;
    public string moveName;
    public int movePower;
    public int moveCost;
    public AttackEffect theEffect;
    public BattleMoveType battleType;
    public bool isMagic;
    public bool heal;
    public bool isPlayer;
    public bool allChar;
    public bool boostAtk;
    public bool boostDef;
    public bool boostMagie;
    public bool boostRes;
    public bool boostCritic;
    public bool boosteDodge;
}
