using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class StarsDisplay : MonoBehaviour
{
    public Image[] stars;
    private Button[] bttns;
    private MenuScript ms;
    public int levelChanger = 0;
    public string keyName = "S";

   void Awake()
    {
        stars = GetComponentsInChildren<Image>();
        ms = Camera.main.GetComponent<MenuScript>();
        bttns = ms.levelChanger.GetComponentsInChildren<Button>();
    }
    void Start()
    {
        
        if (PlayerPrefs.GetInt(keyName)==3)
        {
            int unlockLevel = levelChanger + 1;
            bttns[unlockLevel].interactable = true;
            stars[1].color = new Color(stars[1].color.r, stars[1].color.g, stars[1].color.b, 255);
            stars[2].color = new Color(stars[2].color.r, stars[2].color.g, stars[2].color.b, 255);
            stars[3].color = new Color(stars[3].color.r, stars[3].color.g, stars[3].color.b, 255);
            //stars[0].color = new Color(stars[0].color.r, stars[0].color.g, stars[0].color.b, 255);
            //stars[1].color = new Color(stars[1].color.r, stars[1].color.g, stars[1].color.b, 255);
            //stars[2].color = new Color(stars[2].color.r, stars[2].color.g, stars[2].color.b, 255);
        }
        else if (PlayerPrefs.GetInt(keyName) == 2)
        {
            int unlockLevel = levelChanger + 1;
            bttns[unlockLevel].interactable = true;

            stars[1].color = new Color(stars[1].color.r, stars[1].color.g, stars[1].color.b, 255);
            stars[2].color = new Color(stars[2].color.r, stars[2].color.g, stars[2].color.b, 255);

        }
        else if (PlayerPrefs.GetInt(keyName) == 1)
        {
            //int unlockLevel = levelChanger + 1;
            //bttns[unlockLevel].interactable = true; // новый уровень

            stars[1].color = new Color(stars[1].color.r, stars[1].color.g, stars[1].color.b, 255);
           
        }
    }

}
