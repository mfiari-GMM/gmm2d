using UnityEngine;
using UnityEngine.UI;

public class BattleTargetButton : MonoBehaviour {

    public string moveName;
    public int activeBattlerTarget;
    public Text targetName;

    public bool isItem;
    public string itemName;

    public void Press()
    {
        if (isItem)
        {
            BattleManager.instance.UseItem(itemName, activeBattlerTarget);
        } else
        {
            BattleManager.instance.PlayerAttack(moveName, activeBattlerTarget);
        }
        
    }
}
