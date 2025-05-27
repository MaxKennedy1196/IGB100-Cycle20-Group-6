using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;

public class CameraShakeManager : MonoBehaviour
{
    public static CameraShakeManager instance;

    [SerializeField] private float globalShakeForce = 1f;

    [SerializeField] private float criticalHitShakeForce = 2f;

    [SerializeField] private float shakeCooldown = 2f;

    private float lastShakeTime = -Mathf.Infinity;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void CameraShake(CinemachineImpulseSource impulseSource)
    {
        if (Time.time >= lastShakeTime + shakeCooldown)
        {
            impulseSource.GenerateImpulseWithForce(globalShakeForce);
            lastShakeTime = Time.time;
        }
        
    }

    public void CriticalHitCameraShake(CinemachineImpulseSource impulseSource)
    {
        impulseSource.GenerateImpulseWithForce(criticalHitShakeForce);
    }
}
