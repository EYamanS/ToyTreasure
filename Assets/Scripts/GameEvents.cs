using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents
{
    public static Action<Goal> GoalComplete;
    public static Action<Page> PageDiscovered;
    public static Action<Page> PageLoaded;

    public static Action GoalUpdate;
    public static Action<Goal> GoalUpdateSingle;

    public static Action phoneOpened;
    public static Action helpOpened;

    public static Action ReceivePhoneNotification;
    public static Action<ItemContainer, bool> showContainerState;

    public static Action<InspectItem> inspectItem;

    public static Action SoftCheckWrong;
    public static Action<DropdownGoal> DropdownGoalChange;
    public static Action RestartLevel;
}


