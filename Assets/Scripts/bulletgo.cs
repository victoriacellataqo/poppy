using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletgo : MonoBehaviour
{
    public float speed;
    Vector3 startpos;
    float t;
    // Start is called before the first frame update
    void Start()
    {
        t = 0.2f;
        startpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.parent = null;
        if (t>0)
        {
            transform.position = new Vector3(Random.Range(startpos.x - 0.1f, startpos.x + 0.1f), Random.Range(startpos.y - 0.1f, startpos.y + 0.1f), transform.position.z);
        }
        else
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            if (Vector3.Distance(startpos, transform.position) > 80)
            {
                Destroy(gameObject);
            }
        }
        t -= Time.deltaTime;
        
    }
}
