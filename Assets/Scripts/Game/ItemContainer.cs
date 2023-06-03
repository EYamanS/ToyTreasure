using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    private List<DraggableImage> m_Items = new List<DraggableImage>();
    public ItemType m_ItemType;
    public Goal assignedGoal;

    public bool areItemsValid() 
    {
        foreach (DraggableImage _item in FindObjectsByType<DraggableImage>(FindObjectsInactive.Exclude, FindObjectsSortMode.InstanceID))
        {
            if (_item.type == m_ItemType) { return false; }
        }

        foreach (DraggableImage item in m_Items)
        {
            if (item.type == m_ItemType)
                continue;

            else return false;
        }

        return true;
    }

    public void addItem(DraggableImage Item)
    {
        m_Items.Add(Item);      
        Item.gameObject.SetActive(false);
    }


    public void checkItemsInside()
    {
        Debug.Log("Checking items inside");
        GameEvents.showContainerState?.Invoke(this, areItemsValid());
        GameEvents.GoalUpdate?.Invoke();
    }

    public void DumpItems()
    {
        m_Items = new List<DraggableImage>();
        GameEvents.GoalUpdate?.Invoke();
    }
    public List<DraggableImage> GetItems()
    {
        return m_Items;
    }




}
