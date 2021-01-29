using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Condition
{
    public string name;
    public int amount;
}

[CreateAssetMenu(fileName = "New Condition List", menuName = "ConditionList")]
public class ConditionList : ScriptableObject {
    public Condition[] conditions;
}
