using UnityEngine;

public class QuestObjectActivator : MonoBehaviour {

    public GameObject[] objectsToActivate;

    public Item[] objectsToGain;

    public int moneyToGain;

    public int expToGain;

    public string questToCheck;

    public bool activeIfComplete;

    private bool initialCheckDone;
	
	// Update is called once per frame
	void Update () {
		if(!initialCheckDone)
        {
            initialCheckDone = true;

            CheckCompletion();
        }
	}

    public void CheckCompletion()
    {
        if(QuestManager.instance != null && QuestManager.instance.CheckIfComplete(questToCheck))
        {
            for (int i = 0; i < objectsToActivate.Length; i++)
            {
                Debug.Log(objectsToActivate[i].name + (activeIfComplete ? "active" : "unactive"));
                objectsToActivate[i].SetActive(activeIfComplete);
            }
            
        }
    }
}
