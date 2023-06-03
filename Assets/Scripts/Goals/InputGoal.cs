using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using System;

public class InputGoal : Goal
{
    [SerializeField] TMP_InputField _inputField;

    public override bool isComplete()
    {
        if (_inputField.text != "")
        {
            return true;
        }
        else return false;
    }

    private void Awake()
    {
        _inputField.onValueChanged.AddListener(GoalUpdate);
    }

    private void GoalUpdate(string arg0)
    {
        GameEvents.GoalUpdate?.Invoke();
        PlayerPrefs.SetString("Playername",_inputField.text);
    }

}

