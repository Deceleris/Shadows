using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    void Function ()
    {

        int a = Square(2);
        int b = Square(3);
        int c = Square(4);

        int Square (int v)
        {
            return v * v;
        }
    }

}