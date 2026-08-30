using System;
using UnityEngine;

public class BattleChar : MonoBehaviour {

    public bool isPlayer;
    public string[] movesAvailable;

    public string charName;
    public int currentHp, maxHP, currentMP, maxMP, strength, defence, magie, resistance, wpnPower, wpnMagie, armrPower, armrRes, criticalRate, dodgeRate;
    public BattleMove.BattleMoveType wpnBattleType;
    public bool isWeaponMagic;
    public bool hasDied;

    public SpriteRenderer theSprite;
    public Sprite deadSprite, aliveSprite;

    private bool shouldFade;
    public float fadeSpeed = 1f;

    public BattleMove.BattleMoveType[] weaknesses;
    public BattleMove.BattleMoveType[] resistances;

    public BattleMove.BattleStatus battleStatus;

    public BattleChar transformation;
    public int maxTransformationTurn = 3;

    private int transformationTurn = 0;
    private bool isTransformed = false;
    private float boostAtk = 1f, boostDef = 1f, boostMagie = 1f, boostRes = 1f, boostCritic = 1f, boostDodge = 1f;

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
            return (int)Math.Round(transformation.strength * boostAtk);
        }
        return (int)Math.Round(strength * boostAtk);
    }

    public int GetDefence()
    {
        if (isTransformed)
        {
            return (int)Math.Round(transformation.defence * boostDef);
        }
        return (int)Math.Round(defence * boostDef);
    }

    public int GetMagie()
    {
        if (isTransformed)
        {
            return (int)Math.Round(transformation.magie * boostMagie);
        }
        return (int)Math.Round(magie * boostMagie);
    }

    public int GetResistance()
    {
        if (isTransformed)
        {
            return (int)Math.Round(transformation.resistance * boostRes);
        }
        return (int)Math.Round(resistance * boostRes);
    }

    public int GetWpnPower()
    {
        if (isTransformed)
        {
            return transformation.wpnPower;
        }
        return wpnPower;
    }

    public int GetWpnMagie()
    {
        if (isTransformed)
        {
            return transformation.wpnMagie;
        }
        return wpnMagie;
    }

    public int GetArmrPower()
    {
        if (isTransformed)
        {
            return transformation.armrPower;
        }
        return armrPower;
    }

    public int GetArmrRes()
    {
        if (isTransformed)
        {
            return transformation.armrRes;
        }
        return armrRes;
    }

    public int GetCriticalRate()
    {
        if (isTransformed)
        {
            return (int)Math.Round(transformation.criticalRate * boostCritic);
        }
        return (int)Math.Round(criticalRate * boostCritic);
    }

    public int GetDodgeRate()
    {
        if (isTransformed)
        {
            return (int)Math.Round(transformation.dodgeRate * boostDodge);
        }
        return (int)Math.Round(dodgeRate * boostDodge);
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

    public void IncreaseAtk ()
    {
        boostAtk = 1.5f;
    }

    public void IncreaseDef()
    {
        boostDef = 2f;
    }

    public void resetDef()
    {
        boostDef = 1f;
    }
}
