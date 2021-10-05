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
    [Space(10)]
    public Image DashImage;
    public GameObject DashLocked;
    public Image StateImage;
    public GameObject StateLocked;
    public Image Weapon2Image;
    [Space(10)]
    public Sprite DashSprite;
    public Sprite FireSprite;
    public Sprite NormalStateSprite;
    [Space(10)]
    public GameObject PopUp;
    public TextMeshProUGUI PopUpText;
}
