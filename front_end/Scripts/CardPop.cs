using UnityEngine;
using UnityEngine.UI;// for ui changing
using System.Collections;
using System.Collections.Generic; //needed for list


public class CardPop : MonoBehaviour {

    //In inspector, drag sprite images into each variable
    public Sprite cardback; // default image, is loaded and set in inspector
    public Sprite[] faces; //Images are dragged into inspector
    public int number;

    public List<int> cards;


    Image myImageComponent;

    //Need to be able to reference the hand image objects and assign images to them. 

    void Start()
    {

        myImageComponent = GetComponent<Image>(); //This could be canvasrenderer, image, etc. Gets a part of an object that you can see in inspector
        
    }

    //On button press choose a random card and display it. Effectively a random card is scanned and then displayed. The button acts as the scan.
    public void cardPopup () { //must be public to be accessed outside. When button is pressed, activate this function

        number = Random.Range(0, 9); // generate random number between 0 and the size of the deck (-1) to choose a random card
        myImageComponent.sprite = faces[number];

        cards.Add(number); //Add index to list to save cards
    }
}
