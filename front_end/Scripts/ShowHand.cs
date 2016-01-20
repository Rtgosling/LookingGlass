using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CardPop))] //requires cardpop to work

public class ShowHand : MonoBehaviour {
    public Vector3 start;
    public float cardOffset;

    public GameObject cardPrefab;

    public CardManager cardManager; //global array and variable access

    int cardCount = 0; //to save how many cards in handss

    void Start()
    {
        
    }

    public void ShowCards(){
    
        float co = cardOffset * cardCount; //for calculating the position of the next card

        GameObject cardCopy = (GameObject)Instantiate(cardPrefab); //takes in cardPrefab and creates a copy of it

        cardCopy.GetComponent<SpriteRenderer>().enabled = true; //Original is invisible -> make copies visible

        cardCopy.name = "Hand " + (cardManager.cardsHand.Count - 1); // rename clone and give it number to position within hand

        Vector3 temp = start + new Vector3(co, 0f); //create a vector with the start position having the card offset calculation added to it
        cardCopy.transform.position = temp;  //positioning each card after the last

        float myScale = 0.8f;
        cardCopy.transform.localScale = new Vector3(myScale, myScale); //resize the clones 

        cardCopy.GetComponent<SpriteRenderer>().sprite = cardManager.faces[cardManager.cardsHand[cardManager.cardsHand.Count -1]]; //Make the sprite image equal to the last index added to the card hand
        

        cardCount++;
        
    }
}
