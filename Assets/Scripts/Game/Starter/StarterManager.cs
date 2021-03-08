using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterManager : MonoBehaviour
{

    public Starter starter;

    public void Awake()
    {
        starter.gameObject.SetActive(false);
    }

    public void Initialise ()
    {
        starter.gameObject.SetActive(true);
        starter.StartStarter();
    }
}