using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;



public class LimitMovementArea : MonoBehaviour {

    private Transform transform;
    //public float xOffset = 20;
    //public float yOffset = 400;
    //public float zOffset = -20;
    private const int CAP = 20; // the space between the border and hero

    [SerializeField]
    public int BfRendererBoundsMinX; //{ get; set; }
    [SerializeField]
    public int BfRendererBoundsMaxX; //{ get; set; }
    [SerializeField]
    public int BfRendererBoundsMinZ; //{ get; set; }
    [SerializeField]
    public int BfRendererBoundsMaxZ; //{ get; set; }
    [SerializeField]
    public int BfRendererBoundsMinY; //{ get; set; }
    [SerializeField]
    public int BfRendererBoundsMaxY; //{ get; set; }

    

    void Start()
    {
        //transform = gameObject.GetComponentInChildren<Transform>();
        transform = gameObject.transform.Find("Cha_Knight"); //Korjaa yleiseksi myöhemmin
        //UpdateWalkingBorders();
    }

    public void UpdateWalkingBorders()
    {
        GameObject bf = GameObject.Find("Battlefield");
        if (bf == null)
        {
            Debug.Log("Error, could not find target battlefield game object!");
        }
        else
        {
            Renderer bfPlaneRenderer = bf.GetComponentInChildren(typeof(Renderer)) as Renderer;
            if (bfPlaneRenderer != null)
            {
                BfRendererBoundsMinX = Mathf.RoundToInt(bfPlaneRenderer.bounds.min.x);
                BfRendererBoundsMaxX = Mathf.RoundToInt(bfPlaneRenderer.bounds.max.x);
                BfRendererBoundsMinY = Mathf.RoundToInt(bfPlaneRenderer.bounds.min.y);
                BfRendererBoundsMaxY = Mathf.RoundToInt(bfPlaneRenderer.bounds.max.y);
                BfRendererBoundsMinZ = Mathf.RoundToInt(bfPlaneRenderer.bounds.min.z);
                BfRendererBoundsMaxZ = Mathf.RoundToInt(bfPlaneRenderer.bounds.max.z);
            }
        }

        Debug.Assert(BfRendererBoundsMaxX != 0 || BfRendererBoundsMaxZ != 0, "Error!!! Update walking area failed big time!. Either BfRendererBoundsMaxX or BfRendererBoundsMaxZ is 0");

        Debug.Log("Battlefield length :" + BfRendererBoundsMaxX);
    }

    void LateUpdate()
    {
        LimitPlayerMovementWithClamp();
    }

    private void LimitPlayerMovementWithClamp()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(transform.position.x, BfRendererBoundsMinX + CAP, BfRendererBoundsMaxX + 10 + CAP);
        pos.y = Mathf.Clamp(transform.position.y, 0.2f, 500);
        pos.z = Mathf.Clamp(transform.position.z, BfRendererBoundsMinZ + CAP, BfRendererBoundsMaxZ + CAP);
        transform.position = pos;
    }

}
