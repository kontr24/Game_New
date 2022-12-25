using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickScript : MonoBehaviour
{
    //
    //    public void onClick()
    //    {
    //        Debug.Log("клик")
    //    }
    //    bool Pressed = false;
    //    public void onDown()
    //    {
    //        Pressed = true;
    //    }
    //    public void onUp()
    //    {
    //        Pressed = false;
    //    }

    //    void Update()
    //    {
    //        if (Pressed) Debug.Log("Кнопка нажата")
    //    }



    public bool clickedIs = false;

    public void OnСlick()
    {
        
            clickedIs = false;
        

    }
    public void OnStopСlick()
    {
            clickedIs = true;

        
    }

}

