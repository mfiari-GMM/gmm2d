using UnityEngine;
using UnityEngine.UI;

public class BattleReward : MonoBehaviour {

    public static BattleReward instance;

    public Text xpText, moneyText, itemText;
    public GameObject rewardScreen;

    public string[] rewardItems;
    public int xpEarned;
    public int moneyWin;

    public bool markQuestComplete;
    public string questToMark;

	// Use this for initialization
	void Start () {
        instance = this;
	}

    public void OpenRewardScreen(int xp, int money, string[] rewards)
    {
        xpEarned = xp;
        rewardItems = rewards;
        moneyWin = money;

        xpText.text = "Everyone earned " + xpEarned + " xp!";

        moneyText.text = "You Gain " + moneyWin + " gold!";

        itemText.text = "";

        for(int i = 0; i < rewardItems.Length; i++)
        {
            itemText.text += rewards[i] + "\n";
        }

        rewardScreen.SetActive(true);
    }

    public void CloseRewardScreen()
    {
        for(int i = 0; i < GameManager.instance.playerStats.Length; i++)
        {
            if(GameManager.instance.playerStats[i].gameObject.activeInHierarchy && GameManager.instance.playerStats[i].currentHP > 0)
            {
                GameManager.instance.playerStats[i].AddExp(xpEarned);
            }
        }

        GameManager.instance.AddMoney(moneyWin);

        for (int i = 0; i < rewardItems.Length; i++)
        {
            GameManager.instance.AddItem(rewardItems[i]);
        }

        rewardScreen.SetActive(false);
        GameManager.instance.battleActive = false;

        if(markQuestComplete)
        {
            QuestManager.instance.MarkQuestComplete(questToMark);
        }
    }
}
