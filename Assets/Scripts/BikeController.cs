using UnityEngine;
using UnityEngine.EventSystems;

public class BikeController : MonoBehaviour
{
    public static BikeController instance
    {
        get;private set;
    }

    public enum BikeControlStates
    {
        None,
        InitState,
        StartMovingState,
        CanTapForBoostState,
        ReleaseCharacterFromBike
    }

    private Rigidbody2D _body;

     [SerializeField] private Rigidbody2D frontWheel;
     [SerializeField] private Rigidbody2D backWheel;

    [SerializeField]
    private WheelJoint2D frontWheelJoint;
    [SerializeField]
    private WheelJoint2D backWheelJoint;

    private float _wheelRadius;

    private Vector2 _bikeDirection;

    private float _moveForce = 0;

    public BikeControlStates currentBikeState = BikeControlStates.InitState;

    [SerializeField]
    private Transform characterPose;

    public CharacterController characterController;

    private float _initangle = 0f;

    [SerializeField] private int driverPoseIndex = 0;

    private Collider2D _boydCollider2D;

    [SerializeField] private Collider _dummyCollider;

    [SerializeField] GameObject[] BoosterParticles;

    private void Awake()
    {
        instance = this;

        _wheelRadius = frontWheel.GetComponent<CircleCollider2D>().radius;

        _body = this.GetComponent<Rigidbody2D>();

        _boydCollider2D = GetComponent<BoxCollider2D>();

        for (int i = 0; i < BoosterParticles.Length; i++)
            BoosterParticles[i].SetActive(false);
    }

    private void OnDestroy()
    {
      //  instance = null;
    }

    private void Start()
    {
        ActivateSuperHeroCharacter();
    }

    public void ActivateSuperHeroCharacter()
    {
        if(instance == null)
            instance = this;

        if (characterController != null)
            Destroy(characterController.gameObject);

        characterController = GameManager.instance.InstantiateSuperHero(characterPose);
        characterController.SetDriverIdlePose(driverPoseIndex);
        currentBikeState = BikeControlStates.None;
    }

    private bool _isCharacterReleased = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && currentBikeState != BikeControlStates.None)
        {
            if(currentBikeState == BikeControlStates.InitState)
            {
                characterController.SetDriverPose(driverPoseIndex);
                currentBikeState = BikeControlStates.StartMovingState;
                //characterController.SetDriverPose(driverPoseIndex);
            }
        }

        if (currentBikeState == BikeControlStates.CanTapForBoostState)
        {
            if (Input.GetMouseButton(0) && GlobalVariables.FuelPercentage > 0)
            {
                _applyforce = true;
                GlobalVariables.FuelPercentage -= Time.deltaTime * 20;
                GameManager.instance.CheckFuelHud();
                CameraManager.Instance.lerpDistance = 18;

                for (int i = 0; i < BoosterParticles.Length; i++)
                    BoosterParticles[i].SetActive(true);

                if (!_isCharacterReleased && GlobalVariables.FuelPercentage <= 0)
                {
                    _isCharacterReleased = true;
                    ReleaseCharacter();
                    CameraManager.Instance.lerpDistance = 15f;

                    for (int i = 0; i < BoosterParticles.Length; i++)
                        BoosterParticles[i].SetActive(false);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                CameraManager.Instance.lerpDistance = 15f;
                _applyforce = false;
                for (int i = 0; i < BoosterParticles.Length; i++)
                    BoosterParticles[i].SetActive(false);
            }

            if (_boydCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")) && !_isCharacterReleased)
            {
                _isCharacterReleased = true;
                ReleaseCharacter();
            }
        }

        _currentPosition = transform.position;
        _currentPosition.y = Mathf.Clamp(_currentPosition.y, 0, 50);
        transform.position = _currentPosition;
    }

    private bool _applyforce = false;

    private Vector3 _currentPosition;

    [SerializeField] private Vector2 dirOffset;

    private bool _isGrounded = false;

    private void FixedUpdate()
    {
        var up = transform.up;
        Vector2 frontWheelStartPoint = frontWheel.transform.position - (up * (_wheelRadius + 0.01f));
        Vector2 backWheelStartPoint = backWheel.transform.position - (up * (_wheelRadius + 0.01f));

        if(currentBikeState == BikeControlStates.StartMovingState)
        {
            _bikeDirection = frontWheel.position - backWheel.position;
            _bikeDirection.Normalize();

            _moveForce += 650;
           
            ApplyTorqueBike();
        }

        if (currentBikeState == BikeControlStates.CanTapForBoostState)
        {
            if (_applyforce)
            {
                var dir = Vector2.up + dirOffset + Vector2.right;
                dir.Normalize();
                if(GlobalVariables.FuelPercentage > 0)
                _body.AddRelativeForce(dir * 75, ForceMode2D.Impulse);
            }

            // transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, targetangle), angleResetTime);//,EvalutateEaseIn(multiplier));

            if (_valuesync == 0)
            {
                _body.angularVelocity = 10;
                _timer += 0.1f;
                if (_timer > 10) { _valuesync = 1; _timer = 0; }
            }
            else if (_valuesync == 1)
            {
                _body.angularVelocity = -10;
                _timer += 0.1f;
                if (_timer > 20) { _valuesync = 2; _timer = 0; }
            }
            else if (_valuesync == 2)
            {
                _body.angularVelocity = 10;
                _timer += 0.1f;
                if (_timer > 15) { _valuesync = 3; _timer = 0; }
            }
            else if (_valuesync == 3)
            {
                _body.angularVelocity = -10;
                _timer += 0.1f;
                if (_timer > 10) { _valuesync = 0; _timer = 0; }
            }
        }
    }

    private int _valuesync = 0;
    private float _timer = 0f;

    private void ApplyTorqueBike()
    {
        _body.AddRelativeForce( _bikeDirection * (_moveForce * Time.deltaTime));
    }

    public void ActivateFuel()
    {
        currentBikeState = BikeControlStates.CanTapForBoostState;
        _initangle = transform.eulerAngles.z;
       // targetangle = initangle - 20;
        _body.angularVelocity = 0;
        frontWheel.simulated = false;
        backWheel.simulated = false;
        characterController._CalculateDistance = true;
    }

    private void ReleaseCharacter()
    {
        _dummyCollider.enabled = false;

        characterController.ReleaseCharacter(_body.velocity.magnitude / 2);
        if (currentBikeState == BikeControlStates.CanTapForBoostState)
        {
            currentBikeState = BikeControlStates.ReleaseCharacterFromBike;
        }

        _body.velocity = Vector3.zero;
        _body.drag = 1;
        _body.mass = 10000000;
        characterController.transform.parent = null;

        if (CameraManager.Instance) CameraManager.Instance.target = characterController.transform.GetChild(2);
        CameraManager.Instance.offset = Vector3.zero;
    }

    private void OnDrawGizmosSelected()
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