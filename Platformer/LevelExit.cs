using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField]  float levelLoadDelay = 1f; // Delay before loading the next scene
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
             StartCoroutine(LoadNextScene());
        }
       
    }
    IEnumerator LoadNextScene()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);// Wait for 1 second before loading the next scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0; // Loop back to the first scene if at the end
        }   
        SceneManager.LoadScene(nextSceneIndex);
    }
    
}
