using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
   void Awake()
    {
        int numScenePersists = FindObjectsOfType<ScenePersist>().Length;
        if (numScenePersists > 1)
        {
            Destroy(gameObject); // Ensure only one GameSession exists
        }
        else
        {
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
    }
}
