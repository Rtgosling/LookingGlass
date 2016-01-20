using UnityEngine;
using System.Collections;
using WebSocketSharp;
using System.Collections.Generic;
using System.Linq; //for ToList
using System; //for parse , string to int conversion

public class ClientSocket : MonoBehaviour
{
    
    WebSocket ws = new WebSocket("ws://localhost:8000/");
    string lastMessage = null;
    string messageComplete = null;
    int functionSent = 1; //Lock so function is only sent once. 0 for not sent (unclocked), 1 for sent (locked). will only send when == 0


    public List<string> commands; //list to store commands from server

    public CardPop cardPopScript;
    public ShowHand showHandScript;
    public PlayCard playCardScript;

    // Use this for initialization
    void Start()
    {
        ws.Connect(); //connect to the specified websocket address


    }

    void Update()
    {
        

        ws.OnMessage += (sender, e) =>   //on every update receive message and print
        {
            if (e.Data != lastMessage) //Prevents duplicate messages- PROBLEM MEANS SAME COMMAND CAN'T BE INTENTIONALLY SENT EITHER- TIMER?
            {
                lastMessage = e.Data;
                messageComplete = e.Data;
                functionSent = 0; //Unlock functionSent so it may be called

                Debug.Log(e.Data);
                 
                commands = e.Data.Split(',').ToList<string>(); //split the incoming data into list of commands

        
                //Putting more here causes websocket to disconnect?

            }


        };

        if (functionSent == 0)
        {
            functionSent = 1; //lock function
            Debug.Log("Sending function");
            callFunction(messageComplete);
            
        }

        

    }

    

    public void send()
    {
       // WebSocket ws = new WebSocket("ws://localhost:8000/");
      //  ws.OnMessage += (sender, e) =>
           // Debug.Log("Laputa says: " + e.Data);
       // ws.Connect();
      //  ws.Send("we are connected yay");
    }

    private void callFunction(string messageComplete)
    {
        Debug.Log("Commands[1] is: " + commands[1]);
                                   
             if (commands[1] == "s") //if player is scanning card
             {
                Debug.Log("Scan command recognised");
                int scannedCard;    

                Int32.TryParse(commands[2], out scannedCard); //convert the next element from string to int

                cardPopScript.cardPopup(scannedCard);
                showHandScript.ShowCards();

             }

             else if (commands[1] == "h") //if player is interacting with hand
             {
                if(commands[3] == "p") //if playing card
                {
                    Debug.Log("Play command recognised");
                    int cardToPlay;
                    Int32.TryParse(commands[2], out cardToPlay); //convert the next element from string to int 
                    playCardScript.moveCard(cardToPlay); //Move selected card into playing field
                    cardPopScript.toggleVisOff();    //Make deck scan image invisible
                }
             }

             else if (commands[1] == "t") //if player is interacting with tiles in the playing area
             {
                if (commands[3] == "r") //if playing card
                {
                    Debug.Log("Remove command recognised");
                    int cardToRemove;
                    Int32.TryParse(commands[2], out cardToRemove); //convert the next element from string to int 
                    playCardScript.removeCard(cardToRemove); //Move selected card into playing field
                    cardPopScript.toggleVisOff();    //Make deck scan image invisible
                }

                else if (commands[3] == "f") //if playing card
                {
                    Debug.Log("Floop command recognised");
                    int cardToFloop;
                    Int32.TryParse(commands[2], out cardToFloop); //convert the next element from string to int 
                    playCardScript.floopCard(cardToFloop); //Move selected card into playing field
                    cardPopScript.toggleVisOff();    //Make deck scan image invisible
                }
        }

             else
             {
                 Debug.Log(messageComplete + " not a recognised command");
             }
    }
}