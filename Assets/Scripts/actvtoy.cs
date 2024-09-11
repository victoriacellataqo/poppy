using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class actvtoy : MonoBehaviour
{
    playerctr plr;
    // Start is called before the first frame update
    void Start()
    {
        plr = FindObjectOfType<playerctr>();
        transform.GetChild(plr.toycount).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
