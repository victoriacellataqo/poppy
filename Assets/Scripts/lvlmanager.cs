using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lvlmanager : MonoBehaviour
{
    playerctr plr;
    public Sprite[] sp;
    public string[] tx;
    public Image img;
    public Text count;
    public Text objerctiftxt;
    // Start is called before the first frame update
    void Start()
    {
        plr = FindObjectOfType<playerctr>();
        nextoy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextoy()
    {
        if(plr.toycount<10)
        {
            transform.GetChild(plr.toycount).gameObject.SetActive(true);
            img.sprite = sp[plr.toycount];
            objerctiftxt.text = (plr.toycount+1).ToString()+" Find the " +tx[plr.toycount];
            count.text = plr.toycount.ToString() + "/10";
        }
        
    }
}
