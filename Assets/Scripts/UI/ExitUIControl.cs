using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ExitUIControl : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ExitCanvas;
    public GameObject BagCanvas;
    public GameObject ShopCanvas;
    private static bool GameIsPaused = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Esc get");
            if(GameIsPaused)
            {
                Resume(0);
            }else
            {
                Pause(0);
            }
        }
        if(Input.GetKeyDown(KeyCode.B))
        {
            if(GameIsPaused)
            {
                Resume(1);
            }else
            {
                Pause(1);
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (GameIsPaused)
            {
                Resume(2);
            }
            else
            {
                Pause(2);
            }
        }
    }
     public void Resume(int _type)
    {
        if (_type == 0) ExitCanvas.SetActive(false);
        if (_type == 1) BagCanvas.SetActive(false);
        if (_type == 2) ShopCanvas.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPaused = false;
    }

    public void Pause(int _type)
    {
        if (_type == 0) ExitCanvas.SetActive(true);
        if (_type == 1) BagCanvas.SetActive(true);
        if (_type == 2) ShopCanvas.SetActive(true);
        Time.timeScale = 0.0f;
        GameIsPaused = true;
    }

    public void ReturnToMain()
    {
        GameIsPaused = false;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
}
