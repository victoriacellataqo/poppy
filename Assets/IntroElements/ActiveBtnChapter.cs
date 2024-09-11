using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBtnChapter : MonoBehaviour
{
    public Behaviour behaviour;
    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        behaviour.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(panel.activeSelf)
        {
            behaviour.enabled = true;

        }else behaviour.enabled = false;

    }
}
