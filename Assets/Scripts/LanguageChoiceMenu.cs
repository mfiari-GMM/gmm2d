using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;

public class LanguageChoiceMenu : MonoBehaviour {
    

    public void SwitchToFr()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
        PlayerPrefs.SetString("language", LocalizationSettings.SelectedLocale.LocaleName);
        SceneManager.LoadScene("MainMenu");
    }

    public void SwitchToEn()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
        PlayerPrefs.SetString("language", LocalizationSettings.SelectedLocale.LocaleName);
        SceneManager.LoadScene("MainMenu");
    }
}
