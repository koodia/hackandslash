using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CurtainController //: MonoBehaviour {
{

   


    public static bool IsCurtainUp = true;//{ get; set; }
                                          //IsCurtainUp = true;



    //public static IEnumerator StartGameCurtain()
    //{
    //    while (CurtainController.IsCurtainUp == true)
    //    {
    //        yield return null;
    //    }
    //}


    /// <summary>
    /// Fades out to black
    /// </summary>
    public static IEnumerator LowerCurtainAndCallSceneSwitch(GC.MyDelegate callbackFun)
    {
        float fadeTime = GameObject.Find("Fader").GetComponent<SceneSwitchFading>().FadeBlack(1);
        yield return new WaitForSeconds(1.5f);
        IsCurtainUp = false;
        callbackFun();

    }

    public static IEnumerator RiseCurtain(float waitTime)
    {
        yield return new WaitForSeconds(waitTime); //wait some time until rise the curtain

        float fadeTime = GameObject.Find("Fader").GetComponent<SceneSwitchFading>().FadeOut(-1);
        yield return new WaitForSeconds(fadeTime);
        IsCurtainUp = true;
    }
}
