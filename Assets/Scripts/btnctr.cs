
using UnityEngine;
using UnityEngine.EventSystems;

public class btnctr : MonoBehaviour/*, IPointerDownHandler,
    IPointerUpHandler*/
{
    public float t;
    public bool pressed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t -= Time.deltaTime;
        if(t<=0 && pressed)
        {
            t = 0.5f;
            SoundManager.instance.Play("step");
        }
    }




    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    pressed = true;
    //}

    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    pressed = false;
    //}
}
