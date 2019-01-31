using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BoosterType { SPEED_UP, SLOW_DOWN,INCREASE, DECREASE, REVERSE }
[CreateAssetMenu(fileName ="BoosterData")]
public class BoosterData : ScriptableObject {

    public int boostValue;
    public BoosterType boosterType;
    public Color color;
}
