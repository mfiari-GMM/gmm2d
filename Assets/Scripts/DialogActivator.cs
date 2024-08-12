using UnityEngine;

public class DialogActivator : MonoBehaviour {

    public string[] lines;

    private bool canActivate;

    public bool isPerson = true;

    public bool shouldActivateQuest;
    public string questToMark;
    public bool markComplete;
    public bool shouldHealPlayer;
    public string[] playersToAdd;
    public string[] playersToRemove;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(canActivate && Input.GetButtonDown("Fire1") && !DialogManager.instance.dialogBox.activeInHierarchy && !GameManager.instance.battleActive)
        {
            DialogManager.instance.ShowDialog(lines, isPerson);
            DialogManager.instance.ShouldActivateQuestAtEnd(questToMark, markComplete);
            DialogManager.instance.ShouldHealPlayerAtEnd(shouldHealPlayer);
            DialogManager.instance.ShouldAddPlayerAtEnd(playersToAdd);
            DialogManager.instance.ShouldRemovePlayerAtEnd(playersToRemove);
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
