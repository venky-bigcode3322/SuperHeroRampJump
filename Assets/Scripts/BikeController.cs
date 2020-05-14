using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
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

    private float wheelRadius;

    private Vector2 BikeDirection;

    private float MoveForce = 0;

    [SerializeField] Text SpeedText;

    public BikeControlStates CurrentBikeState = BikeControlStates.InitState;

    [SerializeField] Transform CharacterPose;

    public CharacterController CharacterController;

    float initangle = 0f;

    private PolygonCollider2D boydCollider2D;

    private void Awake()
    {
        Instance = this;

        wheelRadius = FrontWheel.GetComponent<CircleCollider2D>().radius;

        body = this.GetComponent<Rigidbody2D>();

        CharacterController.transform.localPosition = CharacterPose.localPosition;
        CharacterController.transform.localRotation = CharacterPose.localRotation;

        boydCollider2D = GetComponent<PolygonCollider2D>();
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

            if (boydCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")) && !isCharacterReleased)
            {
                isCharacterReleased = true;
                ReleaseCharacter();
            }
        }

        currentPosition = transform.position;
        currentPosition.y = Mathf.Clamp(currentPosition.y, 0, 50);
        transform.position = currentPosition;
    }

    bool applyforce = false;

    private Vector3 currentPosition;

    [SerializeField] Vector2 dirOffset;

    bool isGrounded = false;

    private void FixedUpdate()
    {
        Vector2 frontWheelStartPoint = FrontWheel.transform.position - (transform.up * (wheelRadius + 0.01f));
        Vector2 backWheelStartPoint = BackWheel.transform.position - (transform.up * (wheelRadius + 0.01f));

        if(CurrentBikeState == BikeControlStates.StartMovingState)
        {
            BikeDirection = FrontWheel.position - BackWheel.position;
            BikeDirection.Normalize();

            MoveForce += 650;
           
            ApplyTorqueBike();
        }

        if (CurrentBikeState == BikeControlStates.CanTapForBoostState)
        {
            if (applyforce)
            {
                Vector2 dir;
                dir = Vector2.up + dirOffset + Vector2.right;
                dir.Normalize();
                if(GlobalVariables.FuelPercentage > 0)
                body.AddRelativeForce(dir * 75, ForceMode2D.Impulse);
            }

            // transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, targetangle), angleResetTime);//,EvalutateEaseIn(multiplier));

            if (valuesync == 0)
            {
                body.angularVelocity = 10;
                timer += 0.1f;
                if (timer > 10) { valuesync = 1; timer = 0; }
            }
            else if (valuesync == 1)
            {
                body.angularVelocity = -10;
                timer += 0.1f;
                if (timer > 20) { valuesync = 2; timer = 0; }
            }
            else if (valuesync == 2)
            {
                body.angularVelocity = 10;
                timer += 0.1f;
                if (timer > 15) { valuesync = 3; timer = 0; }
            }
            else if (valuesync == 3)
            {
                body.angularVelocity = -10;
                timer += 0.1f;
                if (timer > 10) { valuesync = 0; timer = 0; }
            }
        }
    }

    int valuesync = 0;
    float timer = 0f;

    public void ApplyTorqueBike()
    {
        body.AddRelativeForce( MoveForce * BikeDirection * Time.deltaTime);
    }

    public void ActivateFuel()
    {
        CurrentBikeState = BikeControlStates.CanTapForBoostState;
        initangle = transform.eulerAngles.z;
       // targetangle = initangle - 20;
        body.angularVelocity = 0;
        FrontWheel.simulated = false;
        BackWheel.simulated = false;
    }

    public void ReleaseCharacter()
    {
        if (CameraManager.Instance) CameraManager.Instance.target = CharacterController.transform.GetChild(2);
        CameraManager.Instance.offset = Vector3.zero;
     
        CharacterController.ReleaseCharacter(body.velocity.magnitude / 2);
        if (CurrentBikeState == BikeControlStates.StartMovingState)
        {
            CurrentBikeState = BikeControlStates.ReleaseCharacterFromBike;
        }

        body.velocity = Vector3.zero;
        body.drag = 1;
        body.mass = 10000000;

        CharacterController.transform.parent = null;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector2 direction = (Vector2.up + dirOffset + Vector2.right) * 5;
        Gizmos.DrawRay(transform.position, direction);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            Debug.LogError("TouchedGround");
        }
    }
}