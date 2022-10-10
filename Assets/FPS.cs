using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{

    void Start() {
        // VR requires a higher FPS than 60 so this is subject to change
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        float fps = 1.0f / Time.deltaTime;
        this.gameObject.GetComponent<Text>().text = "FPS: " + fps; 
    }
}
