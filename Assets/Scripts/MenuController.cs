using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	public void StartGame()
    {
        SceneManager.LoadScene("Scenes/GameplayScene");
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("Scenes/GameplayScene"));
    }
}
