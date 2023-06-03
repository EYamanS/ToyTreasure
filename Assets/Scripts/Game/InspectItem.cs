using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InspectItem : DraggableImage
{
    public GameObject InspectContent;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }


    public override void OnEndDrag(PointerEventData eventData)
    {
        // Re-enable raycasting on the image after dragging
        canvasGroup.blocksRaycasts = true;

        // Check if the image was dropped on top of another object
        GameObject droppedObject = eventData.pointerCurrentRaycast.gameObject;
        if (droppedObject != null && droppedObject.TryGetComponent<Inspector>(out Inspector _inspector))
        {
            _inspector.InspectItem(this);
            ResetPosition();
        }
        else
        {
            // Reset the position of the image to its original position
            rectTransform.anchoredPosition = originalPosition;
        }
    }
}
