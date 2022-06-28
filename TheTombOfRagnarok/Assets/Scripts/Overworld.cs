using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overworld : MonoBehaviour
{
    public GameObject player;
    private bool isPlacedDown = false;

    void movePlayerCharacter(){

    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector3(0, 1, 0);
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
                player.transform.position = hitObject.transform.position;
                player.transform.Rotate(1.0f, 0, 1.0f, Space.Self);
                if(Input.GetMouseButtonDown(0)){
                    isPlacedDown = true;
                    player.GetComponent<Rigidbody>().useGravity = true;
                }
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            //Debug.Log("Did not Hit");
        }
    }
}
