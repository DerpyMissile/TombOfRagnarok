using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battle : MonoBehaviour
{
    [SerializeField] Text myText;
    [SerializeField] Text myText2;
    [SerializeField] Text myText3;
    [SerializeField] Button nextPhase;
    int turn = 1;
    int playerMana = 0;
    int enemyMana = 0;
    int maxMana = 0;
    int phaseNumber = 1;
    [SerializeField] Player playerCharac;
    [SerializeField] Enemy enemyCharac;
    bool placedCharac = false;
    bool isPlayerTurn = true;

    Battle(Player mc, Enemy nc){
        playerCharac = mc;
        enemyCharac = nc;
    }

    void enemyTurn(){
        maxMana++;
        goMoveEnemy();
        summonIfPossible();
        attackIfCan();
    }

    void increasePhase(){
        phaseNumber++;
    }

    void goMove(GameObject moveTo){
        int movesLeft = playerCharac.getMovement();
        while(movesLeft>0 || phaseNumber==2){
            myText.text = "Where should I go?\nI have " + playerCharac.getMovement() + " spaces left to move.";
            myText2.text = "Movement Phase";
            if(moveTo.transform.position.x == playerCharac.transform.position.x){
                if(Mathf.Abs(moveTo.transform.position.z - playerCharac.transform.position.z) <= movesLeft){
                    playerCharac.transform.position = moveTo.transform.position + new Vector3(0, 1, 0);
                    movesLeft -= (int)Mathf.Abs(moveTo.transform.position.z - playerCharac.transform.position.z);
                }else{

                }
            }else{
                if(Mathf.Abs(moveTo.transform.position.x - playerCharac.transform.position.x) <= movesLeft){
                    playerCharac.transform.position = moveTo.transform.position + new Vector3(0, 1, 0);
                    movesLeft -= (int)Mathf.Abs(moveTo.transform.position.x - playerCharac.transform.position.x);
                }else{

                }
            }
        }
        if(phaseNumber == 3){

        }else{
            phaseNumber++;
        }
    }

    void summonIfWant(){
        while(phaseNumber == 3){
            myText.text = "Where should I summon?";
            myText2.text = "Summon Phase";
        }
    }

    void attackIfWant(){

    }

    void goMoveEnemy(){

    }

    void summonIfPossible(){

    }

    void attackIfCan(){

    }
    // Start is called before the first frame update
    void Start()
    {
        myText.text = "Pick a square to place your player!";
        myText2.text = "Placement Phase";
        myText3.text = "Current Mana: " + playerMana;
        nextPhase.onClick.AddListener(increasePhase);
    }

    // Update is called once per frame
    void Update()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            GameObject hitObject = hit.collider.transform.gameObject;
            Debug.Log(hitObject.name);
            if(!placedCharac && Input.GetMouseButtonDown(0)){
                playerCharac.transform.position = hitObject.transform.position + new Vector3(0, 1, 0);
                placedCharac = true;
                phaseNumber++;
            }
            else if(isPlayerTurn && placedCharac){
                maxMana++;
                playerMana = maxMana;
                myText3.text = "Current Mana: " + playerMana;
                //goMove(hitObject);
                summonIfWant();
                attackIfWant();
                turn++;
                isPlayerTurn = false;
            }else{
                isPlayerTurn = true;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }
}
