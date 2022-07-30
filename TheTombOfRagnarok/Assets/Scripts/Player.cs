using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Player
{
    static int hp = 1;
    static int atk = 1;
    static int movement = 1;
    static int money = 0;
    static Vector3 position = new Vector3(0, 0, 0);

    public static void setStats(String playerChara){
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
    }

    public static int getHP(){
        return hp;
    }

    public static int getAtk(){
        return atk;
    }

    public static int getMovement(){
        return movement;
    }

    public static void increaseMoney(int howMuch){
        money += howMuch;
    }
}
