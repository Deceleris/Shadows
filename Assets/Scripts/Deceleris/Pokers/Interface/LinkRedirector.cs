using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkRedirector : MonoBehaviour
{
    public void socialNetWork(string link)
    {
        Application.OpenURL(link);
    }
}
