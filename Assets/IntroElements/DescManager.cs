using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DescManager : MonoBehaviour
{
     
    public TextMeshProUGUI desc;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void changeDesc(int index)
    { 
        if (index == 1)
        {
            desc.text = "ARE YOU SURE YOU WANT TO PLAY CHAPTER ONE?";
        }else if (index == 2)
        {

            desc.text = "ARE YOU SURE YOU WANT TO PLAY CHAPTER TWO?";
        }
        else if (index == 3)
        {

            desc.text = "ARE YOU SURE YOU WANT TO PLAY CHAPTER THREE?";
        }
        else if (index == 4)
        {

            desc.text = "ARE YOU SURE YOU WANT TO PLAY CHAPTER FOUR?";
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
