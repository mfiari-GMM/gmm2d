using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestSceneTransition : MonoBehaviour
{

    public string questToCheck;

    private bool initialCheckDone;

    public bool shouldCompleteQuest;
    public string QuestToComplete;


    public float waitToLoad = 1f;

    public string areaToLoad;

    public string areaTransitionName;

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
            StartCoroutine(StartSceneTransition());
        }
    }

    public IEnumerator StartSceneTransition()
    {
        UIFade.instance.FadeToBlack();

        GameManager.instance.fadingBetweenAreas = true;

        PlayerController.instance.areaTransitionName = areaTransitionName;

        yield return new WaitForSeconds(waitToLoad);

        SceneManager.LoadScene(areaToLoad);

        if (shouldCompleteQuest)
        {
            QuestManager.instance.MarkQuestComplete(QuestToComplete);
        }
    }
}
