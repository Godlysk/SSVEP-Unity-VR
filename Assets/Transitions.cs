using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;

public class Transitions : MonoBehaviour
{

    string sentence = "";
    GameObject[] labels;
    GameObject output;

    int phase = 0;

    string[] main = new string[] {"Predictive\nPredictive\nPredictive", ". , ?\n! & :\n; - ←", "A B C\nD E F\nG H I", "J K L\nM N O\nP Q R", "S T U\nV W X\nY Z _"};
    string[] predictive = new string[] {"Other\nPredictions", "Go\nBack", "First", "Second", "Third"};
    string[] predictions = new string[] {"First", "Second", "Third"};
    public string[] layout = new string[] {"Predictive\nPredictive\nPredictive", "Go\nBack", "Option 1", "Option 2", "Option 3"};

    // Start is called before the first frame update
    void Start()
    {
        layout = main;
        labels = new GameObject[5];
        GetLabels();
        UpdateLayout();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) MakeTransition(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2)) MakeTransition(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3)) MakeTransition(2);
        else if (Input.GetKeyDown(KeyCode.Alpha4)) MakeTransition(3);
        else if (Input.GetKeyDown(KeyCode.Alpha5)) MakeTransition(4);
    }

    public void GetLabels() {
        output = GameObject.FindGameObjectWithTag("Textbox");
        for (int i=0; i<labels.Length; i++)
            labels[i] = GameObject.FindGameObjectWithTag("Label" + i);
    }

    public void MakeTransition(int choice) {
        switch(choice) {
            case 0:
                layout = predictive;
                phase = 2;
                break;
            case 1:
                if (phase == 0) {
                    SetLayout(choice, '\n');
                    phase++;
                } else {
                    layout = main;
                    phase = 0;
                }
                break;
            case 2:
            case 3:
            case 4:
                if (phase == 0) {
                    SetLayout(choice, '\n');
                    phase++;
                } else if (phase == 1) {
                    SetLayout(choice, ' ');
                    phase++;
                } else if (phase == 2) {
                    OutputValue(layout[choice]);
                    layout = main;
                    phase = 0;
                }
                break;
            default:
                break;
        }
        UpdateLayout();
    }

    public void SetLayout(int choice, char delimiter) {
        string[] values = layout[choice].Split(delimiter);
        layout = new string[] {String.Join('\n', predictions), "Go\nBack", values[0], values[1], values[2]};
    }

    public void UpdateLayout() {
        for (int i=0; i<layout.Length; i++) {
            labels[i].GetComponent<TextMesh>().text = layout[i];
        }
    }

    public void OutputValue(string value) {
        if (value.Equals("←")) {
            if (sentence.Length > 0)
                sentence = sentence.Remove(sentence.Length - 1);
        } else if (value.Equals("_")) sentence += " ";
        else sentence += value;
        output.GetComponent<TextMesh>().text = sentence;
    }

    void SetPredictive(string[] modelOutput) {
        predictions = modelOutput;
        main[0] = String.Join('\n', predictions);
    }
}
