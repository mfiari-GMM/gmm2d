using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UiItem : MonoBehaviour
{

    [SerializeField]
    private UnityEvent selectedEvent;
    [SerializeField]
    private UnityEvent applyEvent;
    [SerializeField]
    public Sprite selectedImage;

    private bool isSelected;
    private Sprite initialSprite;



    // Update is called once per frame
    void Update()
    {
        if (isSelected && Input.GetButtonDown("Fire1"))
        {
            applyEvent.Invoke();
        }
    }

    public void SelectItem ()
    {
        isSelected = true;

        ApplySelected();
    }

    public void UnselectItem()
    {
        isSelected = false;

        Image myImage = GetComponent<Image>();

        myImage.sprite = initialSprite;
    }

    void ApplySelected()
    {
        
        if (selectedImage == null)
            return;

        Image myImage = GetComponent<Image>();

        initialSprite = myImage.sprite;

        myImage.sprite = selectedImage;

        if (selectedEvent != null) {
            selectedEvent.Invoke();
        }
    }
}
