using UnityEngine;

public class TentacleLine : MonoBehaviour
{
    public GameManager Manager;
    public Player player;
    public LineRenderer lineRenderer;

    void Awake()
    {
        
        Manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();//find gamemanager
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();//find Player     
    }

    void Update()
    {
        lineRenderer.SetPosition(0, player.transform.position);
        lineRenderer.SetPosition(1, transform.position);
    }
}
