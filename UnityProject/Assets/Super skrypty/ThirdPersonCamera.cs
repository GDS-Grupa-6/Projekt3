using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{

    [SerializeField]
    private float distanceAway;
    [SerializeField]
    private float distanceAwayMultiplier = 1.5f;
    [SerializeField]
    private float distanceUpMultiplier = 5f;
    [SerializeField]
    private float distanceUp;
    [SerializeField]
    private float smooth;
    [SerializeField]
    private Transform followXForm;
    [SerializeField]
    private Vector3 offset = new Vector3(0f, 1.5f, 0f);
    [SerializeField]
    private float freeTreshold = -0.1f;
    [SerializeField]
    private Vector2 camMinDistFromChar = new Vector2(1f, -0.5f);
    [SerializeField]
    private float rightStickThreshold = 0.1f;
    [SerializeField]
    private float freeRotationDegreePerSecond = -5;

    //private global only
    private Vector3 lookDir;
    private Vector3 targetPosition;
    private Vector3 savedRigToGoal;
    private float distanceAwayFree;
    private float distanceUpFree;
    private Vector2 rightStickPrevFrame = Vector2.zero;


    //Smoothing and damping
    private Vector3 velocityCamSmooth = Vector3.zero;
    [SerializeField]
    private float camSmoothDampTime = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        followXForm = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        Vector3 characteroffset = followXForm.position + new Vector3(0f, distanceUp, 0f);

        //Calculate direction from camera to player, kill Y and normalize to give a valid direction with unit magnitude
        lookDir = characteroffset - this.transform.position;
        lookDir.y = 0;
        lookDir.Normalize();
        Debug.DrawRay(this.transform.position, lookDir, Color.green);



        // setting the target position to be the correct offset from the hovercraft
        targetPosition = characteroffset + followXForm.up * distanceUp - lookDir * distanceAway;
        //      Debug.DrawRay(followXForm.position, Vector3.up * distanceUp, Color.red);
        //      Debug.DrawRay(followXForm.position, -1f * followXForm.forward * distanceAway, Color.blue);
        Debug.DrawLine(followXForm.position, targetPosition, Color.magenta);

        CompensateForWalls(characteroffset, ref targetPosition);

        // making a smooth transition between its current position and the position it wants to be in
        smoothPosition(this.transform.position, targetPosition);


        // make sure the camera is looking right way!
        transform.LookAt(followXForm);
    }

    private void smoothPosition(Vector3 fromPos, Vector3 toPos)
    {
        // making a smooth transition between its current position and the position it wants to be in
        this.transform.position = Vector3.SmoothDamp(fromPos, toPos, ref velocityCamSmooth, camSmoothDampTime);
    }

    private void CompensateForWalls(Vector3 fromObject, ref Vector3 toTarget)
    {
        Debug.DrawLine(fromObject, toTarget, Color.cyan);
        //Compensate the walls between camera
        RaycastHit wallHit = new RaycastHit();
        if (Physics.Linecast(fromObject, toTarget, out wallHit))
        {
            Debug.DrawRay(wallHit.point, Vector3.left, Color.red);
            toTarget = new Vector3(wallHit.point.x, toTarget.y, wallHit.point.z);
        }
    }
}
