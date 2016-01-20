using UnityEngine;
using System.Collections;
using System.Collections.Generic; //needed for list

public class CardManager : MonoBehaviour {


    public List<GameObject> cardsPlay; //array containing cards in the playing field#

    //initialise with value to check for, when removing set value back to that

    public List<int> cardsPlayI = new List<int>()
	{
	    0,
	    0,
	    0,
        0
	};



    public List<int> cardsHand; //cards in the players hand

    public Sprite[] faces; //Possible card faces in players deck - Images are dragged into inspector

    void Start () {
        Screen.SetResolution(758, 425, false); //Set the default windowed resolution for the game
    }
    
}
