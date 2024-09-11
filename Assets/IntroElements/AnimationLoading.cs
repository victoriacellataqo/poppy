using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationLoading : MonoBehaviour
{
    public RectTransform _mainicon;
    public float _timeStep;
    public float _oneStepAngle;

        float _startTime;
    // Start is called before the first frame update
    void Start()
    {
        _startTime = Time.time;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - _startTime >= _timeStep)
        {
            Vector3 iconAngle = _mainicon.localEulerAngles;
            iconAngle.z += _oneStepAngle;

            _mainicon.localEulerAngles = iconAngle;

            _startTime = Time.time;


        }
    }
}
