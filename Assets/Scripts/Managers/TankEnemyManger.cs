using System;
using UnityEngine;

[Serializable]
public class TankEnemyManger
{
    // Start is called before the first frame update
 public Color m_PlayerColor;            
    public Transform m_SpawnPoint;         
    [HideInInspector] public int m_EnemyNumber;             
    [HideInInspector] public string m_ColoredPlayerText;
    [HideInInspector] public GameObject m_Instance;          
    [HideInInspector] public int m_Wins;    
    [HideInInspector] public Transform m_Player;                
    private PathFinding m_pathfinding;
    //private TankMovement m_Movement;       
    //private TankShooting m_Shooting;
    private TankHealth m_Health;
    private GameObject m_CanvasGameObject;


    public void Setup(GameObject _Player)
    {
        //m_Movement = m_Instance.GetComponent<TankMovement>();
        //m_Shooting = m_Instance.GetComponent<TankShooting>();
        m_Health = m_Instance.GetComponent<TankHealth>();
        m_CanvasGameObject = m_Instance.GetComponentInChildren<Canvas>().gameObject;

        // m_Movement.m_PlayerNumber = m_PlayerNumber;
        // m_Shooting.m_PlayerNumber = m_PlayerNumber;

        m_pathfinding = m_Instance.GetComponent<PathFinding>();
        m_pathfinding.Point1 = _Player;
        m_ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(m_PlayerColor) + ">Enemy " + m_EnemyNumber + "</color>";

        MeshRenderer[] renderers = m_Instance.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = m_PlayerColor;
        }
    }
    public void DisableControl()
    {
       // m_Movement.enabled = false;
        //m_Shooting.enabled = false;

        m_CanvasGameObject.SetActive(false);
    }


    public void EnableControl()
    {
        //m_Movement.enabled = true;
        //m_Shooting.enabled = true;

        m_CanvasGameObject.SetActive(true);
    }


    public void Reset()
    {
        m_Instance.transform.position = m_SpawnPoint.position;
        m_Instance.transform.rotation = m_SpawnPoint.rotation;

        m_Instance.SetActive(false);
        m_Instance.SetActive(true);
    }
    public float GetHP(){
        return m_Health.GetCurrentHp();
    }

    public void AddHp(){
        m_Health.Health = 20;
    }
}
