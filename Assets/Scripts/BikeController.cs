using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BikeController : MonoBehaviour
{

    public static BikeController Instance
    {
        get;private set;
    }

    public enum BikeControlStates
    {
        InitState,
        StartMovingState,
        CanTapForBoostState,
        ReleaseCharacterFromBike
    }

    private Rigidbody2D body;

    [SerializeField] Rigidbody2D FrontWheel;
    [SerializeField] Rigidbody2D BackWheel;

    [SerializeField] WheelJoint2D FrontWheelJoint;
    [SerializeField] WheelJoint2D BackWheelJoint;

    RaycastHit2D rayHit_front;
    RaycastHit2D rayHit_back;

    private float wheelRadius;

    private Vector2 BikeDirection;

    private float MoveForce = 0;

    [SerializeField] Text SpeedText;

    public BikeControlStates CurrentBikeState = BikeControlStates.InitState;

    [SerializeField] Transform CharacterPose;

    public CharacterController CharacterController;

    private void Awake()
    {
        Instance = this;

        wheelRadius = FrontWheel.GetComponent<CircleCollider2D>().radius;

        body = this.GetComponent<Rigidbody2D>();

        CharacterController.transform.localPosition = CharacterPose.localPosition;
        CharacterController.transform.localRotation = CharacterPose.localRotation;
    }

    private void Start()
    {
        CurrentBikeState = BikeControlStates.InitState;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(CurrentBikeState == BikeControlStates.InitState)
            {
                CurrentBikeState = BikeControlStates.StartMovingState;
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 frontWheelStartPoint = FrontWheel.transform.position - (transform.up * (wheelRadius + 0.01f));
        Vector2 backWheelStartPoint = BackWheel.transform.position - (transform.up * (wheelRadius + 0.01f));

        rayHit_front = Physics2D.Raycast(frontWheelStartPoint, -transform.up, wheelRadius + 0.01f);
        rayHit_back = Physics2D.Raycast(backWheelStartPoint, -transform.up, wheelRadius + 0.01f);

        BikeDirection = FrontWheel.position - BackWheel.position;
        BikeDirection.Normalize();

        if(CurrentBikeState == BikeControlStates.StartMovingState)
        {
            MoveForce += 50f;
           
            ApplyTorqueBike();
        }

        SpeedText.text = "BikeSpeed: " + body.velocity.magnitude;
    }

    public void ApplyTorqueBike()
    {
        body.AddRelativeForce( MoveForce * BikeDirection * Time.deltaTime);
    }

    public void ReleaseCharacter()
    {
        if (CameraFollow.Instance) CameraFollow.Instance.Target = CharacterController.transform.GetChild(2);
        CharacterController.transform.parent = null;
        CharacterController.ReleaseCharacter();
        if(CurrentBikeState == BikeControlStates.StartMovingState)
        {
            CurrentBikeState = BikeControlStates.ReleaseCharacterFromBike;
        }

        body.velocity = Vector2.zero;
    }
}