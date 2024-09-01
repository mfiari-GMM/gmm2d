using UnityEngine;

public class PickupItem : MonoBehaviour {

    public bool isChest;
    public Sprite chestSprite;

    private bool canPickup;
    private bool isOpen = false;

    // Update is called once per frame
    void Update () {
		if(canPickup && !isOpen && Input.GetButtonDown("Fire1") && PlayerController.instance.canMove)
        {
            GameManager.instance.AddItem(GetComponent<Item>().itemName);

            if (isChest)
            {
                GetComponent<SpriteRenderer>().sprite = chestSprite;
                isOpen = true;
            }
            else {
                Destroy(gameObject);
            }
            
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            canPickup = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canPickup = false;
        }
    }
}
