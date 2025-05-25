using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;

//Script used to handle game elements and player upgrades
public class GameManager : MonoBehaviour
{
    //Singleton Setup
    public static GameManager instance = null;

    public float volumeLevel;

    //public GameObject tentacleObject;

    [HideInInspector] public List<GameObject> enemyList = new List<GameObject>();
    public Player player;
    public GameObject playerCamera;

    public GameObject expDrop;
    public GameObject hpDrop;
    public GameObject foodDrop;

    public List<AudioClip> enemyGoreSounds;
    public List<GameObject> goreList;
    public GameObject dmgEffect;

    [Header("Enemies")]

    public GameObject farmerPrefab;
    public GameObject blacksmithPrefab;
    public GameObject clericPrefab;

    [Header("Background Sprites")]

    public GameObject skullPrefab;
    public GameObject spinePrefab;
    public GameObject treePrefab;

    [Header("UI")]
    public PauseMenu pauseMenu;
    public bool upgradeMenuOpen = false;

    [Header("Screen Fading")]
    public SceneFade screenFade;
    public AnimationCurve startCurve;
    public AnimationCurve endCurve;

    int skullQuantity = 75;
    int spineQuantity = 75;
    int treeQuantity = 75;

    Vector2 RandVector = new Vector2();

    
    float SpawnRangeMin = -250f;
    float SpawnRangeMax = 250f;

    public int enemyCount;
    public int farmerCount;
    public int blacksmithCount;
    public int clericCount;


    // Awake Checks - Singleton setup
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
    }


    void Start()
    {
        //Fade in the game scene
        screenFade.fadeCurve = startCurve;
        screenFade.ActivateFade();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();//Get reference to Player

        for(int i = 0; i< skullQuantity; i++ )
        {
            RandVector = new Vector2(Random.Range(SpawnRangeMin,SpawnRangeMax),Random.Range(SpawnRangeMin,SpawnRangeMax));
            Instantiate(skullPrefab,RandVector,Quaternion.identity);
        }

        for(int i = 0; i< spineQuantity; i++ )
        {
            RandVector = new Vector2(Random.Range(SpawnRangeMin,SpawnRangeMax),Random.Range(SpawnRangeMin,SpawnRangeMax));
            Instantiate(spinePrefab,RandVector,Quaternion.identity);
        }

        for (int i = 0; i < treeQuantity; i++)
        {
            RandVector = new Vector2(Random.Range(SpawnRangeMin, SpawnRangeMax), Random.Range(SpawnRangeMin, SpawnRangeMax));
            Instantiate(treePrefab, RandVector, Quaternion.identity);
        }
    }

    public void Update()
    {
        enemyCount = enemyList.Count;

        if (Input.GetKeyDown(KeyCode.Escape) && !upgradeMenuOpen)
        {
            pauseMenu.togglePause();
        }
    }

    public void GameWin()
    {
        StartCoroutine(Win());
    }

    private IEnumerator Win()
    {
        screenFade.fadeCurve = endCurve;
        screenFade.ActivateFade();
        yield return new WaitForSeconds(screenFade.fadeDuration + screenFade.endWait);
        SceneManager.LoadScene(3);
    }
}