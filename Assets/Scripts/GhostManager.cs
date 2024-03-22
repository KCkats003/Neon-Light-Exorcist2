using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GhostManager : MonoBehaviour
{
    public static GhostManager Instance;
    public List<Ghost> Ghosts = new List<Ghost>();

    public Transform ghostRoster;
    public GameObject miniGhost;
    public GameObject uiPrefab;
    public Transform overviewLocation;

    private GameObject currentOverviewInstance;

    private void Awake()
    {
        Instance = this;
    }

    public void Add(Ghost ghost)
    {
        Ghosts.Add(ghost);
    }

    public void ListItems()
    {
        Debug.Log("Listing Ghosts...");

        DestroyCurrentOverview();

        //Makes it so there is only one of each ghost
        foreach (Transform ghost in ghostRoster)
        {
            Destroy(ghost.gameObject);
        }

        foreach (var ghost in Ghosts) {

            GameObject obj = Instantiate(miniGhost, ghostRoster);

            var ghostButton = obj.GetComponent<Button>();
            var ghostData = ghost;

            ghostButton.onClick.AddListener(() =>
            {
                OnGhostButtonClick(ghostData);
            });


            var ghostName = obj.transform.Find("ghostName").GetComponent<TextMeshProUGUI>();
            var ghostIconMini = obj.transform.Find("iconMini").GetComponent<Image>();

            Debug.Log(ghost.ghostName);

            ghostName.text = ghost.ghostName;
            ghostIconMini.sprite = ghost.iconMini;

            
        }
    }

    public void OnGhostButtonClick(Ghost ghost)
    {

        DestroyCurrentOverview();

        GameObject uiInstance = Instantiate(uiPrefab, overviewLocation);

        var ghostName = uiInstance.transform.Find("ghostNameInfo").GetComponent<TextMeshProUGUI>();
        var ghostType = uiInstance.transform.Find("ghostTypeInfo").GetComponent<TextMeshProUGUI>();
        var abilityName = uiInstance.transform.Find("ghostAbilityInfo").GetComponent<TextMeshProUGUI>();
        var skillAmount = uiInstance.transform.Find("ghostSkillAmountInfo").GetComponent<TextMeshProUGUI>();
        var ghostDescription = uiInstance.transform.Find("ghostDescriptionInfo").GetComponent<TextMeshProUGUI>();

        var ghostIconMini = uiInstance.transform.Find("miniIcon").GetComponent<Image>();
        var ghostIconLarge = uiInstance.transform.Find("largeIcon").GetComponent<Image>();

        // Update UI elements with ghost data
        ghostName.text = ghost.ghostName;
        ghostType.text = ghost.type;
        abilityName.text = ghost.ability;
        skillAmount.text = ghost.skillAmount;
        ghostDescription.text = ghost.description;
        ghostIconMini.sprite = ghost.iconMini;
        ghostIconLarge.sprite = ghost.iconLarge;

        currentOverviewInstance = uiInstance;
    }

    private void DestroyCurrentOverview()
    {
        if (currentOverviewInstance != null)
        {
            Destroy(currentOverviewInstance);
        }
    }
}
