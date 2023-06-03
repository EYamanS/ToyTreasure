using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TransformingItem : DraggableImage
{
    [SerializeField] TransformingItem ItemTarget;
    [SerializeField] Sprite UpgradedImage;
    [SerializeField]
    GameObject[] revealOnTransform;

    [Space(10)]
    [SerializeField] bool destroyOnTransform = false;
    [Space(10)]

    Image image;
    Sprite defaultSprite;
    public bool isTransformed = false;

    [Space(20)]
    [SerializeField] bool multipleTarget = false;


    private void Awake()
    {
        image = GetComponent<Image>();
        defaultSprite = image.sprite;
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalPosition = rectTransform.anchoredPosition;

    }
    public void AllowMovement()
    {
        isMoveable = true;
    }
    public void ResetItem()
    {
        image.sprite = defaultSprite;
        isTransformed = false;
    }

    public virtual void Transform(bool multi = false)
    {
        if (UpgradedImage != null)
        {
            image.sprite = UpgradedImage;
        }

        isTransformed = true;
        image.SetNativeSize();
        rectTransform.anchoredPosition = originalPosition;
        GameEvents.GoalUpdate?.Invoke();

        if (!multi && !ItemTarget.isTransformed) ItemTarget.Transform();

        foreach (GameObject go in revealOnTransform)
        {
            go.SetActive(true);
        }

        if(destroyOnTransform) { Destroy(gameObject); }
    }

    private void multiTransform(TransformingItem _otherItem)
    { 
        _otherItem.Transform(multi:true);
    }


    public override void OnEndDrag(PointerEventData eventData)
    {
        // Re-enable raycasting on the image after dragging
        canvasGroup.blocksRaycasts = true;

        // Check if the image was dropped on top of another object
        GameObject droppedObject = eventData.pointerCurrentRaycast.gameObject;
        if (droppedObject != null)
        {
            if (!multipleTarget && droppedObject.TryGetComponent<TransformingItem>(out TransformingItem _otherItem) && _otherItem == ItemTarget) Transform();
            else 
            {
                if (droppedObject.TryGetComponent<TransformingItem>(out TransformingItem _otherItem2))
                    multiTransform(_otherItem2); 
            }
        }
        else
        {
            // Reset the position of the image to its original position
            rectTransform.anchoredPosition = originalPosition;
        }
    }
}
