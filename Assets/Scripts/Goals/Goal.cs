using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OyuncakHazinesiNerede.Managers;


public abstract class Goal : MonoBehaviour {

    
    public abstract bool isComplete();
    public virtual void OnComplete()
    {
        GameEvents.GoalComplete?.Invoke(this);        
    }

}
