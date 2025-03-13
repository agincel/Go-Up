using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomController : MonoBehaviour {

    [SerializeField] private int eggCount;

    private GameObject birb;
    private SpawnerController spawner;
    private StatsController birbStats;

    [Header("References")]
    public TMPro.TextMeshProUGUI countText;

    GameControls gameControl;

    void Start () {

        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            try
            {
                spawner = GameObject.Find("BirbSpawner").GetComponent<SpawnerController>();
            }
            catch
            {
                Debug.Log("Spawner not found, will try again in Update()");
            }

            if (spawner)
                birb = spawner.getBirb();
            if (birb)
                birbStats = birb.GetComponent<StatsController>();
            if (birbStats)
                birbStats.setEggMax(eggCount);

            SetEggCounter();
        }

        // Create a temporary reference to the current scene.
        Scene currentScene = SceneManager.GetActiveScene();
        // Retrieve the name of this scene.
        string sceneName = currentScene.name;


        gameControl = new GameControls();
		gameControl.Gameplay.Enable();
	}
	
	void Update () {

        // If we're not in the main menu
        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            // Keep trying to get the ref if you dont have it
            if (!spawner)
                spawner = GameObject.Find("BirbSpawner").GetComponent<SpawnerController>();
            if (!birb)
                birb = spawner.getBirb();
            if (!birbStats)
                birbStats = birb.GetComponent<StatsController>();
            if (birbStats)
                birbStats.setEggMax(eggCount);

            // Restart Input
            //if (Input.GetButtonDown("Restart"))
            if (gameControl.Gameplay.Restart.WasPressedThisFrame())
            {
                Debug.Log("Restarting level");
                restartLevel();
            }
            
            // Go to menu by pressing Esc or by pressing SELECT
            //if (Input.GetButtonDown("Menu"))
            if (gameControl.Gameplay.BackToMenu.WasPressedThisFrame())
            {
                SceneManager.LoadScene("Start");
            }

            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                //SceneManager.LoadScene("EndScreen");
            }
           

            SetEggCounter();
        }

    }

    public void restartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void nextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitRequest()
    {
        Debug.Log("Quit requested");
        Application.Quit();
    }

    void SetEggCounter()
    {
        if (countText && birbStats)
            countText.text = "Eggs Remaining: " + birbStats.getEggCurr();
    }
}
