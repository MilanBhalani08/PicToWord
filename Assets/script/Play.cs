using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Play : MonoBehaviour
{
    int levelno=0,maxlevel=0;
   
    public Image img1, img2;
    public Sprite[] p1, p2;
    string[] mixans = {"FZONOMTUBJAKLL","SHJTJKAADSR","GVKFOSNVANKLVD","CQPLSCDGBAVGTY"};
    string[] trueans = {"FOOTBALL","STAR","GOAL","PLAY"};
    string curans = "", curmixans = "";
    public GameObject btnprefeb, downHolder,uperHolder,ansprefeb;
    string[] hintshow = {""};
    int[] btnPos = new int[15];
    int score = 0;
    public Text p;
    public Button hintbtn;
    public Text hintbox;

    // Start is called before the first frame update
    void Start()
    {
        score = PlayerPrefs.GetInt("score", 0);
        maxlevel = PlayerPrefs.GetInt("maxlevel", 0);
        levelno = PlayerPrefs.GetInt("levelno",1);
        img1.sprite = p1[levelno - 1];
        img2.sprite = p2[levelno - 1];
        curans = trueans[levelno - 1];
        curmixans = mixans[levelno - 1];

        for(int i=0;i<curmixans.Length;i++)
        {
            string S = curmixans[i].ToString();  
            int tempNo = i;
            GameObject store = Instantiate(btnprefeb, downHolder.transform);
            store.GetComponentInChildren<Text>().text=curmixans[i].ToString();
            store.tag = "downBTN";
            store.GetComponent<Button>().onClick.AddListener(() => downBtnclick(S, tempNo));
            
            
        }
        for (int i = 0; i < curans.Length; i++)
        {
           
            int tempNo = i;
            GameObject store2 = Instantiate(btnprefeb, uperHolder.transform);
            store2.tag = "uperBTN";
            store2.GetComponent<Button>().onClick.AddListener(() =>uperBtnclick (tempNo));
            
        }
        p.text = "score-> "+score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void downBtnclick(string s,int downNo)
    {
      
        GameObject[] uperBTNS = GameObject.FindGameObjectsWithTag("uperBTN");
        GameObject[] downBTNS = GameObject.FindGameObjectsWithTag("downBTN");
        string userans = "";
        for (int i = 0; i < uperBTNS.Length; i++)
        {
           
            if (uperBTNS[i].GetComponentInChildren<Text>().text ==   "")
            {

                btnPos[i] = downNo;
                uperBTNS[i].GetComponentInChildren<Text>().text = s;
                downBTNS[downNo].GetComponentInChildren<Text>().text = "";
                downBTNS[downNo].GetComponent<Button>().interactable = false;

                break;
            }
        }
        for(int i = 0; i < uperBTNS.Length; i++)
        {
            userans += uperBTNS[i].GetComponentInChildren<Text>().text;
            if (curans.Length == userans.Length)
            {
                if (curans == userans)
                {
                    PlayerPrefs.DeleteKey("skip" + levelno);
                    SceneManager.LoadScene("winner");
                    PlayerPrefs.SetInt("levelno",levelno+1);
                    if(maxlevel<levelno+1)
                    {
                        PlayerPrefs.SetInt("maxlevel",levelno);
                        PlayerPrefs.SetInt("score", score + 10);               
                    }
                }
                else
                {
                    SceneManager.LoadScene("retry");
                }
            }
        }
    }
    void uperBtnclick(int uperNo)
    { 
        GameObject[] uperBTNS = GameObject.FindGameObjectsWithTag("uperBTN");
        GameObject[] downBTNS = GameObject.FindGameObjectsWithTag("downBTN");
        print(uperBTNS.Length);
        int downNO = btnPos[uperNo];
        downBTNS[downNO].GetComponentInChildren<Text>().text = uperBTNS[uperNo].GetComponentInChildren<Text>().text;
        downBTNS[downNO].GetComponent<Button>().interactable=true;
        uperBTNS[uperNo].GetComponentInChildren<Text>().text = "";
    }
    public void hint()
    {
        if (score >= 20)
        {

            hintbox.text = trueans[levelno - 1];
            PlayerPrefs.SetInt("score",score-20);
            
        }
        else
        {
            hintbox.text = "your score is low";
        }
        p.text = "score -> " + score;
    }
    public void skip()
    {
        PlayerPrefs.SetInt("skip" + levelno, levelno);
        PlayerPrefs.SetInt("levelno", levelno + 1);
        SceneManager.LoadScene("play");
    }
}
