using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ghostOverview : MonoBehaviour
{
   
    void Start()
    {
        
    }

    public Ghost ghostData; 
    public GameObject uiPrefab; 

    public void OnButtonClick()
    {
        GameObject uiInstance = Instantiate(uiPrefab, Vector3.zero, Quaternion.identity);

        var ghostName = uiInstance.transform.Find("ghostName").GetComponent<TextMeshProUGUI>();
        var ghostType = uiInstance.transform.Find("ghostTypeInfo").GetComponent<TextMeshProUGUI>();
        var abilityName = uiInstance.transform.Find("ghostAbilityInfo").GetComponent<TextMeshProUGUI>();
        var skillAmount = uiInstance.transform.Find("ghostSkillAmountInfo").GetComponent<TextMeshPro>();
        var ghostDescription = uiInstance.transform.Find("ghostDescriptionInfo").GetComponent<TextMeshProUGUI>();
       
        var ghostIconMini = uiInstance.transform.Find("miniIcon").GetComponent<Image>();
        var ghostIconLarge = uiInstance.transform.Find("largeIcon").GetComponent<Image>();

    }
}
