using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 offset;
    private Transform ghostRosterTransform;

    // Reference to the Ghost associated with this draggable button
    public Ghost ghostData;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        ghostRosterTransform = GameObject.Find("ghostRoster").transform;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out offset);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = eventData.position + offset; //Fixes the grab position of the mouse... I think
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.isValid)
        {
            GameObject panel = eventData.pointerCurrentRaycast.gameObject;

            if (panel.CompareTag("PartyMemberPanel"))
            {
                rectTransform.SetParent(panel.transform);
                rectTransform.localPosition = Vector3.zero;

                if (ghostData != null)
                {
                    GameManager.instance.AddGhostToParty(ghostData.gameObject, ghostData);
                }
                else
                {
                    Debug.LogWarning("No Ghost data found on the dropped ghost object.");
                }
            }
            else
            {
                // Check if the ghost was previously in the party roster
                bool wasInParty = GameManager.instance.partyRosterGhosts.Contains(ghostData);

                rectTransform.SetParent(ghostRosterTransform);
                RemoveGhostFromPartyFromAnyPanel(ghostData.gameObject, ghostData);

                // If the ghost was previously in the party roster, remove it from the party roster
                /*
                if (wasInParty)
                {
                    //GameManager.instance.RemoveGhostFromParty(ghostData.gameObject);
                }
                */
            }
        }
    }

    private void RemoveGhostFromPartyFromAnyPanel(GameObject ghostObject, Ghost ghost)
    {
        if (ghost != null)
        {
            Debug.Log("Removing ghost from the party: " + ghost.ghostName);
        }
        else
        {
            Debug.LogWarning("No Ghost data found on the ghost object.");
        }

        GameManager.instance.RemoveGhostFromParty(ghostObject, ghost);

    }


}