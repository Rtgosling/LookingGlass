using UnityEngine;
using UnityEngine.UI;// for ui changing
using System.Collections;
using System.Collections.Generic; //needed for list


public class CardPop : MonoBehaviour
{

    public Sprite cardback; // default image, is loaded and set in inspector
    public int number;

    public CardManager cardManager; // global array and variable access

    Image myImageComponent;


    void Start()
    {

        myImageComponent = GetComponent<Image>(); //This could be canvasrenderer, image, etc. Gets a part of an object that you can see in inspector
        myImageComponent.GetComponent<Image>().enabled = false; //Make centre image invisible until a card is scanned
    }

    
    public void cardPopup(int scannedCard) //On button press choose a random card and display it. Effectively a random card is scanned and then displayed. The button acts as the scan.
    { //must be public to be accessed outside. When button is pressed, activate this function
        toggleVisOn(); //Make deck scan image visible every time a card is scanned

        //  number = Random.Range(0, 9); // generate random number between 0 and the size of the deck (-1) to choose a random card
        number = scannedCard;
        myImageComponent.sprite = cardManager.faces[number];

        cardManager.cardsHand.Add(number); //Add index to list to save cards
    }



    public void toggleVisOff() //Make deck scan image invisible
    {
        myImageComponent.GetComponent<Image>().enabled = false;
    }

    public void toggleVisOn() //Make deck scan image visible
    {
        myImageComponent.GetComponent<Image>().enabled = true;
    }

    public void toggleVis()
    {
        if (myImageComponent.GetComponent<Image>().enabled == true)
        { //Check to see if the centre image is visible
            myImageComponent.GetComponent<Image>().enabled = false; //make deck scan image invisible
        }
        else
        { 
            myImageComponent.GetComponent<Image>().enabled = true; //Make deck scan image visible
        }
    }

}