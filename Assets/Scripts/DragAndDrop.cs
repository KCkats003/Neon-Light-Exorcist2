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
    private Transform partyRosterTransform;

    // Reference to the Ghost associated with this draggable button
    public Ghost ghostData;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        ghostRosterTransform = GameObject.Find("ghostRoster").transform;
        partyRosterTransform = GameObject.Find("partyRoster").transform; // Assuming "partyRoster" is the name of the target party roster transform
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out offset);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = eventData.position + offset;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.isValid)
        {
            Transform targetTransform = eventData.pointerCurrentRaycast.gameObject.transform;

            if (targetTransform == partyRosterTransform) // Check if the target transform is the party roster transform
            {
                rectTransform.SetParent(targetTransform);
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
            else if (targetTransform == ghostRosterTransform) // Check if the target transform is the ghost roster transform
            {
                rectTransform.SetParent(targetTransform);
                rectTransform.localPosition = Vector3.zero;

                // Remove the ghost from the party roster
                GameManager.instance.RemoveGhostFromParty(ghostData.gameObject, ghostData);
            }
            else
            {
                // Revert the ghost to its original position if dropped on neither roster
                rectTransform.SetParent(ghostRosterTransform); // Or any default parent
                rectTransform.localPosition = Vector3.zero;
            }
        }
    }
}
