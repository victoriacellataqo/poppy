using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemctr : MonoBehaviour
{
    NavMeshAgent ag;
    public GameObject ctrui,losepanel;
    Transform t,p;
    
    public bool follo,find,onet;
    // Start is called before the first frame update
    void Start()
    {
        ag = GetComponent<NavMeshAgent>();
        t = FindObjectOfType<lvlmanager>().transform;
        ag.SetDestination(t.GetChild(Random.Range(0,t.childCount)).position);
        p = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {   
        if(!find)
        {
            if (ag.remainingDistance < 0.2f && !follo)
            {
                ag.SetDestination(t.GetChild(Random.Range(0, t.childCount)).position);
            }
            //ag.SetDestination(t.GetChild(3).position);


            if (Vector3.Distance(transform.position, p.position) < 45)
            {
                follo = true;
                ag.SetDestination(p.position);
            }

            if (Vector3.Distance(transform.position, p.position) > 65 && follo)
            {
                follo = false;
                ag.SetDestination(t.GetChild(Random.Range(0, t.childCount)).position);
            }

            if (Vector3.Distance(transform.position, p.position) <= 7f)
            {
                find=true;
                ag.isStopped=true;
                ag.enabled = false;
            }

        }
        else if(find && !onet)
        {
            onet = true;
            ctrui.SetActive(false);
            transform.LookAt(p.GetChild(2));
            p.LookAt(transform.GetChild(1));
            transform.GetChild(0).gameObject.GetComponent<Animator>().Play("scream");
            SoundManager.instance.Play("ghawta");
            StartCoroutine(lsgame());
        }
        
    }

    IEnumerator lsgame()
    {
        yield return new WaitForSeconds(3);
        losepanel.SetActive(true);
    }
}
