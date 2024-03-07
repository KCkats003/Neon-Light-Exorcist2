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
   

    // Start is called before the first frame update
    void Start()
    {

        //Debug.Log("SCREAM");
        //_mainCamera = Camera.main;
        // player = GameObject.FindGameObjectsWithTag("Player")[0];
        //   FightAnimator = player.GetComponent<Animator>();

        battleController = GameObject.FindGameObjectsWithTag("BattleSystem")[0];
        battleControllerScript = battleController.GetComponent<BattleSystem_KatieVer>();

      
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
