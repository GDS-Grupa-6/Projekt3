using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerLogic : MonoBehaviour
{
    //Inspector serialized
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float directionDampTime = 0.25f;
    [SerializeField]
    private GameObject gamecam;
    [SerializeField]
    private float directionSpeed = 3.0f;
    [SerializeField]
    private float rotationDegreePerSecond = 120f;
    [SerializeField]
    private float speedDampTime = 0.05f;
    [SerializeField]
    private InputManager inputManager;

    //Private global only
    private float horizontal = 0.0f;
    private float vertical = 0.0f;
    private AnimatorStateInfo stateInfo;
    private float speed = 0.0f;
    private float direction = 0f;
    private float charAngle = 0f;

    //hashes
    private int m_LocomotionId = 0;
    private int m_LocomotionPivotLId = 0;
    private int m_LocomotionPivotRId = 0;

    public Animator Animator
    {
        get
        {
            return this.animator;
        }
    }

    public float Speed
    {
        get
        {
            return this.speed;
        }
    }

    public float LocomotionThreshold { get { return 0.2f; } }


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

            if(animator.layerCount >=2)
            {
                animator.SetLayerWeight(1, 1);
            }

        //Hash all animation names performance
        m_LocomotionId = Animator.StringToHash("Base Layer.Locomotion");
        m_LocomotionPivotLId = Animator.StringToHash("Base Layer.LocomotionPivotL");
        m_LocomotionPivotRId = Animator.StringToHash("Base Layer.LocomotionPivotR");

    }

    // Update is called once per frame
    void Update()
    {
        if (animator)
        {
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            // pull values from controller/keyboard
            horizontal = inputManager.MovementControls().x;
            vertical = inputManager.MovementControls().y;

            charAngle = 0f;
            direction = 0f;


            //Translate controls stick coordinates into world/cam/character space
            StickToWorldspace(this.transform, gamecam.transform, ref direction, ref speed, ref charAngle, IsInPivot());

            animator.SetFloat("Speed", speed, speedDampTime, Time.deltaTime);
            animator.SetFloat("Direction", direction, directionDampTime, Time.deltaTime);
            


            if (speed > LocomotionThreshold) //dead zone
            {
                if (!IsInPivot())
                {
                    animator.SetFloat("Angle", charAngle);
                }
            }
            if (speed < LocomotionThreshold && Mathf.Abs(horizontal) < 0.05f)
            {
                animator.SetFloat("Direction", 0f);
                animator.SetFloat("Angle", 0f);

            }


        }
    }

    void FixedUpdate()
    {
        //Rotate character model if stick os tilted right or left, but only if character is moving that direction
        if (IsInLocomotion() && ((direction >= 0 && horizontal >=0) || (direction < 0 && horizontal < 0)))
        {
            Vector3 rotationAmount = Vector3.Lerp(Vector3.zero, new Vector3(0f, rotationDegreePerSecond * (horizontal < 0f ? -1f : 1f), 0f), Mathf.Abs(horizontal));
            Quaternion deltaRotation = Quaternion.Euler(rotationAmount * Time.deltaTime);
            this.transform.rotation = (this.transform.rotation * deltaRotation);
        }
    }


    public bool IsInPivot()
    {
        return stateInfo.fullPathHash == m_LocomotionPivotLId || stateInfo.fullPathHash == m_LocomotionPivotRId;
    }

    public bool IsInLocomotion()
    {
        return stateInfo.fullPathHash == m_LocomotionId;
    }

    public void StickToWorldspace(Transform root, Transform camera, ref float directionOut, ref float speedOut, ref float angleOut, bool isPivoting)
    {
        Vector3 rootDirection = root.forward;

        Vector3 stickDirection = new Vector3(horizontal, 0, vertical);

        speedOut = stickDirection.sqrMagnitude;

        //Get camera rotation
        Vector3 CameraDirection = camera.forward;
        CameraDirection.y = 0.0f; //kill Y
        Quaternion referentialShift = Quaternion.FromToRotation(Vector3.forward, CameraDirection);

        //Convert joystick input in Worldspace coordinates
        Vector3 moveDirection = referentialShift * stickDirection;
        Vector3 axisSign = Vector3.Cross(moveDirection, rootDirection);

        Debug.DrawRay(new Vector3(root.position.x, root.position.y + 2f, root.position.z), moveDirection, Color.green);
        Debug.DrawRay(new Vector3(root.position.x, root.position.y + 2f, root.position.z), axisSign, Color.red);
        Debug.DrawRay(new Vector3(root.position.x, root.position.y + 2f, root.position.z), rootDirection, Color.magenta);
//      Debug.DrawRay(new Vector3(root.position.x, root.position.y + 2f, root.position.z), stickDirection, Color.blue);

      float angleRootToMove = Vector3.Angle(rootDirection, moveDirection) * (axisSign.y >= 0 ? -1f : 1f);

        if (!isPivoting)
        {
            angleOut = angleRootToMove;
        }

      angleRootToMove /= 180f;
        
      directionOut = angleRootToMove * directionSpeed;
    }
}
