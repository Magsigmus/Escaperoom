using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class NumPadManeger : MonoBehaviour
{
   
    public string trueCode;
    List<string> code = new List<string>();
    public TextMeshProUGUI codeDesplay;
    public UnityEvent Correct;

    

    public void ButtonInput(string i)
    {
        if (i == "C")
        {
            code = new List<string>();
            codeDesplay.text = "";
        }
        else if (i == "E")
        {
            if (trueCode == string.Join("", code))
            {
                codeDesplay.color = new Color (0, 255, 0, 255);
                Correct.Invoke();
                this.enabled = false;
            }
            else
            {
                code = new List<string>();
                codeDesplay.text = "";
            }

        }
        else if (code.Count < 4)
        {
            code.Add(i);
            codeDesplay.text = string.Join(' ', code);
        }
    }


    
    
}
