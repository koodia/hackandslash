using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : Singleton<FadeManager>
{

    public Image fadeImage;
    private bool isInTransition;
    private float transition;
    public bool isShowing;
    private float duration;

    public override void Awake()
    {
        base.Awake();
    }

    public void Fade(bool showing, float duration)
    {
        isShowing = showing;
        isInTransition = true;
        this.duration = duration;
        transition = (isShowing) ? 0 : 1;
    }

    void Update()
    {
        if (!isInTransition)
        {
            return;
        }

        //Show:
        transition += (isShowing) ? Time.deltaTime * (1 / duration)
        : -Time.deltaTime * (1 / duration); //Hide

        fadeImage.color = Color.Lerp(new Color(1, 1, 1, 0), Color.black, transition);

        if (transition > 1 || transition < 0)
        {
            isInTransition = false;
        }
    }
}
