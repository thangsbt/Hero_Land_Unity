using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Door : MonoBehaviour
{
    public int Levelload ;
    //public GameController gm;
    // Start is called before the first frame update
    void Start()
    {
       // gm = GameObject.FindGameObjectWithTag("gamecontroller").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            savescore();
            GameController.instance.Inputtext.text = ("Press E to enter");
        }
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                savescore();
                SceneManager.LoadScene(Levelload);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            GameController.instance.Inputtext.text = ("");
        }
    }
    void savescore()
    {
        PlayerPrefs.SetInt("points", GameController.instance.score);
    }
}
