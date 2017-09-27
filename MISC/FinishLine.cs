using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (GC.GetCntr().sceneSwitchAllowed == false)
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            LowerCurtainAndCallSceneSwitch();
        }
    }

    public void LowerCurtainAndCallSceneSwitch()
    {
        Debug.Log("Finish line crossed. Changing the scene");
        GC.GetCntr().FreezeAllObjects(true);
        GC.GetCntr().StartCoroutine(CurtainController.LowerCurtainAndCallSceneSwitch(GC.GetCntr().Switch));
    }

}
