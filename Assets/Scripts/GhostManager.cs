using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 * What is left to do for this thing
 * 
 * I need to get the party roster which holds the Ghosts the player
 * has put into their party
 * 
 * I then need to determine what Ghost that is and then tie that to the combat
 * prefab
 * 
 * I could on the Ghost script attach the GhostBase script
 * then assign that to the battleScene
 * 
 */



public class GhostManager : MonoBehaviour
{
    public static GhostManager Instance;
    public List<Ghost> Ghosts = new List<Ghost>();

    public Transform ghostRoster;
    public GameObject miniGhost;
    public GameObject uiPrefab;
    public Transform overviewLocation;
    public Transform partyRoster;

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

        DestroyCurrentOverview();

        foreach (Transform ghost in ghostRoster)
        {
            Destroy(ghost.gameObject);
        }

        foreach (var ghost in Ghosts)
        {

            GameObject obj = Instantiate(miniGhost, ghostRoster);

            var ghostButton = obj.GetComponent<Button>();
            var ghostData = ghost;

            var dragAndDrop = obj.GetComponent<DragAndDrop>();
            if (dragAndDrop != null)
            {
                dragAndDrop.ghostData = ghost;
            }

            ghostButton.onClick.AddListener(() =>
            {
                OnGhostButtonClick(ghostData);
            });


            var ghostName = obj.transform.Find("ghostName").GetComponent<TextMeshProUGUI>();
            var ghostIconMini = obj.transform.Find("iconMini").GetComponent<Image>();

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

    public List<Ghost> GetPartyGhosts()
    {
        List<Ghost> partyGhosts = new List<Ghost>();

        foreach (Transform ghostTransform in partyRoster)
        {
            Ghost ghost = ghostTransform.GetComponent<Ghost>();
            if (ghost != null && Ghosts.Contains(ghost))
            {
                partyGhosts.Add(ghost);
                Debug.Log("Ghost in party roster: " + ghost.ghostName);
            }
        }

        return partyGhosts;
    }
}