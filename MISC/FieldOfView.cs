using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour {

    public float viewRadius;
    [Range(0, 360)]
    public float viewAngel;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public List<Transform> visibleTargets = new List<Transform>();


    void Start()
    {
        StartCoroutine("FindTargetsWithDelay", 0.3f);
    }

    void Update()
    {
        if (visibleTargets.Count != 0)
        {
            this.gameObject.GetComponent<EnemyAI>().enabled = true;
        }
        else
        {
            this.gameObject.GetComponent<EnemyAI>().enabled = false;
        }
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, dirToTarget);
            if (angle < viewAngel / 2) //Huom nuoli toistaiseksi
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, distanceToTarget, obstacleMask))
                {
                    if (!visibleTargets.Contains(target))
                        visibleTargets.Add(target);
                }
            }
            //else
            //{
            //    //Otherwise remove it from the list
            //    if(visibleTargets.Contains(target))
            //    {
            //        visibleTargets.Remove(target);
            //    }
            //}

        }

        if (targetsInViewRadius.Length == 0)
        {
            visibleTargets.Clear();
        }

    }


    public Vector3 DirFromAngle(float angleInDegress, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegress += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegress * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegress * Mathf.Deg2Rad));
    }
	
}
