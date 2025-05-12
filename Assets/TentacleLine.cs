using UnityEngine;
using UnityEngine.UIElements;

public class TentacleLine : MonoBehaviour
{
    public GameManager Manager;
    public Player player;
    public LineRenderer lineRenderer;
    int Length = 5;
    public Vector3[] segmentPoses;
    public Vector3[] segmentV;

    public Transform targetDir;

    float targetDist = 0.5f;

    float smoothVar = 0.1f;

    Vector3 inbetweenfront;
    Vector3 inbetweenMiddle;
    Vector3 inbetweenback;

    void Awake()
    {
        
        Manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();//find gamemanager
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();//find Player     
        lineRenderer.positionCount = Length;
        segmentPoses = new Vector3[Length];
        segmentV = new Vector3[Length];

        segmentPoses[0] = player.transform.position;
        segmentPoses[1] = player.transform.position;
        segmentPoses[2] = player.transform.position;
        segmentPoses[3] = player.transform.position;
        segmentPoses[4] = player.transform.position;
        lineRenderer.SetPositions(segmentPoses);
    
    }

    void Update()
    {
        //lineRenderer.SetPosition(0, player.transform.position);
        //lineRenderer.SetPosition(1, transform.position);

        inbetweenMiddle = segmentPoses[0] + segmentPoses[4];
        inbetweenMiddle = inbetweenMiddle/2;

        inbetweenfront = segmentPoses[0] + segmentPoses[2];
        inbetweenfront = inbetweenfront/2;

        inbetweenback = segmentPoses[4] + segmentPoses[2];
        inbetweenback = inbetweenback/2;


        
        segmentPoses[0] = player.transform.position;

        segmentPoses[1] =  Vector3.SmoothDamp(segmentPoses[1], inbetweenfront, ref segmentV[1], smoothVar);
        
        segmentPoses[2] =  Vector3.SmoothDamp(segmentPoses[2], inbetweenMiddle, ref segmentV[2], smoothVar);

        segmentPoses[3] =  Vector3.SmoothDamp(segmentPoses[3], inbetweenback, ref segmentV[3], smoothVar);

        segmentPoses[4] = transform.position;

        //for (int i = 1; i < segmentPoses.Length; i++)
        //{
            //segmentPoses[i] = Vector3.SmoothDamp(segmentPoses[i], segmentPoses[0]/2 + segmentPoses[Length - 1]/2, ref segmentV[i],smoothVar);
       // }

        lineRenderer.SetPositions(segmentPoses);
        
    }
}
