using UnityEngine;
using System.Collections;
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

    [SerializeField] GameObject DustParticle;

    public ObjectPooling DustParticlePool;

    private void Awake()
    {
        instance = this;

        _initialBarPercentage = GlobalVariables.FuelPercentage;

        DustParticlePool = new ObjectPooling();
        DustParticlePool.InitializePool(DustParticle);
    }

    private void Start()
    {
        InstantiateBike();

        GlobalVariables.ResetScoreValues(); 
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

    public IEnumerator LevelComplete()
    {
        yield return new WaitForSeconds(3f);

        if (UiHandler.Instance) UiHandler.Instance.ShowPopup(AllPages.Ingame, AllPages.FinishPage);
    }
}