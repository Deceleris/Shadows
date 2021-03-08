using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBarValueReset : MonoBehaviour
{

    public void OnEnable()
    {
        GetComponent<Scrollbar>().value = 1;
    }

}