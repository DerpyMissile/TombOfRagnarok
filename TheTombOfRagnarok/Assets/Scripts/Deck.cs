using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Deck
{
    static public List<Card> cards = new List<Card>();

    static public void addCard(Card newCard){
        cards.Add(newCard);
    }
}
