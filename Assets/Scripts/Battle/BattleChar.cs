using UnityEngine;

public class BattleChar : MonoBehaviour {

    public bool isPlayer;
    public string[] movesAvailable;

    public string charName;
    public int currentHp, maxHP, currentMP, maxMP, strength, defence, magie, resistance, wpnPower, armrPower;
    public bool hasDied;

    public SpriteRenderer theSprite;
    public Sprite deadSprite, aliveSprite;

    private bool shouldFade;
    public float fadeSpeed = 1f;

    public BattleMove.BattleMoveType[] weaknesses;
    public BattleMove.BattleMoveType[] resistances;

    public BattleChar transformation;
    public int maxTransformationTurn = 3;

    private int transformationTurn = 0;
    private bool isTransformed = false;

    // Update is called once per frame
    void Update () {
		if(shouldFade)
        {
            theSprite.color = new Color(Mathf.MoveTowards(theSprite.color.r, 1f, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(theSprite.color.g, 0f, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(theSprite.color.b, 0f, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(theSprite.color.a, 0f, fadeSpeed * Time.deltaTime));
            if(theSprite.color.a == 0)
            {
                gameObject.SetActive(false);
            }
        }
	}

    public string[] GetMovesAvailable ()
    {
        if (isTransformed)
        {
            return transformation.movesAvailable;
        }
        return movesAvailable;
    }

    public Sprite GetAliveSprite ()
    {
        if (isTransformed)
        {
            return transformation.aliveSprite;
        }
        return aliveSprite;
    }

    public int GetStrength ()
    {
        if (isTransformed)
        {
            return transformation.strength;
        }
        return strength;
    }

    public int GetDefence()
    {
        if (isTransformed)
        {
            return transformation.defence;
        }
        return defence;
    }

    public int GetMagie()
    {
        if (isTransformed)
        {
            return transformation.magie;
        }
        return magie;
    }

    public int GetResistance()
    {
        if (isTransformed)
        {
            return transformation.resistance;
        }
        return resistance;
    }

    public int GetWpnPower()
    {
        if (isTransformed)
        {
            return transformation.wpnPower;
        }
        return wpnPower;
    }

    public int GetArmrPower()
    {
        if (isTransformed)
        {
            return transformation.armrPower;
        }
        return armrPower;
    }

    public BattleMove.BattleMoveType[] GetWeaknesses ()
    {
        if (isTransformed)
        {
            return transformation.weaknesses;
        }
        return weaknesses;
    }

    public BattleMove.BattleMoveType[] GetResistances()
    {
        if (isTransformed)
        {
            return transformation.resistances;
        }
        return resistances;
    }

    public void EnemyFade()
    {
        shouldFade = true;
    }

    public void Transformation ()
    {
        this.isTransformed = true;

    }

    public bool IsTransformed()
    {
        return this.isTransformed;

    }

    public void NextTransformationTurn()
    {
        transformationTurn++;

    }

    public bool ShoudlDeTransform()
    {
        return transformationTurn >= this.maxTransformationTurn;
    }

    public void Detransformation()
    {
        this.isTransformed = false;
        this.transformationTurn = 0;

    }
}
