using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public string name;
    public int atk;
    public int hp;
    public int movement;
    public int range;
    public string type;
    //ok so there's technically 4 types: Summon, Astral, Boon, Empower
    //Summons are summon monster to fight for you
    //Astral are monster effects
    //Boons are spells you cast to either harm your foes or helpful charms to assist your troops
    //Empower help you, the character
}
