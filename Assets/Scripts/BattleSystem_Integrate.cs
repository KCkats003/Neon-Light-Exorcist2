using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


//public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem_Integrate : MonoBehaviour
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


    public GameObject WinScreen;
    public GameObject LoseScreen;

    public Battle_HUD_Katie playerHUD;
    public Battle_HUD_Katie enemyHUD;

    //  end Katie Additions

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

        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);
        // end Katie Additions
    }

    IEnumerator SetupBattle()
    {

        //GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        GameObject playerGO = playerPrefab;
        playerUnit = playerGO.GetComponent<Unit>();

        //GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        GameObject enemyGO = enemyPrefab;
        enemyUnit = enemyGO.GetComponent<Unit>();
        //end of new 

        //Loading in the ghosts
        GameObject ghostOneGo = Instantiate(ghostOne, GhostOneStation);
        GameObject ghostTwoGo = Instantiate(ghostTwo, GhostTwoStation);
        GameObject ghostThreeGo = Instantiate(ghostThree, GhostThreeStation);
        GameObject ghostFourGo = Instantiate(ghostFour, GhostFourStation);

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }


    IEnumerator PlayerAttack(int damageAmount)
    {

        bool isDead = enemyUnit.TakeDamage(damageAmount);

        if (isDead)
        {
            state = BattleState.WON;
            enemyHUD.SetHP(enemyUnit.currentHP = 0);
            EndBattle();
        }
        else
        {

            FightAnimator.SetTrigger("PlayerAttack");
            EffectsAnimator.SetTrigger("PlayerHurts");
            EnemysAnimator.SetTrigger("Hurt");

            state = BattleState.ENEMYTURN;
            enemyHUD.SetHP(enemyUnit.currentHP);

            yield return new WaitForSeconds(1f);
            StartCoroutine(EnemyTurn());
        }

    }

    IEnumerator PlayerHeal(int healAmount)
    {
        FightAnimator.SetTrigger("PlayerHeal");
        playerUnit.Heal(healAmount);
        
        state = BattleState.ENEMYTURN;

        playerHUD.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(1f);

        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        playerPlaque.SetActive(false);
        enemyPlaque.SetActive(true);

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
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    void EndBattle()
    {
        if (state == BattleState.WON)



        {
            //katie
            WinScreen.SetActive(true);
            //katie
        }
        else if (state == BattleState.LOST)
        {
            // Load original scene on loss
            SceneManager.LoadScene(SampleScene, LoadSceneMode.Single);
        }

        // Store player's health in GameManager
        GameManager.playerHealth = playerUnit.currentHP;
    }


    //katie animation
    public void NextScene()
    {
        SceneManager.LoadScene(SampleScene, LoadSceneMode.Single);
    }
    //end of katie enimation

    void PlayerTurn()
    {
        playerPlaque.SetActive(true);
        enemyPlaque.SetActive(false);
    }


    public void OnAttackButtonRed(int skillAmount)
    {
        Debug.Log("Red ATTACK");
        if (enemyUnit.type == Unit.GhostType.Green)
        {
            skillAmount *= 2;
            Debug.Log("Red is super effective against Green");
        }

        enemyHUD.SetHP(enemyUnit.currentHP);

        //Katies Animationss

        // end Katie Additions
        // if (state != BattleState.PLAYERTURN)
        //    return;


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

        enemyHUD.SetHP(enemyUnit.currentHP);

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

        enemyHUD.SetHP(enemyUnit.currentHP);

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

}