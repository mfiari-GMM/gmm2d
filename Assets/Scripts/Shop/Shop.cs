using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

    public static Shop instance;

    public GameObject shopMenu;
    public GameObject buyMenu;
    public GameObject sellMenu;

    public Text goldText;

    public string[] itemsForSale;

    public ItemButton[] buyItemButtons;
    public ItemButton[] sellItemButtons;

    public Item selectedItem;
    public Text buyItemName, buyItemDescription, buyItemValue, buyItemQuantity, buyItemStrength, buyItemMagie, buyItemDefence, buyItemResistance;
    public Text sellItemName, sellItemDescription, sellItemValue;

	// Use this for initialization
	void Start () {
        instance = this;
	}

    public void OpenShop()
    {
        shopMenu.SetActive(true);
        OpenBuyMenu();

        GameManager.instance.shopActive = true;

        goldText.text = GameManager.instance.currentGold.ToString() + "g";
    }

    public void CloseShop()
    {
        shopMenu.SetActive(false);
        GameManager.instance.shopActive = false;
    }

    public void OpenBuyMenu()
    {
        buyItemButtons[0].Press();

        buyMenu.SetActive(true);
        sellMenu.SetActive(false);

        for (int i = 0; i < buyItemButtons.Length; i++)
        {
            buyItemButtons[i].buttonValue = i;

            if (itemsForSale[i] != "")
            {
                buyItemButtons[i].buttonImage.gameObject.SetActive(true);
                buyItemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(itemsForSale[i]).itemSprite;
                buyItemButtons[i].amountText.text = "";
            }
            else
            {
                buyItemButtons[i].buttonImage.gameObject.SetActive(false);
                buyItemButtons[i].amountText.text = "";
            }
        }
    }

    public void OpenSellMenu()
    {
        sellItemButtons[0].Press();

        buyMenu.SetActive(false);
        sellMenu.SetActive(true);

        ShowSellItems();
        
    }

    private void ShowSellItems()
    {
        GameManager.instance.SortItems();
        for (int i = 0; i < sellItemButtons.Length; i++)
        {
            sellItemButtons[i].buttonValue = i;

            if (GameManager.instance.itemsHeld[i] != "")
            {
                sellItemButtons[i].buttonImage.gameObject.SetActive(true);
                sellItemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;
                sellItemButtons[i].amountText.text = GameManager.instance.numberOfItems[i].ToString();
            }
            else
            {
                sellItemButtons[i].buttonImage.gameObject.SetActive(false);
                sellItemButtons[i].amountText.text = "";
            }
        }
    }

    public void SelectBuyItem(Item buyItem)
    {
        if (buyItem != null)
        {
            selectedItem = buyItem;
            buyItemName.text = selectedItem.itemName;
            buyItemDescription.text = selectedItem.description;
            buyItemValue.text = "Prix : " + selectedItem.value + "g";

            bool isWeaponItem = buyItem.isWeapon || buyItem.isArmour;

            buyItemStrength.gameObject.SetActive(isWeaponItem);
            buyItemMagie.gameObject.SetActive(isWeaponItem);
            buyItemDefence.gameObject.SetActive(isWeaponItem);
            buyItemResistance.gameObject.SetActive(isWeaponItem);

            if (isWeaponItem)
            {
                buyItemStrength.text = "frc : +" + selectedItem.weaponStrength.ToString();
                buyItemMagie.text = "mag : +" + selectedItem.weaponMagie.ToString();
                buyItemDefence.text = "def : +" + selectedItem.armorStrength.ToString();
                buyItemResistance.text = "res : +" + selectedItem.armorResistance.ToString();

            }


            ShowItemQuantity(selectedItem);
        }
    }

    public void SelectSellItem(Item sellItem)
    {
        if (sellItem != null)
        {
            selectedItem = sellItem;
            sellItemName.text = selectedItem.itemName;
            sellItemDescription.text = selectedItem.description;
            sellItemValue.text = "Prix : " + Mathf.FloorToInt(selectedItem.value * .5f).ToString() + "g";
        }
    }

    public void BuyItem()
    {
        if (selectedItem != null)
        {
            if (GameManager.instance.currentGold >= selectedItem.value)
            {
                GameManager.instance.currentGold -= selectedItem.value;

                GameManager.instance.AddItem(selectedItem.itemName);

                ShowItemQuantity(selectedItem);
            }
        }

        goldText.text = GameManager.instance.currentGold.ToString() + "g";
    }

    private void ShowItemQuantity (Item selectedItem)
    {
        int quantity = 0;
        for (int i = 0; i < GameManager.instance.itemsHeld.Length; i++)
        {

            if (GameManager.instance.itemsHeld[i] == selectedItem.itemName)
            {
                quantity = GameManager.instance.numberOfItems[i];
            }
        }

        bool isWeaponItem = selectedItem.isWeapon || selectedItem.isArmour;

        if (isWeaponItem)
        {

            for (int i = 0; i < GameManager.instance.playerStats.Length; i++)
            {

                if (GameManager.instance.playerStats[i].equippedWpn != null
                    && GameManager.instance.playerStats[i].equippedWpn.itemName == selectedItem.itemName)
                {
                    quantity++;
                }
                if (GameManager.instance.playerStats[i].equippedArmr != null
                    && GameManager.instance.playerStats[i].equippedArmr.itemName == selectedItem.itemName)
                {
                    quantity++;
                }
            }

        }
        buyItemQuantity.text = "En stock : " + quantity;
    }

    public void SellItem()
    {
        if(selectedItem != null)
        {
            GameManager.instance.currentGold += Mathf.FloorToInt(selectedItem.value * .5f);

            GameManager.instance.RemoveItem(selectedItem.itemName);
        }

        goldText.text = GameManager.instance.currentGold.ToString() + "g";

        ShowSellItems();
    }
}
