using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointinstant : MonoBehaviour
{
    public GameObject g;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(g, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
