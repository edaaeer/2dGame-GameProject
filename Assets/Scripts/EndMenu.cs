using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public void Quit()
    {
        //Time.timeScale = 1;
        //DataManager.Instance.SaveData();
        SceneManager.LoadScene(0);
    }
}
