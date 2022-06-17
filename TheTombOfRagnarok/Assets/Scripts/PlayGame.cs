using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    [SerializeField] Button startGame;
    public List<Player> ourCharas = new List<Player>();

    void playTheGame(){
        Debug.Log("Wassup jimbo");
        SceneManager.LoadScene("Board");
        SceneManager.UnloadScene("StartMenu");
        ourCharas.Add(new Player().setStats("Necromancer"));
    }
    // Start is called before the first frame update
    void Start()
    {
        startGame.onClick.AddListener(playTheGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
