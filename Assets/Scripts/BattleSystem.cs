using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{

    public Ghost Ghost;

    public GameObject playerPrefab;
    public GameObject ghostOne;
    public GameObject ghostTwo;
    public GameObject ghostThree;
    public GameObject ghostFour;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform GhostOneStation;
    public Transform GhostTwoStation;
    public Transform GhostThreeStation;
    public Transform GhostFourStation;
    public Transform enemyBattleStation;

    Unit playerUnit;
    Unit enemyUnit;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public BattleState state;

    public string SampleScene;


    //Katies Animationss

    public GameObject player;
    private Animator FightAnimator;

    public GameObject playerPlaque;

    public GameObject enemyPlaque;


    public GameObject effects;
    private Animator EffectsAnimator;



    public GameObject enemy;
    private Animator EnemysAnimator;


    public GameObject enemyEffects;
    private Animator EnemyEffectsAnimator;

    //end Katie Additions


    // Start is called before the first frame update
    void Start()
    {

        Debug.Log(GameManager.objectNameToDestroy);

        state = BattleState.START;
        StartCoroutine(SetupBattle());

        //Katies Animationss
        FightAnimator = player.GetComponent<Animator>();
        EffectsAnimator = effects.GetComponent<Animator>();
        EnemysAnimator = enemy.GetComponent<Animator>();
        EnemyEffectsAnimator = enemyEffects.GetComponent<Animator>();


        playerPlaque.SetActive(false);
        enemyPlaque.SetActive(false);


        // end Katie Additions
    }

    IEnumerator SetupBattle()
    {

        //og
        // GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        // playerUnit = playerGO.GetComponent<Unit>();

        //GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        //enemyUnit = enemyGO.GetComponent<Unit>();

        //new
        GameObject playerGO = playerPrefab;
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = enemyPrefab;
        enemyUnit = enemyGO.GetComponent<Unit>();
        //end of new 

        //Loading in the ghosts
        /*
        GameObject ghostOneGo = Instantiate(ghostOne, GhostOneStation);
        GameObject ghostTwoGo = Instantiate(ghostTwo, GhostTwoStation);
        GameObject ghostThreeGo = Instantiate(ghostThree, GhostThreeStation);
        GameObject ghostFourGo = Instantiate(ghostFour, GhostFourStation);
        */
        /*
        GameObject ghostOneGO = Instantiate(GameManager.instance.partyGhosts[0], GhostOneStation);
        ghostOne = ghostOneGO;

        GameObject ghostTwoGO = Instantiate(GameManager.instance.partyGhosts[1], GhostTwoStation);
        ghostTwo = ghostTwoGO;

        GameObject ghostThreeGO = Instantiate(GameManager.instance.partyGhosts[2], GhostThreeStation);
        ghostThree = ghostThreeGO;

        GameObject ghostFourGO = Instantiate(GameManager.instance.partyGhosts[3], GhostFourStation);
        ghostFour = ghostFourGO;
        */

        for (int i = 0; i < GameManager.instance.partyGhosts.Count; i++)
        {
            GameObject ghostGO = Instantiate(GameManager.instance.partyGhosts[i], GetGhostStation(i));
            switch (i)
            {
                case 0:
                    ghostOne = ghostGO;
                    break;
                case 1:
                    ghostTwo = ghostGO;
                    break;
                case 2:
                    ghostThree = ghostGO;
                    break;
                case 3:
                    ghostFour = ghostGO;
                    break;
            }
        }

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(1f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack(int damageAmount)
    {
        //Katies Animationss
        FightAnimator.SetTrigger("PlayerAttack");
        EffectsAnimator.SetTrigger("PlayerHurts");
        EnemysAnimator.SetTrigger("Hurt");
        // end Katie Additions

        bool isDead = enemyUnit.TakeDamage(damageAmount);

        if (isDead)
        {
            state = BattleState.WON;
            enemyHUD.SetHP(enemyUnit.currentHP = 0);
            EndBattle(Ghost);
        }
        else
        {

            state = BattleState.ENEMYTURN;
            enemyHUD.SetHP(enemyUnit.currentHP);

            yield return new WaitForSeconds(1f);

            StartCoroutine(EnemyTurn());
        }

    }

    IEnumerator PlayerHeal(int healAmount)
    {
        playerUnit.Heal(healAmount);

        state = BattleState.ENEMYTURN;

        playerHUD.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(2f);

        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        playerPlaque.SetActive(false);
        enemyPlaque.SetActive(true);
        DisableAllButtons();

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHP(playerUnit.currentHP);

        //Katies Animationss
        FightAnimator.SetTrigger("PlayerHurt");
        EnemyEffectsAnimator.SetTrigger("EnemyAttacks");
        // end Katie Additions

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle(Ghost); //HERE added the Ghost
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    void EndBattle(Ghost ghost) //This line got the Ghost ghost to send the data
    {
        if (state == BattleState.WON)
        {
            GameManager.AddEnemyToDestroy(GameManager.objectNameToDestroy);
            // Load original scene
            SceneManager.LoadScene(SampleScene, LoadSceneMode.Single);

            // Add the defeated ghost to the GameManager's defeatedGhosts list
            GameManager.instance.AddDefeatedGhost(ghost); //ADDED this here
        }
        else if (state == BattleState.LOST)
        {
            // Load original scene on loss
            SceneManager.LoadScene(SampleScene, LoadSceneMode.Single);
        }

        // Store player's health in GameManager
        GameManager.playerHealth = playerUnit.currentHP;
    }


    void PlayerTurn()
    {
        playerPlaque.SetActive(true);
        enemyPlaque.SetActive(false);
        EnableAllButtons();
    }


    public void OnAttackButtonRed(int skillAmount)
    {
        Debug.Log("Red ATTACK");
        if (enemyUnit.type == Unit.GhostType.Green)
        {
            skillAmount *= 2;
            Debug.Log("Red is super effective against Green");
        }

        StartCoroutine(PlayerAttack(skillAmount));
    }

    public void OnAttackButtonBlue(int skillAmount)
    {
        Debug.Log("Blue ATTACK");
        if (enemyUnit.type == Unit.GhostType.Red)
        {
            skillAmount *= 2;
            Debug.Log("Blue is super effective against red");
        }

        StartCoroutine(PlayerAttack(skillAmount));
    }

    public void OnAttackButtonGreen(int skillAmount)
    {
        Debug.Log("Green ATTACK");
        if (enemyUnit.type == Unit.GhostType.Blue)
        {
            skillAmount *= 2;
            Debug.Log("Green is super effective against Blue");
        }

        StartCoroutine(PlayerAttack(skillAmount));
    }



    /*
    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }
    */

    public void OnHealButton(int skillAmount)
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerHeal(skillAmount));
    }

    void EnableAllButtons()
    {
        SetGhostColliders(true);
    }

    void DisableAllButtons()
    {
        SetGhostColliders(false);
    }

    void SetGhostColliders(bool isActive)
    {
        ghostOne.GetComponent<BoxCollider>().enabled = isActive;
        ghostTwo.GetComponent<BoxCollider>().enabled = isActive;
        ghostThree.GetComponent<BoxCollider>().enabled = isActive;
        ghostFour.GetComponent<BoxCollider>().enabled = isActive;
    }

    Transform GetGhostStation(int index)
    {
        switch (index)
        {
            case 0:
                return GhostOneStation;
            case 1:
                return GhostTwoStation;
            case 2:
                return GhostThreeStation;
            case 3:
                return GhostFourStation;
            default:
                return null;
        }
    }
}