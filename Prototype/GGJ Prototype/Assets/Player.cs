using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public int m_Player;
    public float m_XSpeed;
    public float m_YSpeed;

    CharacterController m_CharacterController;
    Vector3 m_MoveDirection;
    string m_HorizontalInput = "Horizontal";
    string m_VerticalInput = "Vertical";
    // Use this for initialization
    void Awake ()
    {
        m_CharacterController = GetComponent<CharacterController>();
        m_HorizontalInput += m_Player.ToString();
        m_VerticalInput += m_Player.ToString();
        m_MoveDirection = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Mathf.Abs(Input.GetAxis(m_HorizontalInput)) > Mathf.Abs(Input.GetAxis(m_VerticalInput)))
        {
            m_MoveDirection = new Vector3(Input.GetAxis(m_HorizontalInput)* m_XSpeed, 0, 0);
        }
        else //if(Mathf.Abs(Input.GetAxis(m_HorizontalInput)) <= Mathf.Abs(Input.GetAxis(m_VerticalInput)))
        {
            m_MoveDirection = new Vector3(0, 0, Input.GetAxis(m_VerticalInput)* m_YSpeed);
        }
	}

    void FixedUpdate()
    {
        transform.rotation = Quaternion.LookRotation(m_MoveDirection.normalized);
        m_CharacterController.Move(m_MoveDirection);
    }
}
