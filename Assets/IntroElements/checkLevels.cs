using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkLevels : MonoBehaviour
{
    public GameObject[] gameObjects;
    // Start is called before the first frame update
    void Start()
    {
        if (AdsManager.instance != null)
        {
            if(AdsManager.instance.getUnlock())
            {
                for (int i = 0; i < gameObjects.Length; i++) gameObjects[i].SetActive(true);

            }
            else
            {

                for (int i = 0; i < gameObjects.Length; i++) gameObjects[i].SetActive(false);
            }

        }
        else
        {

            for (int i = 0; i < gameObjects.Length; i++) gameObjects[i].SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
