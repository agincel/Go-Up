using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitController : MonoBehaviour {

    protected TimerController tc;

    GameControls gameControl;
    float timeSinceRoomLoad = 0;

    private void Start()
    {
        gameControl = new GameControls();
        gameControl.Gameplay.Enable();
		GameObject[] timers = GameObject.FindGameObjectsWithTag("Timer");
		foreach (GameObject g in timers)
		{
		    GameObject.Destroy(g);
		}
	}

    // Update is called once per frame
    void Update () {
        timeSinceRoomLoad += Time.deltaTime;
        //if (Input.GetButtonDown("Cancel"))
        if (gameControl.Gameplay.Restart.WasPressedThisFrame() && timeSinceRoomLoad > 0.1f)
        {
            Debug.Log("Quit requested");
            Application.Quit();
        }
        
        //if (Input.GetButtonDown("Jump"))
        if (gameControl.Gameplay.Jump.WasPressedThisFrame() && timeSinceRoomLoad > 0.1f)
        {
			Debug.Log("Next level requested");
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            PlayerPrefs.SetFloat("leader1", -1);
            PlayerPrefs.SetFloat("leader2", -1);
            PlayerPrefs.SetFloat("leader3", -1);

            GameObject.Find("Leaderboard").GetComponent<UnityEngine.UI.Text>().text = "CLEARED LEADERBOARD";
        }
    }
}
