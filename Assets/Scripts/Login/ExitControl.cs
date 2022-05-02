using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
    public void OnClilkConfirm()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    
}
