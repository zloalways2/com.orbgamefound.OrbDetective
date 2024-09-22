using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Butts : MonoBehaviour
{
    public Game game;
    public bool real;


    public void OnButtonClick()
    {
        game.Button(real);
    }
}
