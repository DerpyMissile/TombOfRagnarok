using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, MOVEPLAYER, ATTACKPLAYER, MOVEENEMY, SUMMONENEMY, ATTACKENEMY, WON, LOST }

public class Battle : MonoBehaviour
{
    [SerializeField] Text myText;
    [SerializeField] Text myText2;
    [SerializeField] Text myText3;
    [SerializeField] Button nextPhase;
    int movesLeft;
    int turn = 1;
    int playerMana = 0;
    int enemyMana = 0;
    int maxMana = 0;
    [SerializeField] Rigidbody playerCharac;
    //[SerializeField] Enemy enemyCharac;
    List<Enemy> enemies = new List<Enemy>();
    bool placedCharac = false;
    bool isPlayerTurn = true;
    bool selectedNewMovementLocation = false;
    bool battleOver = false;
    public BattleState state;
    GameObject moveToWhere;
    [SerializeField] List<Card> hand = new List<Card>();
    [SerializeField] List<Card> battleDeck = Deck.cards;

    public Battle(int howManyEnemies, GameObject whatTile){
        for(int i=0; i<howManyEnemies; ++i){
            enemies.Add(new Enemy());
        }
    }

    void enemyTurn(){
        maxMana++;
        goMoveEnemy();
        summonIfPossible();
        attackIfCan();
    }

    void increasePhase(){
        state++;
    }

    IEnumerator goMove(){
        //Debug.Log(movesLeft);
        //yield return new WaitUntil(() => selectedNewMovementLocation);
        myText.text = "Where should I go?\nI have " + Player.getMovement() + " spaces left to move.";
        while((movesLeft>0) && (state == BattleState.MOVEPLAYER)){
            yield return new WaitUntil(() => selectedNewMovementLocation);
            Vector3 playerLastPos = playerCharac.transform.position;
            Debug.Log("I have " + movesLeft + " moves left.");
            Debug.Log(Mathf.RoundToInt(moveToWhere.transform.position.z));
            Debug.Log(Mathf.RoundToInt(playerCharac.transform.position.z));
            Debug.Log(Mathf.RoundToInt(moveToWhere.transform.position.x));
            Debug.Log(Mathf.RoundToInt(playerCharac.transform.position.x));
            myText.text = "Where should I go?\nI have " + Player.getMovement() + " spaces left to move.";
            myText2.text = "Movement Phase";
            if(Mathf.RoundToInt(moveToWhere.transform.position.x) == Mathf.RoundToInt(playerCharac.transform.position.x)){
                if(Mathf.Abs(Mathf.RoundToInt(moveToWhere.transform.position.z) - Mathf.RoundToInt(playerCharac.transform.position.z)) <= movesLeft){
                    playerCharac.transform.position = moveToWhere.transform.position + new Vector3(0, 1, 0);
                    //movesLeft -= (int)Mathf.Abs(Mathf.RoundToInt(moveToWhere.transform.position.z) - Mathf.RoundToInt(playerCharac.transform.position.z));
                }else{

                }
            }else if((Mathf.RoundToInt(moveToWhere.transform.position.x) != Mathf.RoundToInt(playerCharac.transform.position.x)) && (Mathf.RoundToInt(moveToWhere.transform.position.z) != Mathf.RoundToInt(playerCharac.transform.position.z))){
                //do nothing
                movesLeft += Mathf.Abs(Mathf.RoundToInt(moveToWhere.transform.position.z) - Mathf.RoundToInt(playerLastPos.z)) + Mathf.Abs(Mathf.RoundToInt(moveToWhere.transform.position.x) - Mathf.RoundToInt(playerLastPos.x));
            }else{
                if(Mathf.Abs(Mathf.RoundToInt(moveToWhere.transform.position.x) - Mathf.RoundToInt(playerCharac.transform.position.x)) <= movesLeft){
                    playerCharac.transform.position = moveToWhere.transform.position + new Vector3(0, 1, 0);
                    //movesLeft -= (int)Mathf.Abs(Mathf.RoundToInt(moveToWhere.transform.position.x) - Mathf.RoundToInt(playerCharac.transform.position.x));
                }else{
                    movesLeft += Mathf.Abs(Mathf.RoundToInt(moveToWhere.transform.position.z) - Mathf.RoundToInt(playerLastPos.z)) + Mathf.Abs(Mathf.RoundToInt(moveToWhere.transform.position.x) - Mathf.RoundToInt(playerLastPos.x));
                }
            }
            movesLeft -= Mathf.Abs(Mathf.RoundToInt(moveToWhere.transform.position.z) - Mathf.RoundToInt(playerLastPos.z)) + Mathf.Abs(Mathf.RoundToInt(moveToWhere.transform.position.x) - Mathf.RoundToInt(playerLastPos.x));
            selectedNewMovementLocation = false;
        }
        movesLeft = Player.getMovement();
        StopCoroutine(goMove());
        StartCoroutine(playCards());
    }

    IEnumerator playCards(){
        while(state == BattleState.ATTACKPLAYER){
            if(maxMana == 0){
                for(int i=0; i<5; ++i){
                    if(battleDeck.Count == 0){
                        break;
                    }
                    int randNum = Mathf.RoundToInt(Random.Range(0, battleDeck.Count));
                    hand.Add(battleDeck[randNum]);
                    battleDeck.RemoveAt(randNum);
                }
            }
        }
    }

    void goMoveEnemy(){

    }

    void summonIfPossible(){

    }

    void attackIfCan(){

    }

    void checkIfOver(){
        if(Player.getHP() <= 0){
            battleOver = true;
            return;
        }
        for(int i=0; i<enemies.Count; ++i){
            if(enemies[i].getHP()<=0){

            }else{
                battleOver = false;
                return;
            }
        }
        battleOver = true;
        return;
    }

    IEnumerator setUpBoard(){
        yield return new WaitUntil(() => placedCharac);
        state = BattleState.MOVEPLAYER;
        Debug.Log("Made it to phase 2");
        StopCoroutine(setUpBoard());
        StartCoroutine(goMove());
    }
    // Start is called before the first frame update
    void Start()
    {
        myText.text = "Pick a square to place your player!";
        myText2.text = "Placement Phase";
        myText3.text = "Current Mana: " + playerMana;
        nextPhase.onClick.AddListener(increasePhase);
        state = BattleState.START;
        movesLeft = Player.getMovement();
        StartCoroutine(setUpBoard());
    }

    // Update is called once per frame
    void Update()
    {
        checkIfOver();
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
        // Does the ray intersect any objects excluding the player layer
        //Debug.Log("Working so far");
        //Debug.Log("kachow");
        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            //Debug.Log("Did Hit");
            GameObject hitObject = hit.collider.transform.gameObject;
            //Debug.Log(hitObject.name);
            if(!placedCharac && Input.GetMouseButtonDown(0)){
                playerCharac.transform.position = hitObject.transform.position + new Vector3(0, 1, 0);
                placedCharac = true;
                moveToWhere = hitObject;
                //selectedNewMovementLocation = true;
            }else if(state == BattleState.MOVEPLAYER && Input.GetMouseButtonDown(0) && (hitObject != moveToWhere)){
                moveToWhere = hitObject;
                selectedNewMovementLocation = true;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            //Debug.Log("Did not Hit");
        }
    }
}


/*
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
*/