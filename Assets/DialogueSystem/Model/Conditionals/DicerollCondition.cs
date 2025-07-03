using UnityEngine;
[System.Serializable]
enum DiceType : int
{
    D4 = 4,
    D6 = 6,
    D8 = 8,
    D10 = 10,
    D12 = 12,
    D20 = 20,
    D100 = 100
}


public class DicerollCondition : AbstractCondition
{

    [SerializeField]
    private DiceType diceSize;

    [SerializeField]
    private int bonus;

    [SerializeField]
    private int targetNumber;

    DicerollCondition(DiceType diceSize, int bonuses, int targetNumber)
    {
        this.diceSize = diceSize;
        this.bonus = bonuses;
        this.targetNumber = targetNumber;

    }

    public override bool EvaluateCondition()
    {
        int roll = Random.Range(1, (int)diceSize);
        return ((roll + bonus) >= targetNumber);

    }

}
