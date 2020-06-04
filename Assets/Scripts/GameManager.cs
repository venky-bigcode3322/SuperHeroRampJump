using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get;private set; }

    [SerializeField] Image fuelBar;

    public GameObject[] superHeroCharacters;

    public GameObject[] bikes;

    [SerializeField] Transform spawnPoint;

    private float _initialBarPercentage;

    [SerializeField] private Transform StartPoint;

    [SerializeField] GameObject DustParticle;

    public ObjectPooling DustParticlePool;

    private GameObject currentBike;

    private void Awake()
    {
        instance = this;

        DustParticlePool = new ObjectPooling();
        DustParticlePool.InitializePool(DustParticle);
    }

    private void Start()
    {
        InstantiateBike();

        GlobalVariables.ResetScoreValues();

        if (PluginManager.Instance) PluginManager.Instance.RequestRewardedVideoAd();
    }

    public void CheckFuelHud()
    {
        fuelBar.fillAmount = GlobalVariables.FuelPercentage / _initialBarPercentage;
    }

    public void SetFuelforGame(float Value)
    {
        GlobalVariables.FuelPercentage = Value;
        Debug.Log("Fuel :: " + Value);
        _initialBarPercentage = GlobalVariables.FuelPercentage;
    }

    private void InstantiateBike()
    {
        var obj = Instantiate(bikes[GlobalVariables.selectedBike]) as GameObject;
        obj.transform.position = spawnPoint.position;
        if (CameraManager.Instance) CameraManager.Instance.target = obj.transform;
        currentBike = obj;
    }

    public void InstantiateBikeAgain()
    {
        if(currentBike != null) Destroy(currentBike);

        var obj = Instantiate(bikes[GlobalVariables.selectedBike]) as GameObject;
        obj.transform.position = spawnPoint.position;
        if (CameraManager.Instance) CameraManager.Instance.target = obj.transform;
        currentBike = obj;
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

        if (GlobalVariables.CurrentJumpingDistance >= (GlobalVariables.GameLevel * 1000))
        {
            if (UiHandler.Instance) UiHandler.Instance.ShowPopup(AllPages.Ingame, AllPages.LevelUpPage);
        }
        else
        {
            if (UiHandler.Instance) UiHandler.Instance.ShowPopup(AllPages.Ingame, AllPages.FinishPage);
        }

        if (SoundManager.Instance) SoundManager.Instance.StartCoroutine(SoundManager.Instance.PlayBG(MusicBG.LevelClearFinishClip, false));
    }
}