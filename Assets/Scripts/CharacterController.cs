using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;

public class CharacterController : MonoBehaviour
{
    [SerializeField] Animator animator;

    private List<Collider> AllBodyColliders = new List<Collider>();
    private List<Rigidbody> AllBodyRigidbody = new List<Rigidbody>();

    private Rigidbody _rigidbody;

    [SerializeField] Transform Hips;

    private Transform StartPoint;

    public bool _CalculateDistance = false;

    private Text DistanceText;

    private AirTimeDetector AirTimeDetector;

    private Transform DistanceBar;

    private TextMesh DistanceBarText;


    private void Awake()
    {
        foreach (var item in GetComponentsInChildren<Rigidbody>())
        {
            AllBodyRigidbody.Add(item);
            item.isKinematic = true;
            item.interpolation = RigidbodyInterpolation.Extrapolate;
        }

        foreach (var item in GetComponentsInChildren<Collider>())
        {
            AllBodyColliders.Add(item);
            item.enabled = false;
        }
         
        _rigidbody = GetComponent<Rigidbody>();

        animator = GetComponent<Animator>();

        Hips.transform.GetChild(2).GetComponent<Collider>().enabled = true;
        Hips.transform.GetChild(2).tag = "Player";

        StartPoint = GameObject.Find("StartPoint").transform;

        AirTimeDetector = GetComponentInChildren<AirTimeDetector>();

        DistanceBar = GameManager.instance.DistanceBar;
        DistanceBarText = DistanceBar.GetComponentInChildren<TextMesh>();
    }

    private void Start() 
    {
        if (IngamePage.Instance) DistanceText = IngamePage.Instance.DistanceText;
    }

    private bool MoveDistanceBar = false;

    public void ReleaseCharacter(float val)
    {
        animator.SetTrigger(Flying);

        var transform1 = transform;
        var rotation = transform1.rotation;
        rotation = Quaternion.LookRotation(Vector3.down - transform1.up);
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;
        ActivateRagdoll();

        rotation = Quaternion.LookRotation(Vector3.right);
        transform.rotation = rotation;

        Debug.LogError("Velocity:: " + val);
        foreach (var item in AllBodyRigidbody)
        {
            item.AddForce(Vector3.right * (item.mass * val), ForceMode.Impulse);
        }

        if (SoundManager.Instance) SoundManager.Instance.PlayScreamingSounds();

        Invoke("EnableVelocityCheck", 2);

        MoveDistanceBar = true;

    }

    void EnableVelocityCheck()
    {
        checkTheMovement = true;
        if (PluginManager.Instance) PluginManager.Instance.RequestInterstitial();
    }

    private bool checkTheMovement = false;

    public bool IsDead = false;

    private Vector3 distanceBarPositon;

    private void Update()
    {
        if (IsDead)
            return;

        if (checkTheMovement)
        {
            if (AllBodyRigidbody[1].velocity.magnitude <= 0.5f)
            {
                checkTheMovement = false;

                var distance = Mathf.RoundToInt(CalculateDistance);

                GlobalVariables.CurrentJumpingDistance = distance;

                if (distance > GlobalVariables.BestScore)
                    GlobalVariables.BestScore = distance;

                GlobalVariables.DistanceReward = distance;

               
                if (CameraManager.Instance) CameraManager.Instance.ActivateTurnCamera();

                if (GameManager.instance) StartCoroutine(GameManager.instance.LevelComplete());

                Debug.LogError("Sorry, He Is No More RIP -_-");



                IsDead = true;
                AirTimeDetector.isDead = IsDead;
            }
        }


        if (_CalculateDistance && Time.frameCount % 2 == 0)
        {
            var distance = Mathf.RoundToInt(CalculateDistance);
            DistanceText.text = distance + "m";
            DistanceBarText.text = distance + " m";
        }

        if (MoveDistanceBar)
        {
            distanceBarPositon = DistanceBar.transform.position;
            distanceBarPositon.x = Hips.transform.position.x;
            DistanceBar.transform.position = distanceBarPositon;
        }

    }

    private float Airtimer = 0;

    private Vector3 _currentPosition;
    private static readonly int DriverPose = Animator.StringToHash("DriverPose");
    private static readonly int DriverIdleIndex = Animator.StringToHash("DriverIdleIndex");
    private static readonly int Flying = Animator.StringToHash("Flying");

    private void FixedUpdate()
    {
        if (BikeController.instance == null)
            return;

        //if (applyforce)
        //{
        //    foreach (var item in AllBodyRigidbody)
        //    {
        //        item.AddForce(((Vector3.right - Vector3.down) / 2) * item.mass, ForceMode.Impulse);
        //    }
        //}
        _currentPosition = Hips.localPosition;
        _currentPosition.x = 0;
        Hips.localPosition = _currentPosition;

        if (BikeController.instance.currentBikeState == BikeController.BikeControlStates.ReleaseCharacterFromBike)
        {
            if(Hips.transform.position.y < 5)
            {
                animator.enabled = false;
            }

            if (!AirTimeDetector.isGrounded && Time.frameCount % 2 == 0)
            {
                Airtimer += Time.deltaTime;
                if (IngamePage.Instance) IngamePage.Instance.AirTimeText.text = System.Math.Round(Airtimer, 2).ToString();
                double airtimeScore = System.Math.Round((Airtimer / 50f * 1000f), 0);
                if (IngamePage.Instance) IngamePage.Instance.AirTimeScoreText.text = airtimeScore.ToString();
                GlobalVariables.AirTime = System.Convert.ToSingle(airtimeScore);
            }
        }

       
    }

    public float CalculateDistance
    {
        get => Mathf.Sqrt((StartPoint.position - Hips.position).sqrMagnitude);
    }

    private void ActivateRagdoll() 
    {
        //_animator.enabled = false;
        foreach (var item in AllBodyRigidbody)
        {
            item.isKinematic = false;
            //item.AddForce(Vector3.right * item.mass * 50, ForceMode.Impulse);
        }

        foreach (var item in AllBodyColliders)
        {
            item.enabled = true;
        }

        _rigidbody.useGravity = true;
    }

    public void SetDriverPose(int index)
    {
        animator.SetInteger(DriverPose, index);
    }

    public void SetDriverIdlePose(int index)
    {
        Debug.LogError("Idle Pose :: " + index);
        animator.SetInteger(DriverIdleIndex, index);
    }
}