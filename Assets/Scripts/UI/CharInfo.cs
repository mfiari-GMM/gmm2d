using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class CharInfo : MonoBehaviour
{

    public Text nameText, hpText, mpText, lvlText, expText;
    public Slider expSlider;
    public Image charImage;
   

    public void ShowCharInfo (CharStats playerStats)
    {
        nameText.text = playerStats.charName;
        hpText.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyStringTableCollection", "MENU_HP") + " : " + playerStats.currentHP + "/" + playerStats.maxHP;
        mpText.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyStringTableCollection", "MENU_MP") + " : " + playerStats.currentMP + "/" + playerStats.maxMP;
        lvlText.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyStringTableCollection", "MENU_LVL") + " : " + playerStats.playerLevel;
        expText.text = "" + playerStats.currentEXP + "/" + playerStats.expToNextLevel[playerStats.playerLevel];
        expSlider.maxValue = playerStats.expToNextLevel[playerStats.playerLevel];
        expSlider.value = playerStats.currentEXP;
        charImage.sprite = playerStats.charIamge;
    }
}
