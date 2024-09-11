using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerctr : MonoBehaviour
{
    public int toycount;
    public bool win;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(toycount>=9 && !win)
        {
            win = true;
            StartCoroutine(wingm());
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="toy")
        {
            SoundManager.instance.Play("toy");
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            Destroy(other.gameObject);
            toycount++;
            FindObjectOfType<lvlmanager>().nextoy();

        }
    }

    IEnumerator wingm()
    {
        SoundManager.instance.Play("win");
        yield return new WaitForSeconds(3.5f);
        UiManager.instance.winpanel.SetActive(true);
    }
}
