using UnityEngine;

[System.Serializable]
public class AbstractCondition
{
    public virtual bool EvaluateCondition()
    {
        return true;
    }
    
}
