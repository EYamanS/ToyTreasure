using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTracker : MonoBehaviour
{
    [SerializeField] private Goal[] _currentGoals;

    public void UpdateCurrentGoals(Page page)
    {
        _currentGoals = page.GetGoals();
    }

    public bool IsPageComplete()
    {
        for (int i = 0; i < _currentGoals.Length; i++)
        {
            Goal goal = _currentGoals[i];
            if (goal.isComplete() == false) return false;
        }
        return true;
    }


    public void SoftCheckGoals()
    { 
        foreach (Goal goal in _currentGoals) 
        {
            if ((goal is DropdownGoal)) 
            {
                DropdownGoal dropdown_goal = goal as DropdownGoal;
                if (!dropdown_goal.validateAnswer() && dropdown_goal._dropdownAsset.gameObject.activeInHierarchy)
                { 
                    GameEvents.SoftCheckWrong?.Invoke();
                }
            }
        }
    }

}

