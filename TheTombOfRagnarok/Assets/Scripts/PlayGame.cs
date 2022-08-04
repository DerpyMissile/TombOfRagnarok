using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PlayGame : MonoBehaviour
{
    [SerializeField] Button startGame;
    [SerializeField] Button palButton;
    [SerializeField] Button druiButton;
    [SerializeField] Button necroButton;
    [SerializeField] Button rangButton;
    [SerializeField] Button roguButton;
    [SerializeField] Button magButton;
    [SerializeField] GameObject background;
    [SerializeField] Sprite characterScreen;
    //public List<Player> ourCharas = new List<Player>();
    [SerializeField] GameObject ourCharas;

    void playTheGame(){
        Debug.Log("Wassup jimbo");
        background.GetComponent<Image>().sprite = characterScreen;
        startGame.gameObject.SetActive(false);
        palButton.gameObject.SetActive(true);
        druiButton.gameObject.SetActive(true);
        necroButton.gameObject.SetActive(true);
        rangButton.gameObject.SetActive(true);
        roguButton.gameObject.SetActive(true);
        magButton.gameObject.SetActive(true);
    }

    void selectClass(string whatClass){
        Player.setStats(whatClass);

        switch(whatClass){
            case "Paladin":
            Deck.addCard(new Smite());
            Deck.addCard(new Smite());
            Deck.addCard(new Smite());
            Deck.addCard(new Incite());
            Deck.addCard(new Incite());
            break;

            case "Druid":
            Deck.addCard(new Smite());
            Deck.addCard(new Smite());
            Deck.addCard(new Smite());
            Deck.addCard(new Incite());
            Deck.addCard(new Incite());
            break;

            case "Necromancer":
            Deck.addCard(new Smite());
            Deck.addCard(new Smite());
            Deck.addCard(new Smite());
            Deck.addCard(new Incite());
            Deck.addCard(new Incite());
            break;

            case "Ranger":
            Deck.addCard(new Smite());
            Deck.addCard(new Smite());
            Deck.addCard(new Smite());
            Deck.addCard(new Incite());
            Deck.addCard(new Incite());
            break;

            case "Rogue":
            Deck.addCard(new Smite());
            Deck.addCard(new Smite());
            Deck.addCard(new Smite());
            Deck.addCard(new Incite());
            Deck.addCard(new Incite());
            break;

            case "Mage":
            Deck.addCard(new Smite());
            Deck.addCard(new Smite());
            Deck.addCard(new Smite());
            Deck.addCard(new Incite());
            Deck.addCard(new Incite());
            break;

            case null:
            Deck.addCard(new Smite());
            Deck.addCard(new Smite());
            Deck.addCard(new Smite());
            Deck.addCard(new Incite());
            Deck.addCard(new Incite());
            break;
        }

        StartCoroutine(actuallyStartGame());
    }

    IEnumerator actuallyStartGame(){
        Scene currentScene = SceneManager.GetActiveScene();
        AsyncOperation asyncload = SceneManager.LoadSceneAsync("Board", LoadSceneMode.Additive);
        while(!asyncload.isDone){
            yield return null;
        }
        PlayerPrefs.SetInt("UnitySelectMonitor", 2);
        SceneManager.MoveGameObjectToScene(ourCharas, SceneManager.GetSceneByName("Board"));
        SceneManager.UnloadSceneAsync(currentScene);
    }
    // Start is called before the first frame update
    void Start()
    {
        startGame.onClick.AddListener(playTheGame);
        palButton.gameObject.SetActive(false);
        druiButton.gameObject.SetActive(false);
        necroButton.gameObject.SetActive(false);
        rangButton.gameObject.SetActive(false);
        roguButton.gameObject.SetActive(false);
        magButton.gameObject.SetActive(false);
        palButton.onClick.AddListener(delegate{selectClass("Paladin");});
        druiButton.onClick.AddListener(delegate{selectClass("Druid");});
        necroButton.onClick.AddListener(delegate{selectClass("Necromancer");});
        rangButton.onClick.AddListener(delegate{selectClass("Ranger");});
        roguButton.onClick.AddListener(delegate{selectClass("Rogue");});
        magButton.onClick.AddListener(delegate{selectClass("Mage");});
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
