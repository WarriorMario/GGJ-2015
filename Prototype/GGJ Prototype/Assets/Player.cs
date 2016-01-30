using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public int m_Player;
    public float m_XSpeed;
    public float m_ZSpeed;

    public float m_PickupOffset;
    public ProjectileSlot m_ProjectileSlot;

    CharacterController m_CharacterController;
    Vector3 m_FaceDirection;
    Vector3 m_MoveDirection;
    string m_HorizontalInput = "Horizontal";
    string m_VerticalInput = "Vertical";
    string m_PickupInput = "Pickup";

    GameObject m_LastHit;

    // Use this for initialization
    void Awake()
    {
        m_CharacterController = GetComponent<CharacterController>();
        m_HorizontalInput += m_Player.ToString();
        m_VerticalInput += m_Player.ToString();
        m_PickupInput += m_Player.ToString();

        m_FaceDirection = Vector3.forward;
        m_MoveDirection = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(Input.GetAxis(m_HorizontalInput)) > Mathf.Abs(Input.GetAxis(m_VerticalInput)))
        {
            m_MoveDirection = new Vector3(Input.GetAxis(m_HorizontalInput) * m_XSpeed, 0, 0);
        }
        else
        {
            m_MoveDirection = new Vector3(0, 0, Input.GetAxis(m_VerticalInput) * m_ZSpeed);
        }

        // Pickup.
        if (Input.GetButtonDown(m_PickupInput))
        {
            if (m_LastHit != null)
            {
                m_LastHit.GetComponent<Rigidbody>().useGravity = true;
                m_LastHit.GetComponent<BoxCollider>().enabled = false;
                m_LastHit.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
                Destroy(m_LastHit, 5f);
                m_LastHit = null;

                m_ProjectileSlot.Fill();
            }
        }
    }

    void FixedUpdate()
    {
        // Revert marked.
        if (m_LastHit != null)
            m_LastHit.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.white;

        if (m_MoveDirection.x != 0 || m_MoveDirection.z != 0)
        {
            m_FaceDirection = m_MoveDirection.normalized;
            transform.rotation = Quaternion.LookRotation(m_MoveDirection.normalized);
        }

        // Mark new.
        if (!m_ProjectileSlot.Filled)
            MarkForwardBlock();
        else
            m_LastHit = null;

        m_CharacterController.Move(m_MoveDirection);
    }

    void MarkForwardBlock()
    {
        // Block pickup.
        Vector3 direction = m_FaceDirection.normalized * m_PickupOffset;
        Vector3 position = new Vector3(transform.position.x + direction.x, transform.position.y, transform.position.z + direction.z);
        RaycastHit hit;
        if (Physics.Raycast(position, Vector3.down, out hit, 1f))
        {
            hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            m_LastHit = hit.transform.gameObject;
        }
    }
}
