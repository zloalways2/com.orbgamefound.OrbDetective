using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public GameObject ButtPref;
    public GameObject ButtPref2;
    public List<bool> butts;
    public GameObject ButtsPan;
    public float timer;
    public bool timeon;
    public int score;
    public int scoreneed;
    public int lvl;
    public int lvlopen;

    public TextMeshProUGUI timetxt;
    public TextMeshProUGUI scoretxt;

    public GameObject WinPan;
    public GameObject LosePan;
    public TextMeshProUGUI time1txt;
    public TextMeshProUGUI score1txt;
    public TextMeshProUGUI time2txt;
    public TextMeshProUGUI score2txt;

    public GameObject MenuPan;

    public GameObject LvlsPan;
    public List<GameObject> Lvlsb;

    public GameObject SettingsPan;

    public GameObject GamePan;


    public AudioSource a1;
    public AudioSource a2;

    public Toggle sound;
    public Toggle music;

    public GameObject lp;

    public bool set;

    public void Start()
    {
        LoadData();
        Debug.Log(scoreneed);
        Invoke("Sts", 2f);

    }
    void Sts()
    {
        PanOpen(0);
    }
    
    public void NextLvl()
    {
        lvl++;
        GameStart(lvl);
    }

    public void SetSet(bool set1)
    {
        set = set1;
        if(set1)
        {
            timeon = false;
        }
    }

    public void Back()
    {
        if (set)
        {
            timeon = true;
            set = false;
            PanOpen(1);
        }
        else
        {
            PanOpen(0);
        }
    }
    public void RetryLvl()
    {
        GameStart(lvl);
    }

    public void GameStart(int lvl1)
    {
        if(lvl1 <= lvlopen)
        {
            lvl = lvl1;
            PanOpen(1);
            score = 0;
            timer = 60 - 2 * lvl1;
            scoreneed = 50 + 3 * lvl1;

            timeon = true;
            GenerateCircles();
            ResetTable();
        }
    }

    public void PanOpen(int i)
    {
        List<GameObject> list = new List<GameObject>();
        list.Add(MenuPan);
        list.Add(GamePan);
        list.Add(LvlsPan);
        list.Add(SettingsPan);
        list.Add(WinPan);
        list.Add(LosePan);
        list.Add(lp);
        for(int a = 0; a < list.Count; a++)
        {
            if(i == a)
            {
                list[a].gameObject.SetActive(true);
            }
            else
            {
                list[a].gameObject.SetActive(false);
            }
        }
    }

    public void GenerateCircles()
    {
        butts = new List<bool>();
        for (int i = 0; i < 24; i++)
        {
            butts.Add(false);
        }
        List<int> a = new List<int>();
        a.Add(Random.Range(0, 24));
        a.Add(Random.Range(0, 24));
        while (a[0] == a[1])
        {
            a.RemoveAt(1);
            a.Add(Random.Range(0, 24));
        }
        a.Add(Random.Range(0, 24));
        while (a[2] == a[1] || a[0] == a[2])
        {
            a.RemoveAt(2);
            a.Add(Random.Range(0, 24));
        }
        foreach (int i in a)
        {
            butts[i] = true;
        }
    }
    public void ResetTable()
    {
        for (int i = 0; i < ButtsPan.transform.childCount; i++)
        {
            Destroy(ButtsPan.transform.GetChild(i).gameObject);
        }
        for(int i = 0;i < 24; i++)
        {
            if (butts[i])
            {
                GameObject a = Instantiate(ButtPref, new Vector2(0,0), Quaternion.identity,ButtsPan.transform);
                a.GetComponent<Butts>().real = true;
                a.GetComponent<Butts>().game = this;
            }
            else
            {
                GameObject a = Instantiate(ButtPref2, new Vector2(0, 0), Quaternion.identity, ButtsPan.transform);
                a.GetComponent<Butts>().real = false;
                a.GetComponent<Butts>().game = this;
            }
        }
    }
    public void Lose()
    {
        timeon = false;

        PanOpen(5);
    } 
    public void Win()
    {
        timeon = false;
        if(lvl == lvlopen)
        {
            lvlopen++;
        }
        PanOpen(4);
    }
    public void Button(bool a)
    {
        if (a)
        {
            score++;
            a1.Play();
            if (score == scoreneed)
            {
                Win();
            }
        }
        else
        {
            a2.Play();
            if (score > 0)
            {
                score--;
            }
        }
        GenerateCircles();
        ResetTable();
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("lvl", lvlopen);
        if (sound.isOn)
        {
            PlayerPrefs.SetInt("s1", 1);
        }
        else
        {
            PlayerPrefs.SetInt("s1", 0);
        }
        if (music.isOn)
        {
            PlayerPrefs.SetInt("s2", 1);
        }
        else
        {
            PlayerPrefs.SetInt("s2", 0);
        }

    }

    public void LoadData()
    {
        lvlopen = PlayerPrefs.GetInt("lvl");
        if (PlayerPrefs.HasKey("s1"))
        {
            if(PlayerPrefs.GetInt("s1") == 1)
            {
                sound.isOn = true;
            }
            else
            {
                sound.isOn = false;
            }
            if (PlayerPrefs.GetInt("s2") == 1)
            {
                music.isOn = true;
            }
            else
            {
                music.isOn = false;
            }
        }
    }

    public void Okey()
    {
        timeon = false;
    }

    public void AA()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (timeon)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                Lose();

            }
        }
        SaveData();


        timetxt.text = "Timer:\n" + Mathf.FloorToInt(timer) + "s";
        scoretxt.text = "Score:\n" + score + "/" + scoreneed;
        time1txt.text = "Timer:\n" + Mathf.FloorToInt(timer) + "s";
        score1txt.text = "Score: " + score + "/" + scoreneed;
        time2txt.text = "Timer:\n" + Mathf.FloorToInt(timer) + "s";
        score2txt.text = "Score: " + score + "/" + scoreneed;

        for (int i = 0; i < Lvlsb.Count; i++)
        {
            Color c = Lvlsb[i].GetComponent<Image>().color;
            if(lvlopen >= i)
            {
                c.a = 1f;
            }
            else
            {
                c.a = 0.2f;
            }

            Lvlsb[i].GetComponent<Image>().color = c;
        }

        if (!music.isOn)
        {
            GetComponent<AudioSource>().volume = 0f;
        }
        else
        {
            GetComponent<AudioSource>().volume = 0.2f;
        }

        if (!sound.isOn)
        {
            a1.volume = 0f;
            a2.volume = 0f;
        }
        else
        {
            a1.volume = 1f;
            a2.volume = 1f;
        }
    }

}
