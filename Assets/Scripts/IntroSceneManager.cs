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
            SceneManager.LoadScene(newGameScene);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float speed = 0.013f;

        scrollbar.value -= speed * Time.deltaTime;

        if (scrollbar.value < 0)
        {
            SceneManager.LoadScene(newGameScene);
        }
    }
}
