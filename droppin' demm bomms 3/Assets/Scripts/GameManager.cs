using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    private Spawner spawner;
    public GameObject title;
    private Vector2 screenBounds;
    public GameObject playerPrefab;
    public GameObject player;
    private bool gameStarted = false;
    public GameObject splash;
    public GameObject scoreSystem;
    public Text scoreText;
    public int pointsWorth = 1;
    private int score;

    void Awake()
    {
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        player = playerPrefab;
        scoreText.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        spawner.active = false;
        title.SetActive(true);
        splash.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {



        if(!gameStarted)
        {
            if(Input.anyKeyDown)
            {
                Debug.Log("resetting game!");
                ResetGame();
            }
        } else
        {
            if(player.active == false)
            {
                OnPlayerKilled();
            }
        }
        var nextBomb = GameObject.FindGameObjectsWithTag("Bommmm");

        foreach(GameObject bombObject in nextBomb)
        {
            if(bombObject.transform.position.y < (-screenBounds.y) - 12)
            {
                Destroy(bombObject);
            } else if(bombObject.transform.position.y < (-screenBounds.y) && gameStarted)
            {
                scoreSystem.GetComponent<Score>().AddScore(pointsWorth);
                Destroy(bombObject);
            }
        }
    }

    void ResetGame()
    {
        spawner.active = true;
        title.SetActive(false);
        splash.SetActive(false);
        // player = Instantiate(playerPrefab, new Vector3(0, 0, 0), playerPrefab.transform.rotation);
        // GameObject newPlayer = Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.Euler(0,0,0));
        // player = newPlayer;
        player.SetActive(true);
        gameStarted = true;

        scoreText.enabled = true;
        scoreSystem.GetComponent<Score>().score = 0;
        scoreSystem.GetComponent<Score>().Start();
    }

    void OnPlayerKilled()
    {
        spawner.active = false;
        gameStarted = false;

        splash.SetActive(true);
    }
}