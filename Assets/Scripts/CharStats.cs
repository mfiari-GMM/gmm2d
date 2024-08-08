using UnityEngine;

public class CharStats : MonoBehaviour {

    public string charName;
    public int playerLevel = 1;
    public int currentEXP;
    public int[] expToNextLevel;
    public int maxLevel = 100;
    public int baseEXP = 1000;

    public int currentHP;
    public int maxHP = 100;
    public int currentMP;
    public int maxMP = 30;
    public int[] mpLvlBonus;
    public int strength;
    public int defence;
    public int magie;
    public int resistance;
    public Item equippedWpn;
    public Item equippedArmr;
    public Sprite charIamge;


    public int winHP;
    public int winMP;
    public int winStrength;
    public int winDefence;
    public int winMagie;
    public int winResistance;

    // Use this for initialization
    void Start () {
        expToNextLevel = new int[maxLevel];
        expToNextLevel[1] = baseEXP;

        for(int i = 2; i < expToNextLevel.Length; i++)
        {
            expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.05f);
        }
	}

    public void AddExp(int expToAdd)
    {
        currentEXP += expToAdd;

        if (playerLevel < maxLevel)
        {
            while (currentEXP > expToNextLevel[playerLevel])
            {
                currentEXP -= expToNextLevel[playerLevel];

                playerLevel++;

                maxHP += winHP;
                maxMP += winMP;
                strength += winStrength;
                defence += winDefence;
                magie += winMagie;
                resistance += winResistance;
            }
        }

        if(playerLevel >= maxLevel)
        {
            currentEXP = 0;
        }
    }
}
