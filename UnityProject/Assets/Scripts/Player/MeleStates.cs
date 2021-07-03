using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { Attack, DoubleAttack, TriplleAttack, QuatroAttack, MegaAttack }

[System.Serializable]
public class MeleStates
{
    [Range(0, 4)] public int stateNumber;
    public float power;
    public int pointsForAttack;
}
