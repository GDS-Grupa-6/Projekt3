using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerReferences : MonoBehaviour
{
    [SerializeField] public GameObject Player;
    [SerializeField] public Animator PlayerAnimator;
    [SerializeField] public Rig[] PlayerRigs;
    [SerializeField] public Transform OneHandShootPoint;
    [SerializeField] public Transform TwoHandsShootPoint;
    [SerializeField] public GameObject SecondWeapon;
    [SerializeField] public Transform PlayerGroundCheck;
    [SerializeField] public GameObject[] FireStateVfx;
    [SerializeField] public GameObject[] NorrmalStateVfx;
}
