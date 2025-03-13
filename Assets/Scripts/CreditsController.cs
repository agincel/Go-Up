using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{
    float sceneTime = 0;
    GameControls gameControl;
	private void Start()
	{
		gameControl = new GameControls();
		gameControl.Gameplay.Enable();
	}

	// Update is called once per frame
	void Update () {
        sceneTime += Time.deltaTime;

        if (sceneTime > 0.1f && gameControl.Gameplay.Jump.WasPressedThisFrame())
        {
            SceneManager.LoadScene("Start");
        }
    }
}
