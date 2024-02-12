using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Loader : MonoBehaviour
{
    [SerializeField] private string loadingScreen = "Loading_Screen";

    public void LoadSceneLoadingScreen() {
        SceneManager.LoadScene(loadingScreen);
    }
}
