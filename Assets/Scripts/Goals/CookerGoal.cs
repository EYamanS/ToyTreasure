using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CookerGoal : Goal
{
    [SerializeField] float _cookTime;
    [SerializeField] float cookDegree = 1538f;
    private bool didCook = false;
    private bool startedCooking = false;
    private TransformingItem transformComponent;
    [SerializeField] TMP_Text sayac;

    [SerializeField] GameObject[] revealOnCook;



    public override bool isComplete()
    {
        return didCook;
    }
    private void Awake()
    {
        transformComponent = GetComponent<TransformingItem>();
    }
    public void startCooking()
    {
        if (!transformComponent.isTransformed) { return; }
        if (startedCooking) { return; }


        startedCooking = true;  
        StartCoroutine(cookForT(_cookTime));
        StartCoroutine(incrementForT(_cookTime, .05f));
    }

    IEnumerator incrementForT(float time, float timeIntervals)
    {
        float currentDegree = 0;
        float degreeIncrement = cookDegree / (time / timeIntervals);

        while (currentDegree <= cookDegree + degreeIncrement) 
        {
            yield return new WaitForSeconds(.05f);
            sayac.text = currentDegree.ToString() + "°C";
            currentDegree += degreeIncrement;
        }

    }

    IEnumerator cookForT(float t)
    {
        yield return new WaitForSeconds(t);
        didCook = true;
        GameEvents.GoalUpdate?.Invoke();

        foreach (GameObject go in revealOnCook) { go.SetActive(true); }    
    }

}
