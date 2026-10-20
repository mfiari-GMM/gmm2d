using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class CharItemInfo : MonoBehaviour
{

    public Text nameText, hpText, mpText, lvlText, apteText, strText, strBonusText, defText, defBonusText, magText, magBonusText, resText, resBonusText;
    public Image charImage, statusImage;

    private CharStats playerStats;



    public void ShowCharItemInfo (CharStats playerStats, Item item)
    {

        this.playerStats = playerStats;

        nameText.text = playerStats.charName;
        hpText.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyStringTableCollection", "MENU_HP") + " : " + playerStats.currentHP + "/" + playerStats.maxHP;
        mpText.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyStringTableCollection", "MENU_MP") + " : " + playerStats.currentMP + "/" + playerStats.maxMP;
        lvlText.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyStringTableCollection", "MENU_LVL") + " : " + playerStats.playerLevel;

        strText.text = LocalizationSettings.StringDatabase.GetLocalizedString("UITableCollection", "MENU_STR") + playerStats.strength;
        if (item.isWeapon || item.isArmour)
        {
            int diffStrength = item.weaponStrength - playerStats.equippedWpn.weaponStrength;
            int diffDefence = item.armorStrength - playerStats.equippedWpn.armorStrength;
            int diffMagie = item.weaponMagie - playerStats.equippedWpn.weaponMagie;
            int diffResistance = item.armorResistance - playerStats.equippedWpn.armorResistance;
            ApplyDiff(strBonusText, diffStrength);
            ApplyDiff(defBonusText, diffDefence);
            ApplyDiff(magBonusText, diffMagie);
            ApplyDiff(resBonusText, diffResistance);
            apteText.gameObject.SetActive(true);
            apteText.text = "Apte";
            statusImage.gameObject.SetActive(false);
        } else
        {
            strBonusText.gameObject.SetActive(false);
            defBonusText.gameObject.SetActive(false);
            magBonusText.gameObject.SetActive(false);
            resBonusText.gameObject.SetActive(false);

            apteText.gameObject.SetActive(false);

            if (playerStats.battleStatus == BattleMove.BattleStatus.Poison)
            {
                statusImage.gameObject.SetActive(true);
            }
            else
            {
                statusImage.gameObject.SetActive(false);
            }
        }
        defText.text = LocalizationSettings.StringDatabase.GetLocalizedString("UITableCollection", "MENU_DEF") + playerStats.defence;
        magText.text = LocalizationSettings.StringDatabase.GetLocalizedString("UITableCollection", "MENU_MAG") + playerStats.magie;
        resText.text = LocalizationSettings.StringDatabase.GetLocalizedString("UITableCollection", "MENU_RES") + playerStats.resistance;
        charImage.sprite = playerStats.charIamge;
        
    }

    private void ApplyDiff (Text bonusText, int diff)
    {
        if (diff > 0)
        {
            bonusText.text = "+" + diff;
            bonusText.color = Color.green;
        }
        else if (diff < 0)
        {
            bonusText.text = diff.ToString();
            bonusText.color = Color.red;
        }
        else
        {
            bonusText.text = "+" + diff;
            bonusText.color = Color.white;
        }
        bonusText.gameObject.SetActive(true);
    }

    public void SelectChar ()
    {
        GameMenu.instance.UseItem(this.playerStats);
    } 
}
