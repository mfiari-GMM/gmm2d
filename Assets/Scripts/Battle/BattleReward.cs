using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;
using Random = UnityEngine.Random;
using System;
using System.Collections;

public class BattleReward : MonoBehaviour {

    public static BattleReward instance;

    public Text xpText, moneyText, itemText;
    public GameObject rewardScreen, rewardExpScreen;
    public MenuCharInfo[] menuCharInfos;

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
        moneyWin = money;

        rewardItems = new Item[rewards.Length];

        for (int i = 0; i < rewards.Length; i++)
        {
            int rand = Random.Range(0, 1);
            if (rand == 0)
            {
                rewardItems[i] = rewards[i];
            } else
            {
                rewardItems[i] = null;
            }
        }

        xpText.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyStringTableCollection", "EXP_WIN") + xpEarned + " exp!";

        moneyText.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyStringTableCollection", "GOLD_WIN") + moneyWin + " gold!";

        itemText.text = "";

        for(int i = 0; i < rewardItems.Length; i++)
        {
            if (rewardItems[i] != null)
            {
                itemText.text += rewardItems[i].itemName + "\n";
            }
        }

        rewardScreen.SetActive(true);
    }

    public void ContinueRewardScreen ()
    {
        rewardScreen.SetActive(false);
        rewardExpScreen.SetActive(true);
        StartCoroutine(DisplayRewardExp());
    }

    private IEnumerator DisplayRewardExp()
    {
        for (int i = 0; i < GameManager.instance.playerStats.Length; i++)
        {
            menuCharInfos[i].DipslayInfo(GameManager.instance.playerStats[i]);
        }
        yield return new WaitForSeconds(2f);
        int nbPlayerHasWinExp = 0;
        for (int i = 0; i < GameManager.instance.playerStats.Length; i++)
        {
            CharStats charStats = GameManager.instance.playerStats[i];
            if (charStats.gameObject.activeInHierarchy && charStats.currentHP > 0)
            {
                int levelWin = charStats.AddExp(nbPlayerHasWinExp >= 3 ? xpEarned / 2 : xpEarned);
                nbPlayerHasWinExp++;
                if (levelWin > 0)
                {
                    for (int j = 0; j < charStats.winMoves.Length; j++)
                    {
                        if (charStats.winMoves[j].level == charStats.playerLevel)
                        {
                            Debug.Log(charStats.winMoves[j].moveName);
                        }
                    }
                }
            }
        }
        for (int i = 0; i < GameManager.instance.playerStats.Length; i++)
        {
            menuCharInfos[i].DipslayInfo(GameManager.instance.playerStats[i]);
        }
    }

    public void CloseRewardScreen()
    {

        GameManager.instance.AddMoney(moneyWin);

        for (int i = 0; i < rewardItems.Length; i++)
        {
            if (rewardItems[i] != null)
            {
                GameManager.instance.AddItem(rewardItems[i].itemCode);
            }
        }

        rewardExpScreen.SetActive(false);
        GameManager.instance.battleActive = false;

        if(markQuestComplete)
        {
            QuestManager.instance.MarkQuestComplete(questToMark);
        }

        AudioManager.instance.PlayBGM(FindObjectOfType<CameraController>().musicToPlay);
    }
}
