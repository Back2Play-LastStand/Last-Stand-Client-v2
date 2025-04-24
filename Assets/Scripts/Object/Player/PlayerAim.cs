using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] private Transform m_aimCamera;
    [SerializeField] private float m_minCameraDistance = 1.5f;
    [SerializeField] private float m_maxCameraDistance = 3f;

    public void AimTowardsMouse(Vector2 inputMousePos)
    {
        Ray ray = Camera.main.ScreenPointToRay(inputMousePos);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
        {
            Vector3 dirAimPos = (hitInfo.point - transform.position).normalized;
            float disPlayerToTarget = Vector3.Distance(transform.position, hitInfo.point);
            float disAimCamera = Mathf.Clamp(disPlayerToTarget, m_minCameraDistance, m_maxCameraDistance);
            Vector3 aimCameraPos = transform.position + dirAimPos * disAimCamera;
            m_aimCamera.position = aimCameraPos;
        }
    }
}
