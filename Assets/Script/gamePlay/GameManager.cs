using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

enum JenisPilihan
{
    BidangRuang,Huruf
}
public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class setengahjawabanSprite
    {
        public GameObject SetengahJawaban;
        public List<Sprite> SetengahJawabSprite;
    }
    [System.Serializable]
    public class JenisBidangRuang
    {
        public GameObject CharacterPrefabs;//prefabs character yang ditengah
        public GameObject JawabanPrefab;//prefab untuk jawaban
        //public GameObject SetengahJawaban;

        public List<Transform> Posisijawaban;//poisis transform kiribawah, kanan bawah, kiriatas dan kanan atas
        public List<Sprite> CharacterSprite;
        public List<Sprite> JawabanSprite;
        public List<setengahjawabanSprite> SetengahJawabSprite;

        //public List<Sprite> SetengahJawabSprite;
    }
    

    [SerializeField] JenisPilihan jenis = JenisPilihan.Huruf;
    public SoundManager sfx;

    [Header("UI")]
    public Text ResultText;
    public GameObject PanelPause;
    public GameObject PanelJawabBenar;
    public GameObject PanelJawabSalah;
    public GameObject PResult;
    public Text TotalSoalTxt;
    public Text BenarTxt;
    public Text SalahTxt;
    public Text HighscoreTxt;

    public List<JenisBidangRuang> JumlahBidangRuang;
    public int randomBidangRuang=0;

    [Header("Bagian A")]
    
    //public List<GameObject> CharacterPrefabsA;
    //public List<GameObject> JawabanPrefab;
   

    [Header("Posisi")]
    [SerializeField]private Transform playerPosition;
    /* untuk posisi jawaban
     * 0=posisi kanan
     * 1=posisi kiri
     */
    //public List<Transform> PosisijawabanBagA;
    public List<Transform> Endposisi;

    [SerializeField] List<bool> terisi;

    [SerializeField] PlayerMovement playerscript;
    private int randomA;
    public int RandomJawaban;//jawaban akan dirandom kanan atau kiri
    public int randomjenissetJawaban;
    public int randomSetJawaban;
    public int alffabetId;//id alfabet

    public bool pause=false;
    public bool playing = false;

    int idcondition;//menentukan kondisi benar atau salah

    string _scene;

    public float JSoalBenar = 0;//skor benar

    public float skor;
    public float JSSoal;//jumlah seluruh soal
    public float JSoalSalah=0;//skor salah

    StorageScript storage;

    private void Awake()
    {
        storage = FindObjectOfType<StorageScript>();
        storage.Load();

        RandomJawaban = Random.RandomRange(0, 4);
        for (int i = 0; i < terisi.Count; i++)
        {
            terisi[i] = false;
        }
    }

    void Start()
    {
       
        StartCoroutine(WaitingTIme(0.5f));
        sfx = FindObjectOfType<SoundManager>();

        PanelPause.SetActive(false);
        Time.timeScale = 1;
        InstanceObj();
        
    }

    public void InstanceObj()//memunculkan objek setiap mulai permainan atau next question
    {
        foreach (var jenis in JumlahBidangRuang)
        {
            randomjenissetJawaban = Random.RandomRange(0, jenis.SetengahJawabSprite.Count);
        }
        RandomJawaban = Random.RandomRange(0, 4);
        randomSetJawaban = Random.RandomRange(0, 4);
        while (randomSetJawaban == RandomJawaban)
        {
            randomSetJawaban = Random.RandomRange(0, 4);
        }
        if (JumlahBidangRuang.Count > 0)
            randomBidangRuang=Random.RandomRange(0, JumlahBidangRuang.Count);


        randomA = Random.RandomRange(0, JumlahBidangRuang[randomBidangRuang].CharacterSprite.Count);
        JumlahBidangRuang[randomBidangRuang].CharacterPrefabs.GetComponent<PlayerMovement>().CharacterSprite.sprite 
            = JumlahBidangRuang[randomBidangRuang].CharacterSprite[randomA];
        Instantiate(JumlahBidangRuang[randomBidangRuang].CharacterPrefabs, playerPosition);

        if (playerscript == null)
        {
            playerscript = FindObjectOfType<PlayerMovement>();
        }

        if (RandomJawaban == 0)//jawaban kanan bawah
        {
            //Instantiate(JawabanPrefab[randomA], PosisijawabanBagA[0]);
            JumlahBidangRuang[randomBidangRuang].JawabanPrefab.GetComponent<jawabanScript>().jawabanSprite.sprite 
                = JumlahBidangRuang[randomBidangRuang].JawabanSprite[randomA];

            Instantiate(JumlahBidangRuang[randomBidangRuang].JawabanPrefab, JumlahBidangRuang[randomBidangRuang].Posisijawaban[0]);
            alffabetId = randomA;
            terisi[0] = true;
        }

        else if (RandomJawaban == 1)//kiri bawah
        {
            //Instantiate(JawabanPrefab[randomA], PosisijawabanBagA[1]);
            JumlahBidangRuang[randomBidangRuang].JawabanPrefab.GetComponent<jawabanScript>().jawabanSprite.sprite
                = JumlahBidangRuang[randomBidangRuang].JawabanSprite[randomA];
            Instantiate(JumlahBidangRuang[randomBidangRuang].JawabanPrefab, JumlahBidangRuang[randomBidangRuang].Posisijawaban[1]);

            alffabetId = randomA;
            terisi[1] = true;
        }
        else if (RandomJawaban == 2)//kanan atas
        {
            //Instantiate(JawabanPrefab[randomA], PosisijawabanBagA[1]);
            JumlahBidangRuang[randomBidangRuang].JawabanPrefab.GetComponent<jawabanScript>().jawabanSprite.sprite 
                = JumlahBidangRuang[randomBidangRuang].JawabanSprite[randomA];
            Instantiate(JumlahBidangRuang[randomBidangRuang].JawabanPrefab, JumlahBidangRuang[randomBidangRuang].Posisijawaban[2]);

            alffabetId = randomA;
            terisi[2] = true;
        }
        else if (RandomJawaban >= 3)//kanan atas
        {
            //Instantiate(JawabanPrefab[randomA], PosisijawabanBagA[1]);
            JumlahBidangRuang[randomBidangRuang].JawabanPrefab.GetComponent<jawabanScript>().jawabanSprite.sprite 
                = JumlahBidangRuang[randomBidangRuang].JawabanSprite[randomA];
            Instantiate(JumlahBidangRuang[randomBidangRuang].JawabanPrefab, JumlahBidangRuang[randomBidangRuang].Posisijawaban[3]);

            alffabetId = randomA;
            terisi[3] = true;
        }

        //================setengah jawaban yang benar======================================
        if (randomSetJawaban == RandomJawaban)
        {
            if (RandomJawaban<4)
            {
                randomSetJawaban = Random.RandomRange(randomSetJawaban, 4);
            }
            else if(RandomJawaban>0)
            {
                randomSetJawaban = Random.RandomRange(0,randomSetJawaban);
            }
        }
        if (randomSetJawaban != RandomJawaban)
        {
            if (randomSetJawaban == 0)//jawaban kanan bawah
            {
                //Instantiate(JawabanPrefab[randomA], PosisijawabanBagA[0]);
                JumlahBidangRuang[randomBidangRuang].SetengahJawabSprite[randomjenissetJawaban].SetengahJawaban.GetComponent<jawabanScript>().jawabanSprite.sprite
                    = JumlahBidangRuang[randomBidangRuang].SetengahJawabSprite[randomjenissetJawaban].SetengahJawabSprite[randomA];

                Instantiate(JumlahBidangRuang[randomBidangRuang].SetengahJawabSprite[randomjenissetJawaban].SetengahJawaban, JumlahBidangRuang[randomBidangRuang].Posisijawaban[0]);
                alffabetId = randomA;
                terisi[0] = true;
            }

            else if (randomSetJawaban == 1)//kiri bawah
            {
                //Instantiate(JawabanPrefab[randomA], PosisijawabanBagA[1]);
                JumlahBidangRuang[randomBidangRuang].SetengahJawabSprite[randomjenissetJawaban].SetengahJawaban.GetComponent<jawabanScript>().jawabanSprite.sprite
                    = JumlahBidangRuang[randomBidangRuang].SetengahJawabSprite[randomjenissetJawaban].SetengahJawabSprite[randomA];
                Instantiate(JumlahBidangRuang[randomBidangRuang].SetengahJawabSprite[randomjenissetJawaban].SetengahJawaban, JumlahBidangRuang[randomBidangRuang].Posisijawaban[1]);

                alffabetId = randomA;
                terisi[1] = true;
            }
            else if (randomSetJawaban == 2)//kanan atas
            {
                //Instantiate(JawabanPrefab[randomA], PosisijawabanBagA[1]);
                JumlahBidangRuang[randomBidangRuang].SetengahJawabSprite[randomjenissetJawaban].SetengahJawaban.GetComponent<jawabanScript>().jawabanSprite.sprite
                    = JumlahBidangRuang[randomBidangRuang].SetengahJawabSprite[randomjenissetJawaban].SetengahJawabSprite[randomA];
                Instantiate(JumlahBidangRuang[randomBidangRuang].SetengahJawabSprite[randomjenissetJawaban].SetengahJawaban, JumlahBidangRuang[randomBidangRuang].Posisijawaban[2]);

                alffabetId = randomA;
                terisi[2] = true;
            }
            else if (randomSetJawaban >= 3)//kanan atas
            {
                //Instantiate(JawabanPrefab[randomA], PosisijawabanBagA[1]);
                JumlahBidangRuang[randomBidangRuang].SetengahJawabSprite[randomjenissetJawaban].SetengahJawaban.GetComponent<jawabanScript>().jawabanSprite.sprite
                    = JumlahBidangRuang[randomBidangRuang].SetengahJawabSprite[randomjenissetJawaban].SetengahJawabSprite[randomA];
                Instantiate(JumlahBidangRuang[randomBidangRuang].SetengahJawabSprite[randomjenissetJawaban].SetengahJawaban, JumlahBidangRuang[randomBidangRuang].Posisijawaban[3]);

                alffabetId = randomA;
                terisi[3] = true;
            }
        }
        
        //================mengisi posisi yang kosong=======================================
        int randomR = Random.RandomRange(0, JumlahBidangRuang[randomBidangRuang].JawabanSprite.Count);

        if (JumlahBidangRuang.Count > 0)
            randomBidangRuang = Random.RandomRange(0, JumlahBidangRuang.Count);

        for (int i = 0; i < JumlahBidangRuang[randomBidangRuang].Posisijawaban.Count; i++)
        {
            while (randomA==RandomJawaban||randomA==randomSetJawaban)
            {
                randomA = Random.RandomRange(0, JumlahBidangRuang[randomBidangRuang].CharacterSprite.Count);
            }
            if (i == 0 && terisi[i] == false)
            {
                if (randomA == 0)
                {
                    JumlahBidangRuang[randomBidangRuang].JawabanPrefab.GetComponent<jawabanScript>().jawabanSprite.sprite
                        = JumlahBidangRuang[randomBidangRuang].JawabanSprite[randomA + randomR];
                    Instantiate(JumlahBidangRuang[randomBidangRuang].JawabanPrefab, JumlahBidangRuang[randomBidangRuang].Posisijawaban[i]);
                }
                else if (randomA > 0)
                {
                    if (randomR != randomA)
                    {
                        JumlahBidangRuang[randomBidangRuang].JawabanPrefab.GetComponent<jawabanScript>().jawabanSprite.sprite
                            = JumlahBidangRuang[randomBidangRuang].JawabanSprite[randomR];
                        Instantiate(JumlahBidangRuang[randomBidangRuang].JawabanPrefab, JumlahBidangRuang[randomBidangRuang].Posisijawaban[0]);
                    }
                    else
                    {
                        randomR = Random.RandomRange(randomA, JumlahBidangRuang[randomBidangRuang].JawabanSprite.Count);
                        JumlahBidangRuang[randomBidangRuang].JawabanPrefab.GetComponent<jawabanScript>().jawabanSprite.sprite
                            = JumlahBidangRuang[randomBidangRuang].JawabanSprite[randomR];
                        Instantiate(JumlahBidangRuang[randomBidangRuang].JawabanPrefab, JumlahBidangRuang[randomBidangRuang].Posisijawaban[0]);
                    }
                }
            }
            if (i != 0 && terisi[i] == false)
            {
                if (JumlahBidangRuang.Count > 0)
                    randomBidangRuang = Random.RandomRange(0, JumlahBidangRuang.Count);
                randomR = Random.RandomRange(0, JumlahBidangRuang[randomBidangRuang].JawabanSprite.Count);
                if (randomA == 0)
                {
                    JumlahBidangRuang[randomBidangRuang].JawabanPrefab.GetComponent<jawabanScript>().jawabanSprite.sprite
                        = JumlahBidangRuang[randomBidangRuang].JawabanSprite[randomA + 1];
                    Instantiate(JumlahBidangRuang[randomBidangRuang].JawabanPrefab, JumlahBidangRuang[randomBidangRuang].Posisijawaban[i]);
                }
                else if (randomA > 0)
                {
                    if (randomR != randomA)
                    {
                        JumlahBidangRuang[randomBidangRuang].JawabanPrefab.GetComponent<jawabanScript>().jawabanSprite.sprite
                            = JumlahBidangRuang[randomBidangRuang].JawabanSprite[randomR];
                        Instantiate(JumlahBidangRuang[randomBidangRuang].JawabanPrefab, JumlahBidangRuang[randomBidangRuang].Posisijawaban[i]);
                    }
                    else
                    {
                        randomR = Random.RandomRange(randomA, JumlahBidangRuang[randomBidangRuang].JawabanSprite.Count);
                        JumlahBidangRuang[randomBidangRuang].JawabanPrefab.GetComponent<jawabanScript>().jawabanSprite.sprite
                            = JumlahBidangRuang[randomBidangRuang].JawabanSprite[randomR];
                        Instantiate(JumlahBidangRuang[randomBidangRuang].JawabanPrefab, JumlahBidangRuang[randomBidangRuang].Posisijawaban[i]);
                    }
                }
            }

        }
        sfx.audio.volume = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pause == false)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(1);
        }

        skor = (JSSoal - JSoalBenar) / 100;
        
        skor = JSoalBenar;
    }

    public void Navigator(string NamaScene)//masukan nama scene yang di tuju
    {
        _scene = NamaScene;
        sfx._sfx(0);
        StartCoroutine(ChangeScene(0.8f));
    }

    public void Transisi(string namaScene)
    {
        _scene = namaScene;
        StartCoroutine(ChangeScene(1f));
    }

    public void Pause()
    {
        sfx._sfx(0);
        sfx.bgm.volume = 0.6f;
        pause = true;
        PanelPause.SetActive(true);
    }

    public void correctAnsware()
    {
        idcondition = 0;
        StartCoroutine(showConditionPanel(1.5f));
        playing = false;
    }

    public void incorrectAnsware()
    {
        idcondition = 1;
        StartCoroutine(showConditionPanel(1.5f));
        playing = false;
    }

    public void Resume()
    {
        sfx._sfx(0);
        sfx.bgm.volume = 1f;
        pause = false;
        PanelPause.SetActive(false);
    }

    public void nextQuestion()
    {
        sfx.bgm.volume = 1f;
        PanelJawabBenar.SetActive(false);
        PanelJawabSalah.SetActive(false);
        playing = true;
        if (playing==true)
        {
            playerPosition.transform.DetachChildren();
           
            foreach (var BidangRuang in JumlahBidangRuang)
            {
                for (int i = 0; i < BidangRuang.Posisijawaban.Count; i++)
                {
                    BidangRuang.Posisijawaban[i].transform.DetachChildren();
                }
            }
            for (int i = 0; i < terisi.Count; i++)
            {
                terisi[i] = false;
            }
            InstanceObj();
        }
    }

    public void _Presult()
    {
        if (storage.highscore<=skor)
        {
            storage.highscore = skor;
        }
        storage.save();
        sfx._sfx(0);
        sfx.bgm.Stop();
        PResult.SetActive(true);
        if (JSoalBenar>=JSoalSalah)
        {
            ResultText.text = "Wih Jago";
            ResultText.color= new Color32(255,226,0,255);
        }
        else
        {
            ResultText.text = "Yuk Belajar lagi";
            ResultText.color = new Color32(255, 32, 0, 255);
        }
        TotalSoalTxt.text = JSSoal.ToString();
        BenarTxt.text = JSoalBenar.ToString();
        SalahTxt.text = JSoalSalah.ToString();
        HighscoreTxt.text = storage.highscore.ToString();
        playing = false;
    }

    IEnumerator showConditionPanel(float time)
    {
        yield return new WaitForSeconds(time);
        if (idcondition ==0)
        {
            PanelJawabBenar.SetActive(true);
        }
        else
        {
            PanelJawabSalah.SetActive(true);
        }
        StopCoroutine(showConditionPanel(0));
    }
    IEnumerator WaitingTIme(float time)
    {
        yield return new WaitForSeconds(time);
        playing = true;
        StopCoroutine(WaitingTIme(0));
    }
    IEnumerator ChangeScene(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(_scene);
        StopAllCoroutines();
    }
}
