using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inspector : MonoBehaviour
{
    public void InspectItem(InspectItem item)
    {
        GameEvents.inspectItem?.Invoke(item);
        Debug.Log(item.ToString());
    }
}
