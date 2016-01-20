using UnityEngine;
using System.Collections;
using System.Collections.Generic; //needed for list

public class PlayCard : MonoBehaviour {

    public GameObject getStuff;
 //   public CardPop cardPopScript; //Drag object containing cardpop into inspector (scannedcard). This allows easy access to variables within cardpop

    public CardManager cardManager; //global array and variable access

    //4 play position vectors
    Vector3 Tile1;
    Vector3 Tile2;
    Vector3 Tile3;
    Vector3 Tile4;




    // Use this for initialization
    void Start () {
        //Define the 4 positions for the tiles
        Tile1 = GameObject.Find("Tile1").transform.position;
        Tile2 = GameObject.Find("Tile2").transform.position;
        Tile3 = GameObject.Find("Tile3").transform.position;
        Tile4 = GameObject.Find("Tile4").transform.position;
    }

    public void moveCard(int cardToPlay)
    {

        // int randMax = cardManager.cardsHand.Count; //access the cards in the hand to find how many there are
        //  Debug.Log(randMax);
        //  int rand = Random.Range(0, randMax-1); // generate random number between 0 and the size of the hand to choose a random card for testing
       
        /* //Not used
        // int cardPos = cardManager.cardsHand[rand];

        // int cRand = rand + 1; //Make value correspond with Hand object values (they start on 1)
        */


        int rand = cardToPlay;

        string handNumber = "Hand " + rand; //Concatenate to create string with correct hand name
        //THE PROBLEM IS AFTER MOVING A CARD, THE LIST RESIZES, BUT THE HAND NAME STAYS THE SAME, SO IT WILL TRY TO ACCESS HAND 1 FOR THE NEW CARD BUT THAT#S ALREADY BEEN USED AND IS IN PLAY
        //SOLUTION IS PERHAPS TO MAKE THE INT CARDHAND LIST AN GAMEOBJECT LIST AND JUST MOVE ITEMS BETWEEN THEM
        GameObject currentCard; //Create a game object for the current card being manipulated
        currentCard = GameObject.Find(handNumber); //Find Handobject and assign to currentCard

        currentCard.name = "Play " + (cardManager.cardsPlay.Count); // rename the hand card to a play card, as it is moving from hand to play area. THIS PREVENTS THE CARD IN PLAY BEING MOVED IF THE SAME ELEMENT IS CALLED AGAIN.

        if (cardManager.cardsPlayI[0] == 0) //Check to see if anything is in the first tile position
            {
                currentCard.transform.position = Vector3.Lerp(currentCard.transform.position, Tile1, 1); //if not, move card to first position
                cardManager.cardsPlayI[0] = 1; //change array to indicate first position is filled
            }
            else if (cardManager.cardsPlayI[1] == 0)
            {
                currentCard.transform.position = Vector3.Lerp(currentCard.transform.position, Tile2, 1);
                cardManager.cardsPlayI[1] = 1;
            }
            else if (cardManager.cardsPlayI[2] == 0)
            {
                currentCard.transform.position = Vector3.Lerp(currentCard.transform.position, Tile3, 1);
                cardManager.cardsPlayI[2] = 1;
            }
            else if (cardManager.cardsPlayI[3] == 0)
            {
                currentCard.transform.position = Vector3.Lerp(currentCard.transform.position, Tile4, 1);
                cardManager.cardsPlayI[3] = 1;
            }

            cardManager.cardsPlay.Add(currentCard); //Add the current card to the playing area array
            cardManager.cardsHand.Remove(cardManager.cardsHand[rand]); //remove card from hand array - REMOVING WRONG CARD sometimes - is it still doing this?

    }
    


    public void removeCard(int cardToRemove)//eventually will pass in a number from clientsocket that will replace "rand"
    {
        // int randMax = cardManager.cardsPlay.Count; //Choose random number up to number of cards in play

        //  int rand = Random.Range(0, randMax); //random number up to number of cards in play
        //Debug.Log(rand);

        int rand = cardToRemove;

       
        GameObject currentCard; //create gameobject for the chosen card
        currentCard = cardManager.cardsPlay[rand]; //assign the random card in play to the game object
        cardManager.cardsPlay.Remove(currentCard); //remove the chosen card from cards in play array

        //WHAT POSITION WAS THE CARD IN?
        cardManager.cardsPlayI[rand] = 0; //MUST ASSIGN  POSITION OF THE TILE TO ZERO SO IT CAN BE PLAYED ON AGAIN

        Destroy(currentCard); //delete/remove the chosen card
        

    }

    public void floopCard(int cardToFloop)
    {
        // int randMax = cardManager.cardsPlay.Count; //Choose random number up to number of cards in play

        //  int rand = Random.Range(0, randMax); //random number up to number of cards in play
        //Debug.Log(rand);

        int rand = cardToFloop;

        GameObject currentCard; //create gameobject for the chosen card
        currentCard = cardManager.cardsPlay[rand]; //assign the random card in play to the game object

      
        if (currentCard.transform.rotation.eulerAngles.z != 270)//check is card is already flooped (270 degrees on z axis)
        {
            currentCard.transform.Rotate(0, 0, -90); //if not flooped, turn -90 degrees on z axis
        }

        else if (currentCard.transform.rotation.eulerAngles.z == 270)
        {
            currentCard.transform.Rotate(0, 0, 90); //if already flooped, turn it back postive 90 degrees on z axis
        }

    }
}

    


/*
        int randMax = cardManager.cardsHand.Count; //access the cards in the hand to find how many there are

        int rand = Random.Range(0, randMax); // generate random number between 0 and the size of the hand to choose a random card for testing





        int chosenCard = cardManager.cardsHand[cardManager.cardsHand[rand]];

        // int cRand = rand + 1; //Make value correspond with Hand object values (they start on 1)


        string handNumber = "Hand " + (chosenCard + 1); //Concatenate to create string with correct hand name
*/