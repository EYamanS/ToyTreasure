using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGoal : Goal
{
    public ItemContainer itemContainer;

    private void Awake()
    {
        itemContainer.assignedGoal = this;
    }

    public override bool isComplete()
    {
        return itemContainer.areItemsValid();
    }

    public void checkComplete()
    { 
    
    }
}
