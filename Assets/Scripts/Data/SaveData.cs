using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    // Start is called before the first frame update
    public static SaveData Instance;

    public string playerid;

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
