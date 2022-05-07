using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    // Start is called before the first frame update
    public static SaveData Instance;

    public string playerid;
    public int health;
    public int shield;
    public int attack;

    public float AIHealth;
    public int AIAddHp;
    public int ToolNum;
    public bool playerdie;
    public Transform player;
    private void Awake() 
    {
        if(Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }else if(Instance != null)
        {
            Destroy(gameObject);
        }
    }
}
