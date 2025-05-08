using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;

public class MainMenu : MonoBehaviour {

    public string newGameScene;

    public GameObject continueButton;

    public string loadGameScene;

    public Button frButton;
    public Button enButton;

    // Use this for initialization
    void Start () {
		if(PlayerPrefs.HasKey("Current_Scene"))
        {
            continueButton.SetActive(true);
        } else
        {
            continueButton.SetActive(false);
        }
        SwitchToFr();
        AudioManager.instance.PlayBGM(4);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Continue()
    {
        SceneManager.LoadScene(loadGameScene);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(newGameScene);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Credit()
    {
        SceneManager.LoadScene("Credit");
    }

    public void SwitchToFr()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
        frButton.interactable = false;
        enButton.interactable = true;
    }

    public void SwitchToEn()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
        frButton.interactable = true;
        enButton.interactable = false;
    }
}
