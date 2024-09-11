using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        Time.timeScale = 0;
    }
    void OnDisable()
    {
        Time.timeScale = 1;
    }
    public void moveScene(int index)
    {
        SceneManager.LoadScene(index);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
