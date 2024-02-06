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
    public bool correctCode = false;
    public bool powered = false;
    public MeshRenderer buttonText;
    public Material whiteMaterial, grayMaterial;
    
    public void ButtonInput(string i)
    {
        if(correctCode || !powered) { return; }

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
                correctCode = true;
                Correct.Invoke();
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

    public void Power()
    {
        powered = true;
        codeDesplay.text = "";
        buttonText.material = whiteMaterial;
    }

    public void Unpower()
    {
        powered = false;
        code = new List<string>();
        codeDesplay.text = "";
        buttonText.material = grayMaterial;
    }
}
