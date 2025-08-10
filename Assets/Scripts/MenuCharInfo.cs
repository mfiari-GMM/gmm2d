using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;

public class MenuCharInfo : MonoBehaviour
{

    private CharStats playerStats;

    public Text nameText, hpText, mpText, lvlText, expText;
    public Slider expSlider;
    public Image charImage;



    public void DipslayInfo(CharStats playerStat)
    {
        if (playerStat.gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);

            nameText.text = playerStat.charName;
            hpText.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyStringTableCollection", "MENU_HP") + " : " + playerStat.currentHP + "/" + playerStat.maxHP;
            mpText.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyStringTableCollection", "MENU_MP") + " : " + playerStat.currentMP + "/" + playerStat.maxMP;
            lvlText.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyStringTableCollection", "MENU_LVL") + " : " + playerStat.playerLevel;
            expText.text = "" + playerStat.currentEXP + "/" + playerStat.expToNextLevel[playerStat.playerLevel];
            expSlider.maxValue = playerStat.expToNextLevel[playerStat.playerLevel];
            expSlider.value = playerStat.currentEXP;
            charImage.sprite = playerStat.charIamge;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
