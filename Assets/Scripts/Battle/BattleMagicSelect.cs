using UnityEngine;
using UnityEngine.UI;

public class BattleMagicSelect : MonoBehaviour {

    public string spellName;
    public int spellCost;
    public Text nameText;
    public Text costText;

    public void Press()
    {

        if (spellName == "TRIPLE_POTION" && GameManager.instance.GetItemQuantity("HEALTH_POTION") < 3)
        {
            //let player know there is not enough potion
            BattleManager.instance.battleNotice.theText.text = "pas assez de potion!";
            BattleManager.instance.battleNotice.Activate();
            BattleManager.instance.magicMenu.SetActive(false);
        } else if (BattleManager.instance.activeBattlers[BattleManager.instance.currentTurn].currentMP >= spellCost)
        {
            BattleManager.instance.magicMenu.SetActive(false);
            BattleManager.instance.SelectMove(spellName);
        } else
        {
            //let player know there is not enough MP
            BattleManager.instance.battleNotice.theText.text = "pas assez de PM!";
            BattleManager.instance.battleNotice.Activate();
            BattleManager.instance.magicMenu.SetActive(false);
        }
    }
}
