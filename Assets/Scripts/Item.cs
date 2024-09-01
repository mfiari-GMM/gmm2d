using UnityEngine;

public class Item : MonoBehaviour {
    [Header("Item Type")]
    public bool isItem;
    public bool isWeapon;
    public bool isArmour;

    [Header("Item Details")]
    public string itemName;
    public string description;
    public int value;
    public Sprite itemSprite;

    [Header("Item Details")]
    public int amountToChange;
    public bool affectHP, affectMP, affectStr, resurrect;

    [Header("Weapon/Armor Details")]
    public int weaponStrength;
    public int armorStrength;
    public int weaponMagie;
    public int armorResistance;


    public void Use(int charToUseOn)
    {
        CharStats selectedChar = GameManager.instance.playerStats[charToUseOn];

        if(isItem)
        {
            if(affectHP)
            {
                if (selectedChar.currentHP <= 0)
                {
                    return;
                }
                if (selectedChar.currentHP >= selectedChar.maxHP)
                {
                    return;
                }
                selectedChar.currentHP += amountToChange;

                if(selectedChar.currentHP > selectedChar.maxHP)
                {
                    selectedChar.currentHP = selectedChar.maxHP;
                }
            }

            if(affectMP)
            {
                if (selectedChar.currentHP <= 0)
                {
                    return;
                }
                if (selectedChar.currentMP >= selectedChar.maxMP)
                {
                    return;
                }
                selectedChar.currentMP += amountToChange;

                if (selectedChar.currentMP > selectedChar.maxMP)
                {
                    selectedChar.currentMP = selectedChar.maxMP;
                }
            }

            if(affectStr)
            {
                selectedChar.strength += amountToChange;
            }

            if (resurrect)
            {
                if (selectedChar.currentHP <= 0)
                {
                    selectedChar.currentHP = selectedChar.maxHP / 2;
                }
            }
        }

        if(isWeapon)
        {
            if(selectedChar.equippedWpn != null)
            {
                GameManager.instance.AddItem(selectedChar.equippedWpn.itemName);
            }

            selectedChar.equippedWpn = this;
        }

        if(isArmour)
        {
            if (selectedChar.equippedArmr != null)
            {
                GameManager.instance.AddItem(selectedChar.equippedArmr.itemName);
            }

            selectedChar.equippedArmr = this;
        }

        GameManager.instance.RemoveItem(itemName);
    }
}
