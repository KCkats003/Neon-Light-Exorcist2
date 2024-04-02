using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

public class GhostClick : MonoBehaviour
{

    //  private Camera _mainCamera;
    // public GameObject player;
    // private Animator FightAnimator;
  private GameObject battleController;
    private BattleSystem_KatieVer battleControllerScript;

    public GameObject ghostInfo;
    // Start is called before the first frame update
    void Start()
    {

        //Debug.Log("SCREAM");
        //_mainCamera = Camera.main;
        // player = GameObject.FindGameObjectsWithTag("Player")[0];
        //   FightAnimator = player.GetComponent<Animator>();

        battleController = GameObject.FindGameObjectsWithTag("BattleSystem")[0];
        battleControllerScript = battleController.GetComponent<BattleSystem_KatieVer>();
        ghostInfo.SetActive(false);

        //  battleControllerScript.EnemyTurn();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnMouseDown()
    {
       // FightAnimator.SetTrigger("PlayerAttack");
        battleControllerScript.OnAttackButton();
       // Debug.Log("AHHHHH");
    }


    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        ghostInfo.SetActive(true);
        //Debug.Log("Mouse is over GameObject.");
    }

    void OnMouseExit()
    {
        ghostInfo.SetActive(false);
        //The mouse is no longer hovering over the GameObject so output this message each frame
        //Debug.Log("Mouse is no longer on GameObject.");
    }


    /*
    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider) return;

        Debug.Log(rayHit.collider.gameObject.name);
    }
    */

    /*
     * 
     *  using UnityEngine;

public class InputHandler : MonoBehaviour
{
    #region Variables

    

    #endregion

    private void Awake()
    {
        
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider) return;

        Debug.Log(rayHit.collider.gameObject.name);
    }
}

*/
}
