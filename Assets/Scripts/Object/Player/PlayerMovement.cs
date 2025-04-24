using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IPlayerMovement, IPlayerAnim
{
    public float m_speed;
    private Rigidbody m_rigid;

    private Animator m_anim;
    public Animator Anim => m_anim;

    private void Awake()
    {
        m_rigid = GetComponent<Rigidbody>();
        m_anim = GetComponent<Animator>();
    }

    public void Move(Vector2 inputVec)
    {
        Vector3 moveVec = new Vector3(inputVec.x, 0, inputVec.y);
        transform.position += moveVec * m_speed * Time.deltaTime;
    }

    public void MoveTo(Vector3 des)
    {
        // TODO
    }

    public void PlayAnim(Vector2 inputVec)
    {
        m_anim.SetFloat("Forward", inputVec.y);
        m_anim.SetFloat("Turn", inputVec.x);
    }
}
