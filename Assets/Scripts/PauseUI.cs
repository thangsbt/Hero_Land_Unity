using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseUI : MonoBehaviour
{
    public bool pause = false;
    public GameObject pauseUI;
   
    void Start()
    {
        pauseUI.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) )
        {
            pause = !pause;
        }
        if (pause)
        {
            pauseUI.SetActive(true);
            Time.timeScale = 0;
        }
        if (pause == false)
        {
            pauseUI.SetActive(false);
            Time.timeScale = 1;
        }
        
    }
    public void Resume()
    {
        pause = false;
    }
    public void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    //public void Pause_Button()
    //{
    //    if (Input.GetMouseButton(0))
    //    {
    //        pause = true;
            
    //    }
    //    if (pause == true)
    //    {
    //        pauseUI.SetActive(true);
    //        Time.timeScale = 0;
    //    }
    //    if (pause == false)
    //    {
    //        pauseUI.SetActive(false);
    //        Time.timeScale = 1;
    //    }
    //}
}
