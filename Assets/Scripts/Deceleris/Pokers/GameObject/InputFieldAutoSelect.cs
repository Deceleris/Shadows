using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldAutoSelect : MonoBehaviour
{

    public void OnEnable()
    {
        GetComponent<InputField>().Select();
        GetComponent<InputField>().ActivateInputField();
    }

}