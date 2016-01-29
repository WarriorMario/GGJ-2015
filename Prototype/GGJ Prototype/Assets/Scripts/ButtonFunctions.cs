using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonFunctions : MonoBehaviour {

    public enum ButtonType { STARTGAME, QUIT, OPTIONS }
    public ButtonType m_MyType;

    public ButtonManager m_Manager;
    public ButtonFunctions m_Up;
    public ButtonFunctions m_Left;
    public ButtonFunctions m_Right;
    public ButtonFunctions m_Down;

    public Sprite m_InactiveState;
    public Sprite m_ActiveState;
    public Image m_UsedImage;

    public Text m_NumberSelection;

    private float m_Timer;
    private float m_ButtonTimer;

    void Start()
    {
        m_ButtonTimer = m_Manager.m_ButtonTimer;
    }

    public void Active()
    {
        m_UsedImage.sprite = m_ActiveState;

        if(Input.GetButtonDown("Submit") && m_Timer < 0)
        {
            m_Timer = m_ButtonTimer;
            Submit();
        }

        if(Input.GetAxisRaw("Horizontal0") > 0 && m_Timer < 0)
        {
            m_Timer = m_ButtonTimer;
            if(m_Right != null)
            {
                m_Manager.m_ActiveButton = m_Right;
                m_UsedImage.sprite = m_InactiveState;
            }
        }
        else if(Input.GetAxisRaw("Horizontal0") < 0 && m_Timer < 0)
        {
            m_Timer = m_ButtonTimer;
            if (m_Left != null)
            {
                m_Manager.m_ActiveButton = m_Left;
                m_UsedImage.sprite = m_InactiveState;
            }
        }
        else if (Input.GetAxisRaw("Vertical0") > 0 && m_Timer < 0)
        {
            m_Timer = m_ButtonTimer;
            if (m_Up != null)
            {
                m_Manager.m_ActiveButton = m_Up;
                m_UsedImage.sprite = m_InactiveState;
            }
        }
        else if (Input.GetAxisRaw("Vertical0") < 0 && m_Timer < 0)
        {
            m_Timer = m_ButtonTimer;
            if (m_Down != null)
            {
                m_Manager.m_ActiveButton = m_Down;
                m_UsedImage.sprite = m_InactiveState;
            }
        }
        m_Timer -= Time.deltaTime;
    }


    public void Submit()
    {
        switch(m_MyType)
        {
            case ButtonType.STARTGAME:
                {
                    if(m_NumberSelection != null)
                    {
                        m_NumberSelection.gameObject.SetActive(true);
                    }
                }
                break;
            case ButtonType.QUIT:
                {
                    Application.Quit();
                }
                break;
        }
    }
}
