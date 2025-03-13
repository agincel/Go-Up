using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenChanger : MonoBehaviour {

    protected TimerController tc;
    protected GameObject timerObj;
    float sceneTime = 0f;
    
    GameControls gameControl;

    void Start()
    {
		gameControl = new GameControls();
		gameControl.Gameplay.Enable();
		timerObj = GameObject.FindGameObjectWithTag("Timer");
        if (timerObj != null)
        {
			tc = timerObj.transform.GetComponent<TimerController>();
			tc.Finish();

			float t = tc.finishedTime;

			if (PlayerPrefs.GetInt("canSetLeaderboard") > 0)
			{
				PlayerPrefs.SetInt("canSetLeaderboard", 0);
				List<float> times = new List<float>
				{
					PlayerPrefs.GetFloat("leader1", -1),
					PlayerPrefs.GetFloat("leader2", -1),
					PlayerPrefs.GetFloat("leader3", -1)
				};

				bool hasWritten = false;


				if (!hasWritten && (times[0] == -1 || t < times[0]))
				{
					times.Insert(0, t);
					Debug.Log("Overwriting 0");
					hasWritten = true; //shouldn't have to do this but...Unity?
				}
				else if (!hasWritten && (times[1] == -1 || t < times[1]))
				{
					times.Insert(1, t);
					Debug.Log("Overwriting 1");
					hasWritten = true;
				}
				else if (!hasWritten && (times[2] == -1 || t < times[2]))
				{
					times.Insert(2, t);
					Debug.Log("Overwriting 2");
					hasWritten = true;
				}

				PlayerPrefs.SetFloat("leader1", times[0]);
				PlayerPrefs.SetFloat("leader2", times[1]);
				PlayerPrefs.SetFloat("leader3", times[2]);
			}
		}
    }




    // Update is called once per frame
    void Update () {
        sceneTime += Time.deltaTime;
        if (sceneTime > 0.1f && gameControl.Gameplay.Jump.WasPressedThisFrame())
        {
			SceneManager.LoadScene("Credits");
		}
    }
}
