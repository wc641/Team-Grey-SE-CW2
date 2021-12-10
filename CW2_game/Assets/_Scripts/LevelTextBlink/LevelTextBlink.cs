using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTextBlink : MonoBehaviour
{
    private Text _blinkText;
    private Color _color;
    private float _timechecker = 0;
    public float BlinkFadeInTime = 0.5f;
    public float BlinkStayTime = 0.8f;
    public float BlinkFadeOutTime = 0.7f;

    void Start()
    {
        _blinkText = GetComponent<Text>();
        _color = _blinkText.color;
    }

    void Update()
    {
        _timechecker += Time.deltaTime;
        if (_timechecker < BlinkFadeInTime)
        {
            _blinkText.color = new Color(_color.r, _color.g, _color.b, _timechecker / BlinkFadeInTime);
        }
        else if (_timechecker < BlinkFadeInTime + BlinkStayTime)
        {
            _blinkText.color = new Color(_color.r, _color.g, _color.b, 1);
        }
        else if (_timechecker < BlinkFadeInTime + BlinkStayTime + BlinkFadeOutTime)
        {
            _blinkText.color = new Color(_color.r, _color.g, _color.b, 1 - (_timechecker - (BlinkFadeInTime + BlinkStayTime)) / BlinkFadeOutTime);
        }
        else
        {
            _timechecker = 0;
          
        }
    }
}
