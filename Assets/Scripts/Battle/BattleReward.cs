using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;

public class BattleReward : MonoBehaviour {

    public static BattleReward instance;

    public Text xpText, moneyText, itemText;
    public GameObject rewardScreen;

    public Item[] rewardItems;
    public int xpEarned;
    public int moneyWin;

    public bool markQuestComplete;
    public string questToMark;

	// Use this for initialization
	void Start () {
        instance = this;
	}

    public void OpenRewardScreen(int xp, int money, Item[] rewards)
    {

        AudioManager.instance.PlayBGM(6);
        xpEarned = xp;
        rewardItems = rewards;
        moneyWin = money;

        xpText.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyStringTableCollection", "EXP_WIN") + xpEarned + " exp!";

        moneyText.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyStringTableCollection", "GOLD_WIN") + moneyWin + " gold!";

        itemText.text = "";

        for(int i = 0; i < rewardItems.Length; i++)
        {
            itemText.text += rewards[i].itemName + "\n";
        }

        rewardScreen.SetActive(true);
    }

    public void CloseRewardScreen()
    {
        int nbPlayerHasWinExp = 0;
        for(int i = 0; i < GameManager.instance.playerStats.Length; i++)
        {
            if(GameManager.instance.playerStats[i].gameObject.activeInHierarchy && GameManager.instance.playerStats[i].currentHP > 0)
            {
                GameManager.instance.playerStats[i].AddExp(nbPlayerHasWinExp >= 3 ? xpEarned / 2 : xpEarned);
                nbPlayerHasWinExp++;
            }
        }

        GameManager.instance.AddMoney(moneyWin);

        for (int i = 0; i < rewardItems.Length; i++)
        {
            GameManager.instance.AddItem(rewardItems[i].itemCode);
        }

        rewardScreen.SetActive(false);
        GameManager.instance.battleActive = false;

        if(markQuestComplete)
        {
            QuestManager.instance.MarkQuestComplete(questToMark);
        }

        AudioManager.instance.PlayBGM(FindObjectOfType<CameraController>().musicToPlay);
    }
}
