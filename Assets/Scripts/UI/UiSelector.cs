using UnityEngine;

public class UiSelector : MonoBehaviour
{

    [SerializeField]
    private UiItem[] uiItems;

    private int currentIndex = 0;
    private bool isStickPress = false;
    private bool isStickReleased = true;

    private bool isDisable = false;

    // Start is called before the first frame update
    void Start()
    {
        uiItems[currentIndex].SelectItem();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDisable)
        {
            return;
        }

        if ((Input.GetButtonDown("Vertical") || (isStickPress && !isStickReleased)) && Input.GetAxisRaw("Vertical") < 0)
        {
            uiItems[currentIndex].UnselectItem();
            currentIndex++;
            if (currentIndex >= uiItems.Length)
            {
                currentIndex = 0;
            }
            uiItems[currentIndex].SelectItem();
            isStickPress = false;
        }
        else if ((Input.GetButtonDown("Vertical") || (isStickPress && !isStickReleased)) && Input.GetAxisRaw("Vertical") > 0)
        {
            uiItems[currentIndex].UnselectItem();
            currentIndex--;
            if (currentIndex < 0)
            {
                currentIndex = uiItems.Length - 1;
            }
            uiItems[currentIndex].SelectItem();
            isStickPress = false;
        }
        else if (!isStickPress && isStickReleased && Input.GetAxisRaw("Vertical") != 0 && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            isStickPress = true;
            isStickReleased = false;
        }
        else if (Input.GetAxisRaw("Vertical") == 0)
        {
            isStickPress = false;
            isStickReleased = true;
        }

    }

    public void DisableSelector()
    {
        isDisable = true;
    }

    public void EnableSelector()
    {
        isDisable = false;
    }
}
