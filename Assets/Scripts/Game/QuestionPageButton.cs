using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestionPageButton : MonoBehaviour
{
    [SerializeField] DropdownGoal[] DropdownQuestions;
    [SerializeField] GameObject nextPage;
    [SerializeField] bool lastPage;

    public void TryGoNextPage()
    {
        bool foundfalse = false;
        foreach (DropdownGoal goal in DropdownQuestions)
        {
            DropdownGoal dropdown_goal = goal as DropdownGoal;
            if (!dropdown_goal.validateAnswer() && dropdown_goal._dropdownAsset.gameObject.activeInHierarchy)
            {
                GameEvents.SoftCheckWrong?.Invoke();
                foundfalse = true;
            }
        }

        if (!foundfalse)
        {
            if (!lastPage)
            {
                nextPage.SetActive(true);
                transform.parent.gameObject.SetActive(false);
            }
            else
            {
                GameEvents.GoalUpdate?.Invoke();
                Debug.Log("Checking results");
                transform.parent.parent.parent.gameObject.SetActive(false);
            }
        }
    }
}

