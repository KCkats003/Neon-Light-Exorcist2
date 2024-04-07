using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLab : MonoBehaviour
{
    public string labExitScene;
    public bool inRange;

    private void Update()
    {

        if (inRange && Input.GetKeyUp(KeyCode.Space))
        {
            SceneManager.LoadScene(labExitScene, LoadSceneMode.Single);
        }
    }

    void OnTriggerEnter(Collider other)
    {

        Debug.Log("In zone");
        inRange = true;
    }
}