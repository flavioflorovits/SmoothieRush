using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueGame : MonoBehaviour
{

    public void LoadMain()
    {
        Debug.Log("Loaded main");
        SceneManager.LoadScene("Main");
    }


}
