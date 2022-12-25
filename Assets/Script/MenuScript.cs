using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameObject levelChanger;
    public GameObject exitPanel;
    public GameObject bg;
    public Image[] cars;
    public GameObject garagePanel;
    public Button[] bttns;
    public Text[] carsText;
    public string[] levels;
    //private string[] tuning, tuning2, tuning3;
    //private char del = '9';
    //public Button[] updateBttns;

    //void Awake()
    //{
    //    if (PlayerPrefs.HasKey("t1"))
    //    {
    //        PlayerPrefs.SetString("t1", "09090");
    //        PlayerPrefs.Save();
    //    }
    //    tuning = PlayerPrefs.GetString("t1").Split(del);

    //    for (int i = 0; i< tuning.Length; i++)
    //    {
    //        if (int.Parse(tuning[i]) == 3)
    //        {
    //            updateBttns[i].interactable = false;

    //        }
    //    }
    //}

   void Start()
    {
        //PlayerPrefs.DeleteAll();
        cars[PlayerPrefs.GetInt("c")].color = Color.white;
        if (PlayerPrefs.GetInt(levels[0]) == 3)
        {
            bttns[0].interactable = true;
            carsText[0].gameObject.SetActive(false);
        }
            
    }



    void Update()
    {
        if (garagePanel.activeSelf == true && Input.GetKeyDown(KeyCode.Escape))
        {
            garagePanel.SetActive(false);
        }
          else  if (levelChanger.activeSelf == true && Input.GetKeyDown(KeyCode.Escape))
            {
                levelChanger.SetActive(false);
            }
            else if (exitPanel.activeSelf == false && Input.GetKeyDown(KeyCode.Escape))
            {
                exitPanel.SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                exitPanel.SetActive(false);
            }
        
    }

        public void OnClickStart()
        {
            levelChanger.SetActive(true);
            //SceneManager.LoadScene(0);
        }

        public void OnClickExit()
        {
            Application.Quit();
        }

    public void levelBttns(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void carChanger(int car)
    {
        PlayerPrefs.SetInt("c", car);
        PlayerPrefs.Save();
        switch (car)
        {
            case 0:
                cars[car].color = Color.white;
                cars[car + 1].color = Color.black;
                break;
            case 1:
                cars[car].color = Color.white;
                cars[car - 1].color = Color.black;
                break;

        }
    }



    //public void carTuning(int carPart)
    //{
    //    int updateState = int.Parse(tuning[carPart]) + 1;
    //    if (updateState <= 3)
    //    {
    //        tuning[carPart] = updateState.ToString();
    //        PlayerPrefs.SetString("t1", tuning[0] + "9"+ tuning[1]+"9"+tuning[2]+"9");
    //        PlayerPrefs.Save();
    //    }


    //    if (int.Parse(tuning[carPart]) == 3)
    //    {
    //        updateBttns[carPart].interactable = false;

    //    }
    //    for (int i = 0; i < tuning.Length; i++)
    //    {
    //        print(tuning[i]);
    //    }
    //}
}
