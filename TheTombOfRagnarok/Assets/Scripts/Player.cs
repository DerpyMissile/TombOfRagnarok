using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] int hp;
    [SerializeField] int atk;
    [SerializeField] int movement;
    [SerializeField] int money = 0;

    public Player(){
        hp = 1;
        atk = 1;
        movement = 1;
    }

    public Player setStats(String playerChara){
        switch(playerChara){
            case "Paladin":
            hp = 20;
            atk = 1;
            movement = 2;
            break;
            //Paladin will be more character-focused, minimal attack, lotsa hp

            case "Druid":
            hp = 12;
            atk = 2;
            movement = 3;
            break;
            //Druid will be more spell-focused with healing and attack spells mostly

            case "Necromancer":
            hp = 10;
            atk = 2;
            movement = 3;
            break;
            //Necromancer will be team-focused naturally as he summons. Lots.

            case "Ranger":
            hp = 7;
            atk = 3;
            movement = 3;
            break;
            //Ranger will be more character-focused with range??

            case "Rogue":
            hp = 8;
            atk = 4;
            movement = 4;
            break;
            //Rogue aka Assassin is character focused, strong atk low hp

            case "Mage":
            hp = 8;
            atk = 4;
            movement = 3;
            break;
            //Mage does aoe now

            case null:
            hp = 1;
            atk = 1;
            movement = 1;
            break;
        }
        return this;
    }

    public int getHP(){
        return hp;
    }

    public int getAtk(){
        return atk;
    }

    public int getMovement(){
        return movement;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
