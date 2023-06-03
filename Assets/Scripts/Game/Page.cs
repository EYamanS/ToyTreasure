using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Page : MonoBehaviour
{
    private bool _discovered = false;
    [SerializeField] private Goal[] _goals;
    public GameObject[] DiscoverReveals;
    public string helpString;

    [SerializeField] GameObject[] DeactivateOnRestart;

    public bool ReceivePhoneNotification;
    public GameObject phoneContent;
    public GameObject defaultPhoneContent;

    [Space(40)]
    [SerializeField] bool usePlayerPrefs;
    [SerializeField] string prefString;
    [SerializeField] TMP_Text prefText;


    public void LoadPhoneContent()
    {
        if (phoneContent == null)
        {
            defaultPhoneContent.SetActive(true);
        }
        else
        {
            defaultPhoneContent.SetActive(false);
            phoneContent.SetActive(true);

            phoneContent.transform.SetParent(defaultPhoneContent.transform.parent, false);
        }
    }

    public void OnPageLoad()
    {
        if (!_discovered) GameEvents.PageDiscovered?.Invoke(this);
        GameEvents.PageLoaded?.Invoke(this);
    }

    public void LoadPage()
    {
        // Handle username
        if (usePlayerPrefs)
        {
            string PlayerPrefStr = prefString.Split("{")[1].Split("}")[0];
            prefText.text = prefString.Replace("{" + PlayerPrefStr + "}", PlayerPrefs.GetString(PlayerPrefStr));
        }

        foreach (Goal goal in _goals) 
        {
            if (goal is ItemGoal)
            { 
                ItemGoal itemGoal = (ItemGoal)goal;
                foreach (DraggableImage item in itemGoal.itemContainer.GetItems())
                {
                    item.ResetPosition();
                    item.gameObject.SetActive(true);
                }
                itemGoal.itemContainer.DumpItems();
            }

            // handle other goal resets here.
            // if goal is AnotherGoal:

        }

        // Handle Phone Notifications
        if(ReceivePhoneNotification)
        {
            GameEvents.ReceivePhoneNotification?.Invoke();
        }

        gameObject.SetActive(true);
        OnPageLoad();
    }

    public void unLoadPage()
    {
        if (phoneContent != null)
        {
            foreach (GameObject go in DeactivateOnRestart)
            { go.SetActive(false); }
            phoneContent.SetActive(false);
            defaultPhoneContent.SetActive(true);
            phoneContent.transform.SetParent(transform, false);
        }
    }

    public Goal[] GetGoals() { return _goals; }

}
