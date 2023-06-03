using UnityEditor.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public enum ItemType
{
    Katı, Sıvı, Gaz
}

public class DraggableImage : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public ItemType type;
    protected RectTransform rectTransform;
    protected CanvasGroup canvasGroup;
    protected Vector2 originalPosition;
    public bool isMoveable = true;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Save the original position of the image
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Disable raycasting on the image while dragging
        canvasGroup.blocksRaycasts = false;
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        // Re-enable raycasting on the image after dragging
        canvasGroup.blocksRaycasts = true;

        // Check if the image was dropped on top of another object
        GameObject droppedObject = eventData.pointerCurrentRaycast.gameObject;
        if (droppedObject != null && droppedObject.TryGetComponent<ItemContainer>(out ItemContainer _container))
        {
            _container.addItem(this);
        }
        else
        {
            // Reset the position of the image to its original position
            ResetPosition();
        }
    }

    public void ResetPosition()
    {
        rectTransform.anchoredPosition = originalPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Move the image according to the mouse position
        if (isMoveable)
        {
            rectTransform.anchoredPosition += eventData.delta / GetCanvasScale();
        }
    }

    private float GetCanvasScale()
    {
        // Calculate the scale factor of the canvas
        Canvas canvas = GetComponentInParent<Canvas>();
        return canvas.scaleFactor;
    }
}
