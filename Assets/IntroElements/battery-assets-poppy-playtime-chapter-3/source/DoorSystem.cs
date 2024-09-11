using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 


public class DoorSystem : MonoBehaviour
{   Button btnforOpen;
    [SerializeField] private GameObject openbtn;
    [SerializeField] AudioClip openAudio;
    bool canControl;
    Animator animator;
    bool alreadyOpended;

    public HandMovement handMovement;

    /// </summary>
    // Start is called before the first frame update
    void Start()
    {
  //      handMovement = GetComponentInChildren<HandMovement>();
        alreadyOpended = false;
           animator = GetComponent<Animator>();
           btnforOpen = openbtn.GetComponent<Button>();
        btnforOpen.onClick.AddListener(TaskOpenOnClick);
        openbtn.SetActive(false);

    }
    // Update is called once per frame
    void Update()
    {
        if (!alreadyOpended && canControl)
        {

            openbtn.SetActive(true);

        }
    }

    void TaskOpenOnClick()
    {
       // if (!alreadyOpended && canControl) {
            openbtn.SetActive(false);
            alreadyOpended = true;

            // handscript.SetActive(true);
            handMovement.MoveHandToTarget();
        StartCoroutine(delayforhand());

       // }
        Debug.Log("You have clicked the button!");
    }

    IEnumerator delayforhand()
    {
        yield return new WaitForSecondsRealtime(4f);
        animator.Play("OpenDoor");

        AudioSource audio = GetComponent<AudioSource>();
         

        audio.clip = openAudio;
        audio.Play(); 
    }


    void OnTriggerStay(Collider col)
    {

        if(col.gameObject.tag == "Player")
        {

            canControl = true;
        }

    }
    void OnTriggerExit(Collider col)
    {

        if (col.gameObject.tag == "Player")
        {
            openbtn.SetActive(false);

            canControl = false;
        }

    }
}
