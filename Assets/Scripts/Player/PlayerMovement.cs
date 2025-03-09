using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IPlayerMovement
{
    [SerializeField]
    private float m_speed;
    private Rigidbody m_rigid;

    private void Awake()
    {
        m_rigid = GetComponent<Rigidbody>();
    }

    public void Move(Vector2 inputVec)
    {
        Vector3 moveVec = inputVec * m_speed * Time.deltaTime;
        transform.position += new Vector3(moveVec.x, 0, moveVec.y);
    }

    public void MoveTo(Vector3 des)
    {
        // TODO
    }

    public void LookAtMouse(Vector2 inputMousePos)
    {
        Ray ray = Camera.main.ScreenPointToRay(inputMousePos);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            Vector3 currentPoint = new Vector3(hitPoint.x, transform.position.y, hitPoint.z);
            transform.LookAt(currentPoint);
        }
    }
}
