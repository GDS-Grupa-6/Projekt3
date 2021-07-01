using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { Attack, DoubleAttack, TriplleAttack, QuatroAttack, MegaAttack }

[System.Serializable]
public class MeleStates
{
    public State state;
    public float power;
    public float range;
    public float pointsForAttack;
}
