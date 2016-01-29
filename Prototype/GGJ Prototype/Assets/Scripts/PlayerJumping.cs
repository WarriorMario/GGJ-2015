using UnityEngine;
using System.Collections;

public class PlayerJumping : MonoBehaviour {

    public float m_StartingYPos;
    public float m_JumpPower;
    public float m_Gravity;
    public bool m_Jumping;

    private bool m_StartFrame;
    private float m_Speed;
   
	
	// Update is called once per frame
	void Update ()
    {
        if(m_Jumping)
        {
            if (m_StartFrame)
            {
                m_Speed = m_JumpPower;
                m_StartFrame = false;
            }
            transform.position += new Vector3(0.0f, m_Speed * Time.deltaTime, 0.0f);
            m_Speed -= m_Gravity * Time.deltaTime;
            if(transform.position.y < m_StartingYPos)
            {
                transform.position = new Vector3(transform.position.x, m_StartingYPos, transform.position.z);
                m_Jumping = false;
                m_StartFrame = true;
            }
        }	
	}

    public void Jump()
    {
        m_Jumping = true;
    }
}
