using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

    public Text dialogText;
    public Text nameText;
    public GameObject dialogBox;
    public GameObject nameBox;

    public string[] dialogLines;

    public int currentLine;
    private bool justStarted;

    public static DialogManager instance;

    private string questToMark;
    private bool markQuestComplete;
    private bool shouldMarkQuest;
    private bool shouldHealPlayers;

    public string[] playersToAdd;
    public string[] playersToRemove;

    // Use this for initialization
    void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
        if(dialogBox.activeInHierarchy && !GameManager.instance.battleActive)
        {
            if(Input.GetButtonUp("Fire1"))
            {
                if (!justStarted)
                {
                    currentLine++;

                    if (currentLine >= dialogLines.Length)
                    {

                        string playerText = "";
                        bool isTextDisplayed = false;

                        if (playersToAdd.Length > 0)
                        {
                            dialogBox.SetActive(true);
                            nameBox.SetActive(false);
                            for (int i = 0; i < playersToAdd.Length; i++)
                            {
                                GameManager.instance.AddPlayer(playersToAdd[i]);
                                playerText += playersToAdd[i] + " a rejoins le groupe \n";
                            }
                            dialogText.text = playerText;
                            playersToAdd = new string[0];
                            isTextDisplayed = true;
                        }

                        if (playersToRemove.Length > 0)
                        {
                            dialogBox.SetActive(true);
                            nameBox.SetActive(false);
                            for (int i = 0; i < playersToRemove.Length; i++)
                            {
                                GameManager.instance.RemovePlayer(playersToRemove[i]);
                                playerText += playersToRemove[i] + " a quitter le groupe \n";
                            }
                            dialogText.text = playerText;
                            dialogBox.SetActive(true);
                            playersToRemove = new string[0];
                            isTextDisplayed = true;
                        }

                        if(!isTextDisplayed)
                        {
                            dialogBox.SetActive(false);

                            GameManager.instance.dialogActive = false;

                            if (shouldMarkQuest)
                            {
                                shouldMarkQuest = false;
                                if (markQuestComplete)
                                {
                                    QuestManager.instance.MarkQuestComplete(questToMark);
                                }
                                else
                                {
                                    QuestManager.instance.MarkQuestIncomplete(questToMark);
                                }
                            }
                            if (shouldHealPlayers)
                            {
                                GameManager.instance.HealPlayers();
                            }
                        }
                        
                    }
                    else
                    {
                        CheckIfName();

                        dialogText.text = dialogLines[currentLine];
                    }
                } else
                {
                    justStarted = false;
                }

                
            }
        }

	}

    public void ShowDialog(string[] newLines, bool isPerson)
    {
        dialogLines = newLines;

        currentLine = 0;

        CheckIfName();

        dialogText.text = dialogLines[currentLine];
        dialogBox.SetActive(true);

        justStarted = true;

        nameBox.SetActive(isPerson);

        GameManager.instance.dialogActive = true;
    }

    public void CheckIfName()
    {
        if(dialogLines[currentLine].StartsWith("n-"))
        {
            nameText.text = dialogLines[currentLine].Replace("n-", "");
            currentLine++;
        }
    }

    public void ShouldActivateQuestAtEnd(string questName, bool markComplete)
    {
        questToMark = questName;
        markQuestComplete = markComplete;

        shouldMarkQuest = questName != null && questName.Length > 0;
    }

    public void ShouldHealPlayerAtEnd(bool healPlayer)
    {
        shouldHealPlayers = healPlayer;
    }

    public void ShouldAddPlayerAtEnd(string[] playersToAdd)
    {
        this.playersToAdd = playersToAdd;
    }

    public void ShouldRemovePlayerAtEnd(string[] playersToRemove)
    {
        this.playersToRemove = playersToRemove;
    }
}
