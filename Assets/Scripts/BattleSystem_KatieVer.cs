using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class BattleSystem_KatieVer : MonoBehaviour
{
  //  public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
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

    //  end Katie Additions

    // Start is called before the first frame update
    void Start()
    {

        Debug.Log(GameManager.objectNameToDestroy);

        state = BattleState.START;
        StartCoroutine(SetupBattle());

        //Katies Animationss
    FightAnimator = player.GetComponent<Animator>();

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



        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {

        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);


        if (isDead)
        {
            state = BattleState.WON;
            enemyHUD.SetHP(enemyUnit.currentHP = 0);
            EndBattle();
        }
        else
        {


            state = BattleState.ENEMYTURN;
            enemyHUD.SetHP(enemyUnit.currentHP);

       

            yield return new WaitForSeconds(1f);
            StartCoroutine(EnemyTurn());
        }

    }

    IEnumerator PlayerHeal()
    {
        playerUnit.Heal(5);

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
            GameManager.AddEnemyToDestroy(GameManager.objectNameToDestroy);
            // Load original scene
            SceneManager.LoadScene(SampleScene, LoadSceneMode.Single);
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
    }

    public void OnAttackButton()
    {


        //Katies Animationss
        FightAnimator.SetTrigger("PlayerAttack");
        // end Katie Additions
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerHeal());
    }

}