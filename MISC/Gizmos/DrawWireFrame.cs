using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWireFrame : MonoBehaviour {

    //Could not get to work:
    //void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireCube(gameObject.transform.position, gameObject.transform.localScale);
    //}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(gameObject.transform.position, gameObject.transform.lossyScale);
    }
}
