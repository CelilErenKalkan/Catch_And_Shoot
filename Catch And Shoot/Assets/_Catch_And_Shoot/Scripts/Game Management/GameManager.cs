using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;


    [Header("Button&Texts")]
    public GameObject canvas;
    public GameObject confetties;
    public GameObject firstTimeFinish;
    private GameObject _start;
    private TMP_Text _level;
    private TMP_Text _successful;
    private TMP_Text _fail;
    private Button _next;
    [HideInInspector] public GameObject FTUE;


    [Header("Mathematic")]
    public int score = 0; //Penguins that player got.
    public int total = 0; //Total amount of penguins.
    [HideInInspector] public int lvl;

    [Header("Booleans")]
    public bool isFirstTime;
    [HideInInspector] public bool start;
    [HideInInspector] public bool success;
    public bool isPlayable;
    public bool isPressed;
    private bool _startOnce;


    public Vector3 offset = new Vector3(0.0f, 0.2f, -5.0f);
    public Vector3 offset2 = new Vector3(-1.0f, 5.0f, -8.0f);
    public Vector3 offset3 = new Vector3(0.0f, 10.0f, 0.0f);


    private Button _input;
    [HideInInspector] public GameObject player;
    [HideInInspector] public GameObject cam;




    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("More than one instance found!");
            return;
        }

        Instance = this;

        canvas = GameObject.Find("Canvas");
        player = GameObject.Find("Player");
        _level = canvas.transform.GetChild(0).GetComponent<TMP_Text>();
        _successful = canvas.transform.GetChild(1).GetComponent<TMP_Text>();
        _fail = canvas.transform.GetChild(2).GetComponent<TMP_Text>();
        _input = canvas.transform.GetChild(3).GetComponent<Button>();
        _next = canvas.transform.GetChild(4).GetComponent<Button>();
        FTUE = canvas.transform.GetChild(5).gameObject;
        _start = canvas.transform.GetChild(7).gameObject;
        cam = GameObject.Find("Follower Camera");
    }

    // Start is called before the first frame update
    void Start()
    {
        lvl = PlayerPrefs.GetInt("Level", 1);
        _level.text = "LEVEL " + lvl;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_startOnce)
            StartButton();
    }

    public void LevelCheck()
    {
        canvas.transform.GetChild(5).gameObject.SetActive(false);
        if (success)
        {
            if (!success) return;
            start = false;
            confetties.SetActive(true);
            _successful.gameObject.SetActive(true);
            _next.gameObject.SetActive(true);
        }
        else if (!success)
        {
            if (success) return;
            start = false;
            isPlayable = false;
            _fail.gameObject.SetActive(true);
            _next.gameObject.SetActive(true);
            //StartCoroutine(StopTime());
        }
    }

    public void StartButton()
    {
        _start.SetActive(false);
        _level.gameObject.SetActive(true);
        if (GameManager.Instance.lvl == 1)
        {
            StartCoroutine(FirstTime(FTUE));
        }
        isPlayable = true;
        _startOnce = true;
    }

    public IEnumerator FirstTime(GameObject x)
    {
        x.gameObject.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        x.gameObject.SetActive(false);
    }

    public IEnumerator Timer()
    {
        start = false;
        yield return new WaitForSeconds(2.0f);
        start = true;
    }
}
