using UnityEngine;
using System.Collections;

public class PlayerAiming : MonoBehaviour
{
    public int m_Player;
    public float m_XSpeed;
    public float m_ZSpeed;

    public GameObject m_Indicator;
    public ProjectileSlot m_ProjectileSlot;

    Vector3 m_MoveDirection;
    string m_HorizontalInput = "HorizontalLook";
    string m_VerticalInput = "VerticalLook";
    string m_ThrowInput = "Throw";

    void Awake()
    {
        m_HorizontalInput += m_Player.ToString();
        m_VerticalInput += m_Player.ToString();
        m_ThrowInput += m_Player.ToString();

        m_MoveDirection = Vector3.zero;
    }

    void Update()
    {
        m_MoveDirection = new Vector3(Input.GetAxis(m_HorizontalInput) * m_XSpeed, 0, Input.GetAxis(m_VerticalInput) * m_ZSpeed);

        if (m_ProjectileSlot.Filled)
        {
            if (Input.GetButton(m_ThrowInput))
            {
                m_ProjectileSlot.Fire(m_Indicator.transform.position);
            }
        }
    }

    void FixedUpdate()
    {
        if (m_MoveDirection.x != 0 || m_MoveDirection.z != 0)
        {
            // Move the indicator.
            float x = m_Indicator.transform.position.x + m_MoveDirection.x;
            float y = m_Indicator.transform.position.y;
            float z = m_Indicator.transform.position.z + m_MoveDirection.z;
            m_Indicator.transform.position = new Vector3(x, y, z);
        }
    }
}