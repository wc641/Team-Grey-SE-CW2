using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TextBlink : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Text _blinkText;
    private Color _color;
    private float _timechecker = 0;
    public float BlinkFadeInTime=0.1f;
    public float BlinkStayTime = 0.8f;
    public float BlinkFadeOutTime = 0.2f;

    private bool _hover = false;
    void Start()
    {
        _blinkText = GetComponentInChildren<Text>();
        _color = _blinkText.color;
    }


    void Update()
    {
        _timechecker += Time.deltaTime;
        if (_hover == true)
        {
            _blinkText.color = new Color(255f,229f,0f);
           
        }
        else if (_timechecker < BlinkFadeInTime)
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        _hover = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _hover = false;
    }
}
