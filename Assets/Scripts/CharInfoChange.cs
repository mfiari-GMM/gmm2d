using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharInfoChange : MonoBehaviour
{

    private int playerIndex = 0;
    private bool isSelected = false;

    public void SetPlayerIndex (int index)
    {
        this.playerIndex = index;
    }

    public void ResetDisplay ()
    {
        isSelected = false;
        gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }


    public void CurrentClickedGameObject()
    {
        if (!isSelected)
        {
            if (GameMenu.instance.playerToChange == -1)
            {
                isSelected = true;
                gameObject.GetComponent<Image>().color = new Color32(211, 17, 26, 255);
                GameMenu.instance.playerToChange = playerIndex;
            } else
            {
                CharStats tmpStat = GameManager.instance.playerStats[GameMenu.instance.playerToChange];
                GameManager.instance.playerStats[GameMenu.instance.playerToChange] = GameManager.instance.playerStats[playerIndex];
                GameManager.instance.playerStats[playerIndex] = tmpStat;
                GameMenu.instance.UpdateMainStats();
                GameMenu.instance.playerToChange = -1;
            }
            
        } else
        {
            ResetDisplay();
            GameMenu.instance.playerToChange = -1;
        }
    }
}
