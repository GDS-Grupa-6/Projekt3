using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHudReferences : MonoBehaviour
{
    public  Slider EnergySlider;
    public TextMeshProUGUI EnergyCounterText;
    [Space(10)]
    public Slider HealthSlider;
    public TextMeshProUGUI HealthCounterText;
    [Space(10)]
    public Image ViewFinder;
    public TextMeshProUGUI[] InputTexts;
}
