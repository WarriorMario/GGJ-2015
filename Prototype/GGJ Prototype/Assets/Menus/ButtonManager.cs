using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonManager : MonoBehaviour {

    [HideInInspector]
    public ButtonScript m_ActiveButton;
    public ButtonScript m_ActiveButtonMenu;
    public ButtonScript m_ActiveButtonPlayerSelection;
    public ButtonScript m_ActiveLevelSelection;
    public Image m_InstructionImage;
    public GameObject m_PlayerAmountSelection;
    public GameObject m_NormalMenu;
    public GameObject m_LevelSelection;

    private float m_Timer;
    public float m_TimerLimit;

    // Use this for initialization
    void Start()
    {
        m_ActiveButton = m_ActiveButtonMenu;
        m_InstructionImage.gameObject.SetActive(false);
        DeActivateLevelSelection();
        DeActivatePlayerSelection();
    }

    void Update()
    {
        if (m_InstructionImage.gameObject.activeSelf)
        {
            if (Input.GetButtonDown("Throw0"))
            {
                m_InstructionImage.gameObject.SetActive(false);
            }
        }
        else if(m_PlayerAmountSelection.activeSelf)
        {
            if (Input.GetButtonDown("Throw0"))
            {
                DeActivatePlayerSelection();
            }
            else
            {
                m_ActiveButton.Active();
            }
        }
        else if(m_LevelSelection.activeSelf)
        {
            if (Input.GetButtonDown("Throw0"))
            {
                DeActivateLevelSelection();
            }
            else
            {
                m_ActiveButton.Active();
            }
        }
        else
        {
            m_ActiveButton.Active();
        }
    }

    public void ChangeActiveButton(ButtonScript button)
    {
        m_ActiveButton.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        m_ActiveButton = button;
        m_ActiveButton.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }

    public void ActivatePlayerSelection()
    {
        m_PlayerAmountSelection.SetActive(true);
        ChangeActiveButton(m_ActiveButtonPlayerSelection);
    }

    public void DeActivatePlayerSelection()
    {
        ChangeActiveButton(m_ActiveButtonMenu);
        m_PlayerAmountSelection.SetActive(false);
    }

    public void ActivateLevelSelection()
    {
        m_LevelSelection.SetActive(true);
        m_PlayerAmountSelection.SetActive(false);
        ChangeActiveButton(m_ActiveLevelSelection);
        m_NormalMenu.SetActive(false);
    }

    public void DeActivateLevelSelection()
    {
        m_NormalMenu.SetActive(true);
        m_PlayerAmountSelection.SetActive(true);
        ChangeActiveButton(m_ActiveButtonPlayerSelection);
        m_LevelSelection.SetActive(false);
    }
}
