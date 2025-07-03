using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConditionalList
{
    [SerializeField]
    public List<AbstractCondition> conditions;


    public bool EvaluateConditions()
    {
        foreach (AbstractCondition condition in conditions)
        {
            if (!condition.EvaluateCondition())
            {
                return false;
            }
        }
        return true;
    }

}
