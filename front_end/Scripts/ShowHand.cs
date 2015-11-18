using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CardPop))] //requires cardpop to work

public class ShowHand : MonoBehaviour {
    public Sprite[] faces; //Images are dragged into inspector, must be the same order as in cardpop

    public Vector3 start;
    public float cardOffset;

    public GameObject cardPrefab;
    CardPop cardPop;

    int cardCount = 0; //to save how many cards in handss

    void Start()
    {
        cardPop = GetComponent<CardPop>();
        

    }

    public void ShowCards(){
    
        float co = cardOffset * cardCount; //for calculating the position of the next card

        GameObject cardCopy = (GameObject)Instantiate(cardPrefab); //takes in cardPrefab and creates a copy of it
        Vector3 temp = start + new Vector3(co, 0f); 
        cardCopy.transform.position = temp;  //positioning each card after the last

        float myScale = 0.8f;
        cardCopy.transform.localScale = new Vector3(myScale, myScale); //resize the clones 

        cardCopy.GetComponent<SpriteRenderer>().sprite = faces[cardPop.number]; //

        cardCount++;
        
    }
}
