using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class SocketClient : MonoBehaviour
{
    WebSocket ws;

    // Start is called before the first frame update
    void Start()
    {
        ws = new WebSocket("ws://localhost:8000");
        Debug.Log("Socket Client Started.");
        ws.Connect();
        ws.Send("Initializing Connection.");
        ws.OnMessage += (sender, e) =>
        {
            Debug.Log("Message Received From: " + ((WebSocket)sender).Url + ", Data : " + e.Data);
        };
    }

    // Update is called once per frame
    void Update()
    {
        if(ws == null) return;
        if (Input.GetKeyDown(KeyCode.Space))
            ws.Send("Hello World!");
    }
}
