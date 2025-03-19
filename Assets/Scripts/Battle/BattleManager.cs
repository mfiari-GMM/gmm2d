using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using System.Linq;
using static UnityEngine.GraphicsBuffer;

public class BattleManager : MonoBehaviour
{

    public static BattleManager instance;

    private bool battleActive;

    public GameObject battleScene;

    public Transform[] playerPositions;
    public Transform[] enemyPositions;

    public BattleChar[] playerPrefabs;
    public BattleChar[] enemyPrefabs;

    public List<BattleChar> activeBattlers = new List<BattleChar>();

    public int currentTurn;
    public bool turnWaiting;

    public GameObject uiButtonsHolder;

    public Text magicButtonText;

    public BattleMove[] movesList;
    public GameObject enemyAttackEffect;

    public DamageNumber theDamageNumber;

    public Text[] playerName, playerHP, playerMP;

    public GameObject targetMenu;
    public BattleTargetButton[] targetButtons;

    public GameObject magicMenu;
    public BattleMagicSelect[] magicButtons;

    public GameObject objectMenu;
    public BattleObjectSelect[] objectButtons;

    public BattleNotification battleNotice;

    public BattleNotification battleText;

    public int chanceToFlee = 35;
    private bool fleeing;

    public string gameOverScene;

    public int rewardXP;
    public int rewardMoney;
    public string[] rewardItems;

    public bool cannotFlee;

    public SpriteRenderer battleBg;
    public BattleBackground[] battleBackgrounds;

    public bool isBoss;

    // Use this for initialization
    void Start () {
        instance = this;
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {

        if(battleActive)
        {
            if(turnWaiting)
            {
                if(activeBattlers[currentTurn].isPlayer)
                {
                    uiButtonsHolder.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.Z))
                    {
                        CloseCurrentMenu();
                    }

                } else
                {
                    uiButtonsHolder.SetActive(false);

                    //enemy should attack
                    StartCoroutine(EnemyMoveCo());
                }
            }
        }
	}

    public void BattleStart(string[] enemiesToSpawn, bool setCannotFlee, string battleField)
    {
        if(!battleActive)
        {
            cannotFlee = setCannotFlee;

            battleActive = true;

            for (int i = 0; i < battleBackgrounds.Length; i++)
            {
                if (battleBackgrounds[i].fieldName == battleField)
                {
                    battleBg.sprite = battleBackgrounds[i].background;
                }
            }

            GameManager.instance.battleActive = true;

            transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z);
            battleScene.SetActive(true);

            if (isBoss)
            {
                AudioManager.instance.PlayBGM(1);
            } else
            {
                AudioManager.instance.PlayBGM(0);
            }


            int addIndex = 0;
            for(int i = 0; i < playerPositions.Length; i++)
            {
                int playerIndex = i + addIndex;
                if (GameManager.instance.playerStats.Length > playerIndex)
                {
                    if (GameManager.instance.playerStats[playerIndex].gameObject.activeInHierarchy)
                    {
                        for (int j = 0; j < playerPrefabs.Length; j++)
                        {
                            if (playerPrefabs[j].charName == GameManager.instance.playerStats[playerIndex].charName)
                            {
                                BattleChar newPlayer = Instantiate(playerPrefabs[j], playerPositions[i].position, playerPositions[i].rotation);
                                newPlayer.transform.parent = playerPositions[i];
                                activeBattlers.Add(newPlayer);


                                CharStats thePlayer = GameManager.instance.playerStats[playerIndex];
                                activeBattlers[i].currentHp = thePlayer.currentHP;
                                activeBattlers[i].maxHP = thePlayer.maxHP;
                                activeBattlers[i].currentMP = thePlayer.currentMP;
                                activeBattlers[i].maxMP = thePlayer.maxMP;
                                activeBattlers[i].strength = thePlayer.strength;
                                activeBattlers[i].defence = thePlayer.defence;
                                activeBattlers[i].magie = thePlayer.magie;
                                activeBattlers[i].resistance = thePlayer.resistance;
                                activeBattlers[i].wpnPower = thePlayer.equippedWpn != null ? thePlayer.equippedWpn.weaponStrength : 0;
                                activeBattlers[i].armrPower = thePlayer.equippedArmr != null ? thePlayer.equippedArmr.armorStrength : 0;

                                for (int playerLevel = 1; playerLevel <= thePlayer.playerLevel; playerLevel++)
                                {
                                    for (int k = 0; k < thePlayer.winMoves.Length; k++)
                                    {
                                        if (thePlayer.winMoves[k].level == playerLevel)
                                        {
                                            if (!activeBattlers[i].movesAvailable.Contains(thePlayer.winMoves[k].moveName))
                                            {
                                                activeBattlers[i].movesAvailable = activeBattlers[i].movesAvailable.Concat(new string[] { thePlayer.winMoves[k].moveName }).ToArray();
                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }
                    else
                    {
                        i--;
                        addIndex++;
                    }
                }
                
            }

            for (int i = 0; i < enemiesToSpawn.Length; i++)
            {
                if (enemiesToSpawn[i] != "")
                {
                    for (int j = 0; j < enemyPrefabs.Length; j++)
                    {
                        if (enemyPrefabs[j].charName == enemiesToSpawn[i])
                        {
                            BattleChar newEnemy = Instantiate(enemyPrefabs[j], enemyPositions[i].position, enemyPositions[i].rotation);
                            newEnemy.transform.parent = enemyPositions[i];
                            activeBattlers.Add(newEnemy);
                        }
                    }
                }
            }

            turnWaiting = true;
            currentTurn = Random.Range(0, activeBattlers.Count);

            UpdateBattle();
            UpdateUIStats();
        }
    }

    public void NextTurn()
    {
        currentTurn++;
        if(currentTurn >= activeBattlers.Count)
        {
            currentTurn = 0;
        }

        turnWaiting = true;

        UpdateBattle();
        UpdateUIStats();
    }

    public void UpdateBattle()
    {
        bool allEnemiesDead = true;
        bool allPlayersDead = true;

        for(int i = 0; i < activeBattlers.Count; i++)
        {
            if(activeBattlers[i].currentHp < 0)
            {
                activeBattlers[i].currentHp = 0;
            }
            if (activeBattlers[i].currentHp > activeBattlers[i].maxHP)
            {
                activeBattlers[i].currentHp = activeBattlers[i].maxHP;
            }

            if (activeBattlers[i].currentHp == 0)
            {
                //Handle dead battler
                if(activeBattlers[i].isPlayer)
                {
                    activeBattlers[i].theSprite.sprite = activeBattlers[i].deadSprite;
                } else
                {
                    activeBattlers[i].EnemyFade();
                }

            } else
            {
                activeBattlers[i].theSprite.sprite = activeBattlers[i].GetAliveSprite();
                if (activeBattlers[i].isPlayer)
                {
                    allPlayersDead = false;
                } else
                {
                    allEnemiesDead = false;
                }
            }
        }

        if(allEnemiesDead || allPlayersDead)
        {
            if(allEnemiesDead)
            {
                //end battle in victory
                StartCoroutine(EndBattleCo());
            } else
            {
                //end battle in failure
                StartCoroutine(GameOverCo());
            }
        } else
        {
            while(activeBattlers[currentTurn].currentHp == 0)
            {
                currentTurn++;
                if(currentTurn >= activeBattlers.Count)
                {
                    currentTurn = 0;
                }
            }
        }
    }

    public IEnumerator EnemyMoveCo()
    {
        turnWaiting = false;
        yield return new WaitForSeconds(1f);
        EnemyAttack();
        yield return new WaitForSeconds(1f);
        NextTurn();
    }

    private BattleMove GetMoveByName(string moveName)
    {
        for (int i = 0; i < movesList.Length; i++)
        {
            if (movesList[i].moveName == moveName)
            {
                return movesList[i];
            }
        }
        return null;
    }

    public void EnemyAttack()
    {
        List<int> players = new List<int>();
        for(int i = 0; i < activeBattlers.Count; i++)
        {
            if(activeBattlers[i].isPlayer && activeBattlers[i].currentHp > 0)
            {
                players.Add(i);
            }
        }
        int selectedTarget = players[Random.Range(0, players.Count)];

        if (activeBattlers[currentTurn].IsTransformed())
        {
            activeBattlers[currentTurn].NextTransformationTurn();
        }

        int selectAttack = Random.Range(0, activeBattlers[currentTurn].GetMovesAvailable().Length);
        int movePower = 0;
        bool magic = true;
        bool heal = false;
        BattleMove.BattleMoveType battleType = BattleMove.BattleMoveType.Normal;

        BattleMove theMove = GetMoveByName(activeBattlers[currentTurn].GetMovesAvailable()[selectAttack]);

        if (theMove == null || theMove.moveCost > activeBattlers[currentTurn].currentMP)
        {
            theMove = GetMoveByName("Slash");
        }

        activeBattlers[currentTurn].currentMP -= theMove.moveCost;

        if ("Metamorphose" == theMove.moveName)
        {
            activeBattlers[currentTurn].Transformation();
            battleText.theText.text = theMove.moveName;
            battleText.Activate();
            return;
        }

        Instantiate(theMove.theEffect, activeBattlers[selectedTarget].transform.position, activeBattlers[selectedTarget].transform.rotation);
        movePower = theMove.movePower;
        magic = theMove.isMagic;
        battleType = theMove.battleType;
        heal = theMove.heal;

        if ("Slash" != theMove.moveName)
        {
            battleText.theText.text = theMove.moveName;
            battleText.Activate();
        }

        Instantiate(enemyAttackEffect, activeBattlers[currentTurn].transform.position, activeBattlers[currentTurn].transform.rotation);

        if (heal)
        {
            Heal(selectedTarget, movePower);
        } else
        {
            DealDamage(selectedTarget, movePower, magic, battleType);
        }
        
        if (activeBattlers[currentTurn].ShoudlDeTransform())
        {
            activeBattlers[currentTurn].Detransformation();
            battleText.theText.text = "Detransformation";
            battleText.Activate();
        }

    }

    public void Heal(int target, int movePower)
    {
        float strength = activeBattlers[currentTurn].GetMagie();

        float atkPwr = strength + activeBattlers[currentTurn].GetWpnPower();

        float damageCalc = (atkPwr / 10) * movePower * Random.Range(.9f, 1.1f);
        int damageToGive = Mathf.RoundToInt(damageCalc);

        activeBattlers[target].currentHp += damageToGive;

        Instantiate(theDamageNumber, activeBattlers[target].transform.position, activeBattlers[target].transform.rotation).SetDamage(damageToGive, 2);

        UpdateUIStats();
    }

    public void DealDamage(int target, int movePower, bool magic, BattleMove.BattleMoveType battleType)
    {

        float strength = magic ? activeBattlers[currentTurn].GetMagie() : activeBattlers[currentTurn].GetStrength();
        float defence = magic ? activeBattlers[target].GetResistance() : activeBattlers[target].GetDefence();

        float atkPwr = strength + activeBattlers[currentTurn].GetWpnPower();
        float defPwr = defence + activeBattlers[target].GetArmrPower();

        float damageMutiplicator = 1f;
        int damageWeakness = 0;

        if (Array.IndexOf(activeBattlers[target].GetWeaknesses(), battleType) != -1)
        {
            damageMutiplicator = 1.5f;
            damageWeakness = 1;
        } else if (Array.IndexOf(activeBattlers[target].GetResistances(), battleType) != -1)
        {
            damageMutiplicator = 0.5f;
            damageWeakness = -1;
        }

        float damageCalc = (atkPwr / defPwr) * movePower * Random.Range(.9f, 1.1f) * damageMutiplicator;
        int damageToGive = Mathf.RoundToInt(damageCalc);

        activeBattlers[target].currentHp -= damageToGive;

        Instantiate(theDamageNumber, activeBattlers[target].transform.position, activeBattlers[target].transform.rotation).SetDamage(damageToGive, damageWeakness);

        UpdateUIStats();
    }

    public void UpdateUIStats()
    {
        for (int i = 0; i < playerName.Length; i++)
        {
            if (activeBattlers.Count > i)
            {
                if (activeBattlers[i].isPlayer)
                {
                    BattleChar playerData = activeBattlers[i];

                    playerName[i].gameObject.SetActive(true);
                    playerName[i].text = playerData.charName;
                    if (currentTurn == i)
                    {
                        playerName[i].color = Color.yellow;
                        if (playerData.charName == "Vard" || playerData.charName == "Rose")
                        {
                            magicButtonText.text = "Magic";
                        } else
                        {
                            magicButtonText.text = "Tech.";
                        }
                    } else
                    {
                        playerName[i].color = Color.white;
                    }
                    playerHP[i].text = Mathf.Clamp(playerData.currentHp, 0, int.MaxValue) + "/" + playerData.maxHP;
                    playerMP[i].text = Mathf.Clamp(playerData.currentMP, 0, int.MaxValue) + "/" + playerData.maxMP;

                } else
                {
                    playerName[i].gameObject.SetActive(false);
                }
            } else
            {
                playerName[i].gameObject.SetActive(false);
            }
        }
    }

    public void UseItem (string itemName, int selectedTarget)
    {
        Item selectItem = GameManager.instance.GetItemDetails(itemName);
        selectItem.UseBattle(selectedTarget);
        AudioManager.instance.PlaySFX(6);

        Instantiate(theDamageNumber, activeBattlers[selectedTarget].transform.position, activeBattlers[selectedTarget].transform.rotation).SetDamage(selectItem.amountToChange, 2);

        UpdateUIStats();
        uiButtonsHolder.SetActive(false);
        targetMenu.SetActive(false);

        NextTurn();
    }

    public void PlayerAttack(string moveName, int[] selectedTarget)
    {
        int movePower = 0;
        bool magic = true;
        bool heal = false;

        BattleMove.BattleMoveType battleType = BattleMove.BattleMoveType.Normal;

        BattleMove battleMove = movesList[0];

        if ("Slash" != moveName)
        {
            battleText.theText.text = moveName;
            battleText.Activate();
        }

        for (int i = 0; i < movesList.Length; i++)
        {
            if (movesList[i].moveName == moveName)
            {
                battleMove = movesList[i];
                movePower = battleMove.movePower;
                battleType = battleMove.battleType;
                magic = battleMove.isMagic;
                heal = battleMove.heal;
                break;
            }
        }
        activeBattlers[currentTurn].currentMP -= battleMove.moveCost;

        Instantiate(enemyAttackEffect, activeBattlers[currentTurn].transform.position, activeBattlers[currentTurn].transform.rotation);

        for (int i = 0 ; i < selectedTarget.Length; i++)
        {
            Instantiate(battleMove.theEffect, activeBattlers[selectedTarget[i]].transform.position, activeBattlers[selectedTarget[i]].transform.rotation);


            if (heal)
            {
                Heal(selectedTarget[i], movePower);
            }
            else
            {
                DealDamage(selectedTarget[i], movePower, magic, battleType);
            }
        }
        

        uiButtonsHolder.SetActive(false);
        targetMenu.SetActive(false);

        NextTurn();

    }

    public void CloseCurrentMenu ()
    {
        if (targetMenu.activeInHierarchy)
        {
            targetMenu.SetActive(false);
        }
        if (magicMenu.activeInHierarchy)
        {
            magicMenu.SetActive(false);
        }
    }

    public void SelectMove (string moveName)
    {
        for (int i = 0; i < movesList.Length; i++)
        {
            if (movesList[i].moveName == moveName)
            {
                if (movesList[i].allChar)
                {
                    List<int> Enemies = new List<int>();
                    for (int j = 0; j < activeBattlers.Count; j++)
                    {
                        if (activeBattlers[j].currentHp > 0 
                            && ((!movesList[i].isPlayer && !activeBattlers[j].isPlayer) 
                            || (movesList[i].isPlayer && activeBattlers[j].isPlayer)))
                        {
                            Enemies.Add(j);
                        }
                    }
                    activeBattlers[currentTurn].currentMP -= movesList[i].moveCost;
                    PlayerAttack(moveName, Enemies.ToArray());
                } else
                {
                    OpenTargetMenu(moveName);
                }
                break;
            }
        }
    }

    public void OpenTargetMenu(string moveName)
    {
        targetMenu.SetActive(true);
        bool choosePlayer = false;

        for (int i = 0; i < movesList.Length; i++)
        {
            if (movesList[i].moveName == moveName)
            {
                choosePlayer = movesList[i].isPlayer;
            }
        }

        List<int> Enemies = new List<int>();
        for(int i = 0; i < activeBattlers.Count; i++)
        {
            if((!choosePlayer && !activeBattlers[i].isPlayer) || (choosePlayer && activeBattlers[i].isPlayer))
            {
                Enemies.Add(i);
            }
        }

        for(int i = 0; i < targetButtons.Length; i++)
        {
            if(Enemies.Count > i && activeBattlers[Enemies[i]].currentHp > 0)
            {
                targetButtons[i].gameObject.SetActive(true);

                targetButtons[i].isItem = false;
                targetButtons[i].moveName = moveName;
                targetButtons[i].activeBattlerTarget = Enemies[i];
                targetButtons[i].targetName.text = activeBattlers[Enemies[i]].charName;
            } else
            {
                targetButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void OpenPlayersMenu(string itemName)
    {
        targetMenu.SetActive(true);

        List<int> players = new List<int>();
        for (int i = 0; i < activeBattlers.Count; i++)
        {
            if (activeBattlers[i].isPlayer)
            {
                players.Add(i);
            }
        }

        for (int i = 0; i < targetButtons.Length; i++)
        {
            if (players.Count > i && (
                (itemName == "Elixir" && activeBattlers[players[i]].currentHp <= 0)
                || (itemName != "Elixir" && activeBattlers[players[i]].currentHp > 0)))
            {
                targetButtons[i].gameObject.SetActive(true);

                targetButtons[i].isItem = true;
                targetButtons[i].activeBattlerTarget = players[i];
                targetButtons[i].itemName = itemName;
                targetButtons[i].targetName.text = activeBattlers[players[i]].charName;
            }
            else
            {
                targetButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void OpenMagicMenu()
    {
        magicMenu.SetActive(true);

        for(int i = 0; i < magicButtons.Length; i++)
        {
            if(activeBattlers[currentTurn].GetMovesAvailable().Length > i)
            {
                magicButtons[i].gameObject.SetActive(true);

                magicButtons[i].spellName = activeBattlers[currentTurn].GetMovesAvailable()[i];
                magicButtons[i].nameText.text = magicButtons[i].spellName;

                for(int j = 0; j < movesList.Length; j++)
                {
                    if(movesList[j].moveName == magicButtons[i].spellName)
                    {
                        magicButtons[i].spellCost = movesList[j].moveCost;
                        magicButtons[i].costText.text = magicButtons[i].spellCost.ToString();
                    }
                }

            } else
            {
                magicButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void OpenObjectMenu()
    {
        objectMenu.SetActive(true);
        int itemIndex = 0;

        for (int i = 0; i < objectButtons.Length; i++)
        {
            objectButtons[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < objectButtons.Length; i++)
        {
            while (itemIndex < GameManager.instance.itemsHeld.Length)
            {
                string itemName = GameManager.instance.itemsHeld[itemIndex];
                int quantity = GameManager.instance.numberOfItems[itemIndex];
                itemIndex++;
                Item selectItem = GameManager.instance.GetItemDetails(itemName);
                if (selectItem != null && selectItem.isItem)
                {
                    objectButtons[i].gameObject.SetActive(true);
                    objectButtons[i].itemName = itemName;
                    objectButtons[i].nameText.text = itemName;
                    objectButtons[i].quantityText.text = quantity.ToString();
                    break;
                }
            }
        }
    }

    public void Flee()
    {
        if (cannotFlee)
        {
            battleNotice.theText.text = "Fuite impossible !";
            battleNotice.Activate();
        }
        else
        {
            int fleeSuccess = Random.Range(0, 100);
            if (fleeSuccess < chanceToFlee)
            {
                //end the battle
                //battleActive = false;
                //battleScene.SetActive(false);
                fleeing = true;
                StartCoroutine(EndBattleCo());
            }
            else
            {
                NextTurn();
                battleNotice.theText.text = "La fuite a echoue !";
                battleNotice.Activate();
            }
        }

    }

    public IEnumerator EndBattleCo()
    {
        battleActive = false;
        uiButtonsHolder.SetActive(false);
        targetMenu.SetActive(false);
        magicMenu.SetActive(false);

        yield return new WaitForSeconds(.5f);

        UIFade.instance.FadeToBlack();

        yield return new WaitForSeconds(1.5f);

        for(int i = 0; i < activeBattlers.Count; i++)
        {
            if(activeBattlers[i].isPlayer)
            {
                for(int j = 0; j < GameManager.instance.playerStats.Length; j++)
                {
                    if(activeBattlers[i].charName == GameManager.instance.playerStats[j].charName)
                    {
                        GameManager.instance.playerStats[j].currentHP = activeBattlers[i].currentHp;
                        GameManager.instance.playerStats[j].currentMP = activeBattlers[i].currentMP;
                    }
                }
            }

            Destroy(activeBattlers[i].gameObject);
        }

        UIFade.instance.FadeFromBlack();
        battleScene.SetActive(false);
        activeBattlers.Clear();
        currentTurn = 0;

        if(fleeing)
        {
            GameManager.instance.battleActive = false;
            fleeing = false;
        } else
        {
            if (rewardXP > 0 || rewardMoney > 0 || rewardItems.Length > 0)
            {
                BattleReward.instance.OpenRewardScreen(rewardXP, rewardMoney, rewardItems);
            } else
            {
                if (BattleReward.instance.markQuestComplete)
                {
                    QuestManager.instance.MarkQuestComplete(BattleReward.instance.questToMark);
                }
                GameManager.instance.battleActive = false;
            }
        }
    }

    public IEnumerator GameOverCo()
    {
        battleActive = false;
        UIFade.instance.FadeToBlack();
        yield return new WaitForSeconds(1.5f);
        battleScene.SetActive(false);
        SceneManager.LoadScene(gameOverScene);
    }
}
