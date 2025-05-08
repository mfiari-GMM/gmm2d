using System.Collections;
using UnityEngine;

public class MapInfo : MonoBehaviour
{

    [SerializeField]
    private string title;
    [SerializeField]
    private string description;

    private bool infoHasBeenDisplayed = false;

    void Update()
    {
        if (isFisrtVisit())
        {
            if (!infoHasBeenDisplayed)
            {
                StartCoroutine(DisplayMapInfo());
            }
        }
    }

    private bool isFisrtVisit ()
    {
        return !PlayerPrefs.HasKey("SCENE_" + title);
    }

    private IEnumerator DisplayMapInfo ()
    {
        if (GameMenu.instance != null)
        {
            infoHasBeenDisplayed = true;
            GameMenu.instance.ShowMapInfo(title, description);
            yield return new WaitForSeconds(5f);
            GameMenu.instance.HideMapInfo();
            PlayerPrefs.SetInt("SCENE_" + title, 1);
        }
    }
}
