using Unity.Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour
{
    public CinemachineCamera virtualCamera;
    public Vector3 offset = new Vector3(0, 10, 0);

    void Start()
    {
        if (virtualCamera == null)
            virtualCamera = GetComponent<CinemachineCamera>();

        GameObject player = Managers.Object.MyPlayer.gameObject;

        if (player != null)
        {
            virtualCamera.Follow = player.transform;
        }
    }
}
