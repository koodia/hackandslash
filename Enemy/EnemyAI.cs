using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
    public Transform target;
    public int moveSpeed;
    public int rotationSpeed;
    public float noticeDistance;
    public bool moveAway;



   


    [SerializeField]
    public const int spaceBetween = 5; //Aseta nollaksi tjs. jos vihun massa isompi tai vihun luonne on puskea

	private Transform myTransform;

    public bool isTimeFreezed;
    private float timeToChangeDirection;
    Rigidbody rigidbody;

    void Awake()
	{
        myTransform = transform;

    }

	// Use this for initialization
	void Start () {
		GameObject go = GameObject.Find("Cha_Knight");
        target = go.transform;//go.GetComponentInChildren<Transform>();
                              //maxDistance = 1.5f;

      Rigidbody  rigidbody = myTransform.GetComponent<Rigidbody>();

        //noticeDistance = gameObject.GetComponent<FieldOfView>().viewRadius;
        //Debug.Assert(noticeDistance == 0, "maxDistance is 0?, you sure this is right");
    }

    void OnEnable()
    {
       // isTimeFreezed = gameObject.GetComponent<BaseObject>().timeFreeze;
       // if(baseObjectScript != null)
       // baseObjectScript.timeFreeze = false;
    }

    //public void UnTimeFreeze()
    //{
    //   var testi = gameObject.GetComponent<Creature>();
    //    isTimeFreezed = !testi.timeFreeze;
    //}



    public enum CreatureState {Chase, Attack, Search, Notice, Idle, Natural, Patrol }
    public CreatureState currentState;

    //https://www.youtube.com/watch?v=Is9C4i4XyXk //combotus
    //https://www.youtube.com/watch?v=evKIf6edQ_g&index=24&list=PLX2vGYjWbI0QGyfO8PKY1pC8xcRb0X-nP
    private void Update()
    {
        //TODO:
        //switch (currentState)
        //{
        //    case CreatureState.Chase:
        //        Debug.Log("Chasing!");
        //        break;
        //    case CreatureState.Attack:
        //        Debug.Log("Attacking");
        //        break;
        //    case CreatureState.Search:
        //        Debug.Log("Searching");
        //        break;
        //    case CreatureState.Notice:
        //        Debug.Log("Noticing"); //Cautious?
        //        break;
        //    case CreatureState.Idle:
        //        Debug.Log("Idling");
        //        break;
        //    case CreatureState.Natural:
        //        Debug.Log("Natural movement");
        //        break;
        //    case CreatureState.Patrol:
        //    Debug.Log("Patroling");
        //    break;

        //}


    }





    /* TODO!:
    VIHUN tilat:
    Hunt
    Notice
    Idle
    */



    // Update is called once per frame
    void FixedUpdate ()
    {

        if(gameObject.transform.position.y < -1)
        {
            CodeTester.AlertWhenObjectFallingThroughBattlefield(gameObject);
        }


            if (isTimeFreezed == false)
            {
              //  Debug.DrawLine(target.position, myTransform.position, Color.yellow);

                //Look at target
                Vector3 test = target.position - myTransform.position;
                test.y = 0.0f;
                myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(test), rotationSpeed * Time.deltaTime);
                // tempRotation.SetLookRotation()
                // myTransform.rotation

                if (Vector3.Distance(target.position, myTransform.position) < noticeDistance)
                {
                    ////My towards target
                    if (moveAway)
                    {
                        myTransform.position += -myTransform.forward * moveSpeed * Time.deltaTime;
                    }
                    else
                    {
                        if (Vector3.Distance(target.position, myTransform.position) >= myTransform.GetComponent<Renderer>().bounds.extents.x + spaceBetween)
                        {
                            Vector3 noHeight = myTransform.forward * moveSpeed * Time.deltaTime;
                            noHeight.y = 0.0f; //prevent y movement
                            myTransform.position += noHeight;
                        }
                    }
                }
                else //Move randomly
                {
                //TOOD: Distinct creature, AI
                //MoveRandomPlace();

                }
            }
	    }

    private void MoveRandomPlace()
    {
        timeToChangeDirection -= Time.deltaTime;

        if (timeToChangeDirection <= 0)
        {
            ChangeDirection();
        }

        rigidbody.velocity = myTransform.up * 2;

    }

    private void ChangeDirection()
    {
        float angle = Random.Range(0f, 360f);
        Quaternion quat = Quaternion.AngleAxis(angle, Vector3.forward);
        Vector3 newUp = quat * Vector3.up;
        newUp.z = 0;
        newUp.Normalize();
        myTransform.up = newUp;
        timeToChangeDirection = 4.0f;
    }
}
