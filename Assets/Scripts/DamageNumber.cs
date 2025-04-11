using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DamageNumber : MonoBehaviour {

    public Text damageText;

    public float lifetime = 1f;
    public float moveSpeed = 1f;

    public float placementJitter = .5f;
	
	// Update is called once per frame
	void Update () {
        Destroy(gameObject, lifetime);
        transform.position += new Vector3(0f, moveSpeed * Time.deltaTime, 0f);
	}

    public void DisplayText (string textToDisplay)
    {
        damageText.text = textToDisplay;
        damageText.fontStyle = FontStyle.Normal;
        damageText.color = Color.white;
        damageText.fontSize = 70;
        transform.position += new Vector3(1f, 0.6f, 0f);
    }

    public void SetDamage(int damageAmount, int damageWeakness)
    {
        damageText.text = damageAmount.ToString();
        if (damageWeakness == 1)
        {
            damageText.fontStyle = FontStyle.Bold;
            damageText.color = Color.yellow;
            damageText.fontSize = 109;
        }
        else if (damageWeakness == 2)
        {
            damageText.fontStyle = FontStyle.Bold;
            damageText.color = Color.green;
            damageText.fontSize = 109;
        }
        else if (damageWeakness == -1)
        {
            damageText.fontStyle = FontStyle.Normal;
            damageText.color = Color.white;
            damageText.fontSize = 80;
        }
        else
        {
            damageText.fontStyle = FontStyle.Normal;
            damageText.color = Color.white;
            damageText.fontSize = 109;
        }
        transform.position += new Vector3(0f, 0f, 0f);
    }
}
