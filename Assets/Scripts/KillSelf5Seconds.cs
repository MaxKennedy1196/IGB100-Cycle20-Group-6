using UnityEngine;

public class KillSelf5Seconds : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, 5f);//Kill yourself in 5 seconds
    }
}
