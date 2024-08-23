using UnityEngine;

public class DialogActivator : MonoBehaviour {

    public string[] lines;

    public bool isPerson = true;

    public bool shouldActivateQuest;
    public string questToMark;
    public bool markComplete;
    public bool shouldHealPlayer;
    public bool shouldAutoLaunch;
    public string[] playersToAdd;
    public string[] playersToRemove;

    private bool canActivate;
    private bool hasBeenActivated = false;

    // Update is called once per frame
    void Update () {
		if(canActivate && ((shouldAutoLaunch && !hasBeenActivated) || Input.GetButtonDown("Fire1")) && !DialogManager.instance.dialogBox.activeInHierarchy && !GameManager.instance.battleActive)
        {
            DialogManager.instance.ShowDialog(lines, isPerson);
            DialogManager.instance.ShouldActivateQuestAtEnd(questToMark, markComplete);
            DialogManager.instance.ShouldHealPlayerAtEnd(shouldHealPlayer);
            DialogManager.instance.ShouldAddPlayerAtEnd(playersToAdd);
            DialogManager.instance.ShouldRemovePlayerAtEnd(playersToRemove);
            hasBeenActivated = true;
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            canActivate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canActivate = false;
        }
    }
}
