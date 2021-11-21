using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    #region Variables

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
    private Button _input;
    [HideInInspector] public GameObject FTUE;


    [Header("Mathematic")]
    public int score = 0;
    public int total = 0;
    [HideInInspector] public int lvl;

    [Header("Booleans")]
    public bool isFirstTime;
    private bool _startOnce;
    [HideInInspector] public bool start;
    [HideInInspector] public bool success;
    [HideInInspector] public bool isPlayable;
    [HideInInspector]public bool isPressed;

    [Header("Game Objects")]
    [HideInInspector] public GameObject player;
    [HideInInspector]public GameObject closest;
    [HideInInspector] public GameObject cam;
    [HideInInspector] public GameObject ball;

    #endregion


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("More than one instance found!");
            return;
        }

        Instance = this;


        // Getting Necessary objects in the scene
        canvas = GameObject.Find("Canvas");
        player = GameObject.Find("Player");
        ball = GameObject.Find("Ball");
        _level = canvas.transform.GetChild(0).GetComponent<TMP_Text>();
        _successful = canvas.transform.GetChild(1).GetComponent<TMP_Text>();
        _fail = canvas.transform.GetChild(2).GetComponent<TMP_Text>();
        _next = canvas.transform.GetChild(4).GetComponent<Button>();
        _input = canvas.transform.GetChild(5).GetComponent<Button>();
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

    #region Progress Check
    public void ScoreChange(int x) // Score
    {
        score += x;
        _score.text = score.ToString();
    }

    public void LevelCheck() // Checks if level is success or not
    {
        isPlayable = false;
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
        StartCoroutine(TimeStopper());
    }

    private IEnumerator TimeStopper()
    {
        yield return new WaitForSeconds(3);
        Time.timeScale = 0;
    }

    public void StartButton() // Starts the game
    {
        _start.SetActive(false);
        _level.gameObject.SetActive(true);
        isPlayable = true;
        _startOnce = true;
    }

    #endregion

    #region PlayerMechanics

    public void PlayerChange(GameObject x) // Change the player after pass
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

    private void FindClosest() // Decides which player will follow the ball
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

    #endregion
}
