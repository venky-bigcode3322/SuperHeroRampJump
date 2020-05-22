using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get;private set; }

    [SerializeField] Slider fuelBar;

    public GameObject[] superHeroCharacters;

    public GameObject[] bikes;

    [SerializeField] Transform spawnPoint;

    private float _initialBarPercentage;

    [SerializeField] private Transform StartPoint;

    private void Awake()
    {
        instance = this;
        _initialBarPercentage = GlobalVariables.FuelPercentage;
    }

    private void Start()
    {
        InstantiateBike();
    }

    public void CheckFuelHud()
    {
        fuelBar.value = GlobalVariables.FuelPercentage / _initialBarPercentage;
    }

    private void InstantiateBike()
    {
        var obj = Instantiate(bikes[0]) as GameObject;
        obj.transform.position = spawnPoint.position;
        if (CameraManager.Instance) CameraManager.Instance.target = obj.transform;
    }

    public CharacterController InstantiateSuperHero(Transform initTransform)
    {
        var obj = Instantiate(superHeroCharacters[GlobalVariables.selectedCharacter], initTransform.parent) as GameObject;

        obj.transform.localPosition = initTransform.localPosition;

        return obj.GetComponent<CharacterController>();
    }
}