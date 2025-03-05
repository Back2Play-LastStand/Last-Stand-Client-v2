using System.Collections;
using System.Collections.Generic;
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

    public void LookAtMouse(Vector3 inputMousePos)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(inputMousePos);
        Vector3 direction = mousePos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
