using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Overworld : MonoBehaviour
{
    public GameObject player;
    public bool isPlacedDown = false;
    public bool touchedButton = false;
    public bool gotNewLocation = false;
    private int movesLeft = 0;
    [SerializeField] Slider howPowerful;
    [SerializeField] Button rollDieButton;
    GameObject moveToWhere;

    IEnumerator movePlayerCharacter(){
        Debug.Log("This actually runs");
        yield return new WaitUntil(() => isPlacedDown);
        Debug.Log("Does it even make it this far");
        yield return new WaitUntil(() => touchedButton);
        while(touchedButton){
            yield return new WaitUntil(() => touchedButton);
            Debug.Log("hmmm");
            movesLeft = Mathf.RoundToInt(Mathf.Ceil(Random.Range(0, 20)));
            howPowerful.value = movesLeft;
        }
        touchedButton = false;
        Debug.Log("Made it this far");
        Debug.Log(howPowerful.value);

        Debug.Log(movesLeft);
        if(movesLeft > 10){
            Player.increaseMoney(movesLeft);
            movesLeft = 0;
        }
        while(movesLeft>0){
            yield return new WaitUntil(() => gotNewLocation);
            Vector3 playerLastPos = player.transform.position;
            Debug.Log("I have " + movesLeft + " moves left.");
            Debug.Log(Mathf.RoundToInt(moveToWhere.transform.position.z));
            Debug.Log(Mathf.RoundToInt(player.transform.position.z));
            Debug.Log(Mathf.RoundToInt(moveToWhere.transform.position.x));
            Debug.Log(Mathf.RoundToInt(player.transform.position.x));
            if(Mathf.RoundToInt(moveToWhere.transform.position.x) == Mathf.RoundToInt(player.transform.position.x)){
                if(Mathf.Abs(Mathf.RoundToInt(moveToWhere.transform.position.z) - Mathf.RoundToInt(player.transform.position.z)) <= movesLeft){
                    player.transform.position = moveToWhere.transform.position + new Vector3(0, 1, 0);
                    //movesLeft -= (int)Mathf.Abs(Mathf.RoundToInt(moveToWhere.transform.position.z) - Mathf.RoundToInt(player.transform.position.z));
                }else{

                }
            }else if((Mathf.RoundToInt(moveToWhere.transform.position.x) != Mathf.RoundToInt(player.transform.position.x)) && (Mathf.RoundToInt(moveToWhere.transform.position.z) != Mathf.RoundToInt(player.transform.position.z))){
                //do nothing
                movesLeft += Mathf.Abs(Mathf.RoundToInt(moveToWhere.transform.position.z) - Mathf.RoundToInt(playerLastPos.z)) + Mathf.Abs(Mathf.RoundToInt(moveToWhere.transform.position.x) - Mathf.RoundToInt(playerLastPos.x));
            }else if((Mathf.RoundToInt(moveToWhere.transform.position.x) == Mathf.RoundToInt(player.transform.position.x)) && (Mathf.RoundToInt(moveToWhere.transform.position.z) == Mathf.RoundToInt(player.transform.position.z))){
                //ends turn
                Debug.Log("end turn");
                movesLeft = 0;
                break;
            }else{
                if(Mathf.Abs(Mathf.RoundToInt(moveToWhere.transform.position.x) - Mathf.RoundToInt(player.transform.position.x)) <= movesLeft){
                    player.transform.position = moveToWhere.transform.position + new Vector3(0, 1, 0);
                    //movesLeft -= (int)Mathf.Abs(Mathf.RoundToInt(moveToWhere.transform.position.x) - Mathf.RoundToInt(player.transform.position.x));
                }else{
                    movesLeft += Mathf.Abs(Mathf.RoundToInt(moveToWhere.transform.position.z) - Mathf.RoundToInt(playerLastPos.z)) + Mathf.Abs(Mathf.RoundToInt(moveToWhere.transform.position.x) - Mathf.RoundToInt(playerLastPos.x));
                }
            }
            movesLeft -= Mathf.Abs(Mathf.RoundToInt(moveToWhere.transform.position.z) - Mathf.RoundToInt(playerLastPos.z)) + Mathf.Abs(Mathf.RoundToInt(moveToWhere.transform.position.x) - Mathf.RoundToInt(playerLastPos.x));
            gotNewLocation = false;
        }
        gotNewLocation = false;
        Debug.Log("end end turn kachow");

        //battle start nyoooooom
        //Player playerOnTheBoard = Instantiate(player.GetComponent<Player>());
        //Battle battleStart = new Battle(player.GetComponent<Player>(), Mathf.RoundToInt(Mathf.Ceil(Random.Range(0, 4))), moveToWhere);
    }

    public void buttonHasBeenTouched(){
        touchedButton = true;
    }

    public void buttonHasNotBeenTouched(){
        touchedButton = false;
    }

    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector3(0, 1, 0);
        rollDieButton.onClick.AddListener(buttonHasBeenTouched);
        howPowerful.minValue = 1;
        howPowerful.maxValue = 20;
        StartCoroutine(movePlayerCharacter());
    }

    // public void onPointerDown(PointerEventData rollDieButton){
    //     touchedButton = true;
    // }

    // public void onPointerUp(PointerEventData rollDieButton){
    //     touchedButton = false;
    // }

    // Update is called once per frame
    void Update()
    {
        //touchedButton = false;
        if(Input.GetKeyDown(KeyCode.E)) buttonHasBeenTouched();
        if(Input.GetKeyUp(KeyCode.E)) buttonHasNotBeenTouched();
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
            if(!isPlacedDown){
                player.GetComponent<Rigidbody>().useGravity = false;
                player.transform.position = hitObject.transform.position + new Vector3(0,1,0);
                player.transform.Rotate(1.0f, 0, 1.0f, Space.Self);
                if(Input.GetMouseButtonDown(0)){
                    isPlacedDown = true;
                    player.GetComponent<Rigidbody>().useGravity = true;
                }
            }else{
                moveToWhere = hitObject;
                gotNewLocation = true;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            //Debug.Log("Did not Hit");
        }
    }
}
