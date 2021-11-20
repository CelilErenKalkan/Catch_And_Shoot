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
    private TMP_Text _score;
    [HideInInspector] public GameObject FTUE;


    [Header("Mathematic")]
    public int score = 0;
    public int total = 0;
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
    public GameObject closest;
    [HideInInspector] public GameObject cam;
    [HideInInspector] public GameObject ball;




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
        ball = GameObject.Find("Ball");
        _level = canvas.transform.GetChild(0).GetComponent<TMP_Text>();
        _successful = canvas.transform.GetChild(1).GetComponent<TMP_Text>();
        _fail = canvas.transform.GetChild(2).GetComponent<TMP_Text>();
        _next = canvas.transform.GetChild(3).GetComponent<Button>();
        _input = canvas.transform.GetChild(4).GetComponent<Button>();
        _start = canvas.transform.GetChild(6).gameObject;
        _score = canvas.transform.GetChild(7).transform.GetChild(0).GetComponent<TMP_Text>();
        cam = GameObject.Find("Follower Camera");
    }

    // Start is called before the first frame update
    void Start()
    {
        lvl = PlayerPrefs.GetInt("Level", 1);
        _level.text = "LEVEL " + lvl;
        _score.text = score.ToString();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_startOnce)
            StartButton();

        if (closest == null)
            FindClosest();
    }

    public void ScoreChange()
    {
        score++;
        _score.text = score.ToString();
    }

    public void LevelCheck()
    {
        if (success)
        {
            if (!success) return;
            start = false;
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
        }
    }

    public void StartButton()
    {
        _start.SetActive(false);
        _level.gameObject.SetActive(true);
        isPlayable = true;
        _startOnce = true;
    }

    public void PlayerChange(GameObject x)
    {
        player = x;
        player.GetComponent<PlayerNPC>().enabled = false;
        player.tag = "Untagged";
        closest = null;
        var to = new Vector3(0, 0, 0);
        player.transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, to, Time.deltaTime);
        isPlayable = true;
        player.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void Throw ()
    {
        isPressed = false;
        isPlayable = false;
        player.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Throw");
        player.transform.GetChild(0).GetComponent<Animator>().SetBool("isRunning", false);
        player.GetComponent<PlayerMovement>().enabled = false;
    }

    private void FindClosest()
    {
        GameObject[] players;
        players = GameObject.FindGameObjectsWithTag("Member");
        float distance = Mathf.Infinity;

        foreach (GameObject obj in players)
        {
            Vector3 diff = obj.transform.position - player.transform.position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance && ball.transform.position.z < obj.transform.position.z)
            {
                closest = obj;
                distance = curDistance;
            }
        }

        if(closest != null)
            closest.GetComponent<PlayerNPC>().isClosest = true;
    }
}
