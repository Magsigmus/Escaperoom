using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumPadManeger : MonoBehaviour
{
   
    public string trueCode;
    List<string> code = new List<string>();
    public TextMeshProUGUI codeDesplay;

    void Start()
    {
        
    }

    public void ButtonInput(string i)
    {
        if (i == "C")
        {
            code = new List<string>();
            codeDesplay.text = "";
        }
        else if (i == "E")
        {
            
        }
        else if (code.Count < 4)
        {
            code.Add(i);
            codeDesplay.text = string.Join(' ', code);
        }
    }


    
    
}
