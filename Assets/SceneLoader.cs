using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string ScenePath;

    public void LoadScene(string ScenePath)
	{
		SceneManager.LoadScene(ScenePath, LoadSceneMode.Additive);
	}
}
