using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class myenemct : MonoBehaviour
{
    private Quaternion originalRotation,originalPlayer;
    NavMeshAgent ag;
    public GameObject ctrui, losepanel;
    Transform t, p;
    public GameObject cameraEnemy, cameraPlayer;
    public bool follo, find, onet;
    Vector3 original,originalTrans;
    bool reviving;  
    // Start is called before the first frame update
    void Start()
    { 
       originalTrans = transform.position;
           reviving = false;
           originalRotation = transform.rotation;
        ag = GetComponent<NavMeshAgent>();
        t = FindObjectOfType<lvlmanager>().transform;
        ag.SetDestination(t.GetChild(Random.Range(0, t.childCount)).position);
        p = GameObject.FindGameObjectWithTag("Player").transform;
        original = p.transform.position;
        originalPlayer = p.transform.rotation;



    }

    // Update is called once per frame
    void Update()
    {
        if (!reviving)
        {
            if (!find)
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
                    find = true;
                    ag.isStopped = true;
                    ag.enabled = false;
                }

            }
            else if (find && !onet)
            {
                onet = true;
                ctrui.SetActive(false);
                transform.LookAt(p.GetChild(2));
                p.LookAt(transform.GetChild(1));


                StartCoroutine(lsgame());
            }
        }
    }

    IEnumerator lsgame()
    {
        cameraEnemy.SetActive(true);
        cameraPlayer.SetActive(false);
        transform.GetChild(0).gameObject.GetComponent<Animator>().Play("scream");
        
        SoundManager.instance.Play("ghawta");
        yield return new WaitForSeconds(3f);
       if(AdsManager.instance!=null) AdsManager.instance.ShowInterstitial();

        
        losepanel.SetActive(true);
    }

    public void revive()
    {
          if (AdsManager.instance != null) AdsManager.instance.ShowRewarded(complete);

        
        void complete(bool complete)
        {

            if (complete)
            {



                StartCoroutine(revivewait());
               
                

            }
        }

    }

    IEnumerator revivewait()
    {
        reviving = true;

      //  p.gameObject.SetActive(false);
        cameraEnemy.SetActive(false); 
        cameraPlayer.SetActive(true);
        transform.rotation = originalRotation;
        gameObject.transform.position = originalTrans;
        transform.GetChild(0).gameObject.GetComponent<Animator>().Play("walk");
    //    p.transform.position = original;
        p.transform.rotation = originalPlayer;
        
        ag.enabled = true;
        ag.isStopped = false;


     //   p.gameObject.SetActive(true);
        losepanel.SetActive(false);

        yield return new WaitForSecondsRealtime(0.5f);
        
        ctrui.SetActive(true); 
        reviving = false;
        yield return new WaitForSecondsRealtime(10f);

        onet = false;
        find = false;
    }


}