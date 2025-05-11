using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Level : MonoBehaviour
{
    int levelNO;
    public Button[] allbtn;
    int maxlevel = 0;
    public Sprite tick;

    // Start is called before the first frame update
    void Start()
    {
        levelNO = PlayerPrefs.GetInt("levelNo",1);
        maxlevel = PlayerPrefs.GetInt("maxlevel", 0);
        for (int i=0; i<=maxlevel;i++)
        {
            allbtn[i].interactable = true;
            allbtn[i].GetComponentInChildren<Text>().text = (i + 1).ToString();
            if (i < levelNO)
            {
                if (PlayerPrefs.HasKey("skip" + (i + 1)))
                {
                    allbtn[i].GetComponent<Image>().sprite = null;
                }
                else
                {
                    allbtn[i].GetComponent<Image>().sprite = tick;
                }
            }
            else
            {
                allbtn[i].GetComponent<Image>().sprite = null;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void levelselect(int n)
    {
        PlayerPrefs.SetInt("levelNo",n);
        SceneManager.LoadScene("play");
    }
}
