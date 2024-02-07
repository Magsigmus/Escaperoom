using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class NumPadManeger : MonoBehaviour
{
   
    public string trueCode;
    public List<string> code = new List<string>();
    public TextMeshProUGUI codeDesplay;
    public UnityEvent Correct;
    public bool correctCode = false;
    public bool powered = false;
    public MeshRenderer buttonText;
    public Material whiteMaterial, grayMaterial;
    public AudioSource audioS;
    public AudioClip keyPress;
    public AudioClip correctAudio;
    public AudioClip wrongeAudio;
    public AudioClip noPower;

    public void ButtonInput(string i)
    {
        if(correctCode || !powered)
        {
            audioS.clip = noPower;
            audioS.pitch = 1f;
            audioS.Play();
            return;
        }

        if (i == "C")
        {
            Debug.Log("Triggered");
            code = new List<string>();
            codeDesplay.text = "";
            audioS.clip = keyPress;
            audioS.pitch = 0.1f;
            audioS.Play();
        }
        else if (i == "E")
        {
            if (trueCode == string.Join("", code))
            {
                codeDesplay.color = new Color (0, 255, 0, 255);
                correctCode = true;
                Correct.Invoke();
                audioS.clip = correctAudio;
                audioS.pitch = 1f;
                audioS.Play();
            }
            else
            {
                code = new List<string>();
                codeDesplay.text = "";
                Debug.Log("Triggered");
                audioS.clip = wrongeAudio;
                audioS.pitch = 1f;
                audioS.Play();
            }

        }
        else if (code.Count < 4)
        {
            code.Add(i);
            codeDesplay.text = string.Join(' ', code);
            audioS.clip = keyPress;
            audioS.pitch = 1 + float.Parse(i);
            audioS.Play();
        }
    }

    public void Power()
    {
        powered = true;
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
