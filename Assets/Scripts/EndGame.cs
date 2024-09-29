using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(GameManager.instance.gameObject);
        Destroy(PlayerController.instance.gameObject);
        Destroy(GameMenu.instance.gameObject);
        Destroy(BattleManager.instance.gameObject);
    }

    public void QuitToMain()
    {
        

        SceneManager.LoadScene("MainMenu");
    }

    public void Credit()
    {
        SceneManager.LoadScene("Credit");
    }
}
