using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class SocketClient : MonoBehaviour
{
    Transitions script;
    WebSocket ws;

    int choice = 0;
    bool updateNeeded = false;

    // Start is called before the first frame update
    void Start()
    {   
        script = GameObject.FindGameObjectWithTag("Container").GetComponent<Transitions>();
        ws = new WebSocket("ws://localhost:8000");
        Debug.Log("Socket Client Started");
        ws.Connect();
        ws.Send("Initializing Connection");
        ws.OnMessage += (sender, e) =>
        {
            ReceiveMessage(e.Data);
        };
    }

    void SendMessage(string message) {
        Debug.Log("Message Sent");
        ws.Send(message);
    }

    void ReceiveMessage(string message) {
        choice = int.Parse(message) - 1;
        Debug.Log("Message received: " + choice);
        updateNeeded = true;
        SendMessage("Updated");
    }

    // Update is called once per frame
    void Update()
    {
        if(ws == null) {
            ws = new WebSocket("ws://ce07-129-236-174-83.ngrok.io");
            ws.Connect();
            return;
        }
        if (updateNeeded)
            script.MakeTransition(choice);
            updateNeeded = false;
    }
}
