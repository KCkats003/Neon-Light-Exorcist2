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

    // Start is called before the first frame update
    void Start()
    {

        Debug.Log(GameManager.objectNameToDestroy);

        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {

        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

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

        yield return new WaitForSeconds(1f);

        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHP(playerUnit.currentHP);

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

            //I dont know might add the ghost to the inventory IDK
            GhostManager.Instance.Add(Ghost);
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

}