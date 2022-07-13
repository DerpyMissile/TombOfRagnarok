using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    int hp;
    int atk;
    int movement;

    public Enemy setStats(String enemyChara){
        switch(enemyChara){
            //For the waterfall section
            case "SOTW":
            hp = 3;
            atk = 2;
            movement = 3;
            break;
            //spirit of the waterfall stats

            case "Slime":
            hp = 2;
            atk = 1;
            movement = 1;
            break;
            //slime stats

            case "Spook":
            hp = 3;
            atk = 2;
            movement = 2;
            break;
            //haha skeletal go brrrrr

            case "MDog":
            hp = 5;
            atk = 1;
            movement = 2;
            break;
            //bork bork

            case "Bard":
            hp = 20;
            atk = 5;
            movement = 4;
            break;
            //is a bard

            //For the Room of Blades section
            case "FKnife":
            hp = 3;
            atk = 2;
            movement = 3;
            break;
            //knife go whoosh

            case "SOTYW":
            hp = 2;
            atk = 5;
            movement = 2;
            break;
            //HYAAAA!

            case "GArmor":
            hp = 5;
            atk = 1;
            movement = 2;
            break;
            //a big ol suit

            case "SHour":
            hp = 2;
            atk = 10;
            movement = 1;
            break;
            //shank shank

            case "RK-HBD-666":
            hp = 15;
            atk = 7;
            movement = 4;
            break;
            //beepboop kill kill

            //For the plains area
            case "Cri":
            hp = 5;
            atk = 3;
            movement = 2;
            break;
            //ghastly hourglass

            case "SOTHA":
            hp = 20;
            atk = 5;
            movement = 4;
            break;
            //is a bard

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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
