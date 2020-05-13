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

    private bool isCharacterReleased = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(CurrentBikeState == BikeControlStates.InitState)
            {
                CurrentBikeState = BikeControlStates.StartMovingState;
            }
        }

        if (CurrentBikeState == BikeControlStates.CanTapForBoostState)
        {
            if (Input.GetMouseButton(0) && GlobalVariables.FuelPercentage > 0)
            {
                applyforce = true;
                GlobalVariables.FuelPercentage -= Time.deltaTime * 20;
                GameManager.Instance.CheckFuelHUD();

                if (!isCharacterReleased && GlobalVariables.FuelPercentage <= 0)
                {
                    isCharacterReleased = true;
                    ReleaseCharacter();
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                applyforce = false;
            }
        }
    }

    bool applyforce = false;

    private Vector3 currentPosition;

    private void FixedUpdate()
    {
        Vector2 frontWheelStartPoint = FrontWheel.transform.position - (transform.up * (wheelRadius + 0.01f));
        Vector2 backWheelStartPoint = BackWheel.transform.position - (transform.up * (wheelRadius + 0.01f));

        rayHit_front = Physics2D.Raycast(frontWheelStartPoint, -transform.up, wheelRadius + 0.01f);
        rayHit_back = Physics2D.Raycast(backWheelStartPoint, -transform.up, wheelRadius + 0.01f);

        if(CurrentBikeState == BikeControlStates.StartMovingState)
        {
            BikeDirection = FrontWheel.position - BackWheel.position;
            BikeDirection.Normalize();

            MoveForce += 100;
           
            ApplyTorqueBike();
        }

        if (CurrentBikeState == BikeControlStates.CanTapForBoostState)
        {
            if (applyforce)
            {
                float speed = body.velocity.magnitude;
                float angularSpeed = body.angularVelocity;

                Vector2 dir;
                //if (transform.position.y >= 50)
                //    dir = Vector2.right;
                //else
                    dir = Vector2.up + Vector2.right;

                dir.Normalize();
                body.AddForce(transform.right * GlobalVariables.FuelPercentage, ForceMode2D.Impulse);

                body.angularVelocity = 0;
                //body.gravityScale = 0.5f;

                //var q = Quaternion.LookRotation(dir);//(Vector2.up + Vector2.left);
                //body.MoveRotation(Quaternion.RotateTowards(transform.rotation,  q, 5 * Time.deltaTime));
            }
        }
        //currentPosition = transform.position;
        //currentPosition.y = Mathf.Clamp(currentPosition.y,0,100);
        //transform.position = currentPosition;
    }

    public void ApplyTorqueBike()
    {
        Debug.Log("applying torque");
        body.AddRelativeForce( MoveForce * BikeDirection * Time.deltaTime);
    }

    public void ActivateFuel()
    {
        CurrentBikeState = BikeControlStates.CanTapForBoostState;
        FrontWheel.simulated = false;
        BackWheel.simulated = false;
        
    }

    public void ReleaseCharacter()
    {
        if (CameraManager.Instance) CameraManager.Instance.target = CharacterController.transform.GetChild(2);
        CharacterController.transform.parent = null;
        CharacterController.ReleaseCharacter();
        if (CurrentBikeState == BikeControlStates.StartMovingState)
        {
            CurrentBikeState = BikeControlStates.ReleaseCharacterFromBike;
        }

        //body.velocity = body.velocity / 4;
        //body.mass = 10000000;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector2 direction = transform.TransformDirection(Vector2.up + Vector2.left) * 5;
        Gizmos.DrawRay(transform.position, direction);
    }
}