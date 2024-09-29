using UnityEngine;
using UnityEngine.UI;

public class BattleObjectSelect : MonoBehaviour
{

    public string itemName;
    public Text nameText;
    public Text quantityText;

    public void Press()
    {
        BattleManager.instance.objectMenu.SetActive(false);
        BattleManager.instance.OpenPlayersMenu(itemName);
    }
}
