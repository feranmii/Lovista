using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using EventCallbacks;
using UnityEngine;

public class CameraFunctions : MonoBehaviour
{	
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    [SerializeField] private CinemachineBasicMultiChannelPerlin multiChannelPerlin;
    //Shake length should not be higher than 10
    [Range(0,10)]
    public float shakeLength = 10;

    // Use this for initialization
    void Start () {
        virtualCamera = GetComponent<CinemachineVirtualCamera> ();
        multiChannelPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin> ();
        if(virtualCamera.Follow == null)
            virtualCamera.Follow = PlayerManager.Instance.transform;
        
        EventManager.Instance.RegisterListener<OnPlayerHurt>(PlayerHitShake);
        
    }
		
    // Update is called once per frame
    void Update () {
        multiChannelPerlin.m_FrequencyGain += (0 - multiChannelPerlin.m_FrequencyGain) * Time.deltaTime * (10-shakeLength);
    }

    public void Shake(float shake, float length){
        shakeLength = length;
        multiChannelPerlin.m_FrequencyGain = shake;
    }

    public void PlayerHitShake(OnPlayerHurt pd)
    {
        Shake(300, 1);
    }
}
