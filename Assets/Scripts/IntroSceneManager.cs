using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroSceneManager : MonoBehaviour
{

    [SerializeField]
    private string newGameScene;

    [SerializeField]
    private Text introText;

    [SerializeField]
    private Scrollbar scrollbar;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlayBGM(4);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            StartGame();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float speed = 0.010f;

        scrollbar.value -= speed * Time.deltaTime;

        if (scrollbar.value < 0)
        {
            StartGame();
        }
    }

    private void StartGame ()
    {
        if ("House4".Equals(newGameScene))
        {
            PlayerPrefs.DeleteAll();
        }
        SceneManager.LoadScene(newGameScene);
    }
}
