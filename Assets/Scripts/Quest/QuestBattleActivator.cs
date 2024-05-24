using System.Collections;
using UnityEngine;

public class QuestBattleActivator : MonoBehaviour
{

    public BattleType battleType;

    public string questToCheck;

    private bool initialCheckDone;

    public bool shouldCompleteQuest;
    public string QuestToComplete;

    // Update is called once per frame
    void Update()
    {
        if (!initialCheckDone)
        {
            CheckCompletion();
        }
    }

    public void CheckCompletion()
    {
        if (QuestManager.instance != null && QuestManager.instance.CheckIfComplete(questToCheck))
        {
            initialCheckDone = true;
            StartCoroutine(StartBattleCo());
        }
    }

    public IEnumerator StartBattleCo()
    {
        UIFade.instance.FadeToBlack();
        GameManager.instance.battleActive = true;

        BattleManager.instance.rewardItems = battleType.rewardItems;
        BattleManager.instance.rewardXP = battleType.rewardXP;

        yield return new WaitForSeconds(1.5f);

        BattleManager.instance.BattleStart(battleType.enemies, true);
        UIFade.instance.FadeFromBlack();

        BattleReward.instance.markQuestComplete = shouldCompleteQuest;
        BattleReward.instance.questToMark = QuestToComplete;
    }
}