using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateExFight : MonoBehaviour
{

    private Animator FightAnimator; 

    // Start is called before the first frame update
    void Start()
    {
        FightAnimator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        if (FightAnimator != null) {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                FightAnimator.SetTrigger("PlayerHurt");
            }
        }
        
    }
}
