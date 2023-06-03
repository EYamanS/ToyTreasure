using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DropdownGoal : Goal
{   
    public TMP_Dropdown _dropdownAsset;
    [SerializeField] int _answer;
    [SerializeField] bool doNotifyGoalComplete = true;

    public override bool isComplete()
    {
        return _answer == _dropdownAsset.value;
    }

    private void Awake()
    {
        GameEvents.phoneOpened += ResetColors;
        _dropdownAsset.onValueChanged.AddListener(CheckVal);
    }

    private void CheckVal(int val)
    {
        GameEvents.DropdownGoalChange?.Invoke(this);

        if (doNotifyGoalComplete)
            GameEvents.GoalUpdate?.Invoke();
    }

    public bool validateAnswer()
    {
        if (isComplete())
        {
            ColorBlock TrueColorBlock = _dropdownAsset.colors;
            TrueColorBlock.normalColor = Color.green;
            _dropdownAsset.colors = TrueColorBlock;
            return true;
        }

        else
        {
            ColorBlock TrueColorBlock = _dropdownAsset.colors;
            TrueColorBlock.normalColor = Color.red;
            _dropdownAsset.colors = TrueColorBlock;
            return false;
        }
    }

    private void ResetColors()
    {
        ColorBlock TrueColorBlock = _dropdownAsset.colors;
        TrueColorBlock.normalColor = Color.white;
        _dropdownAsset.colors = TrueColorBlock;
    }
}
