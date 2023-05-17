using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    GameManager gm;

    public SpriteRenderer CharacterSprite;
    [SerializeField]Rigidbody2D player;
    Vector3 playerVelocity;
    [SerializeField] float moveSpeed=10f;
    public bool benar;
    public bool salah=false;
    [SerializeField] Vector2 target;
    Transform _terget;
    private bool isDragging;
    int id = 0;
    public bool playgame=false;
    bool playsfx=false;
    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }

    void Start()
    {
        playgame = true;
        _terget = gameObject.transform;
        benar = false;
        if (player==null)
        {
            player = gameObject.GetComponent<Rigidbody2D>();
           
        }
        player.bodyType = RigidbodyType2D.Static;
    }
    private void Update()
    {
        if (gameObject.transform.parent==null)
        {
            Destroy(gameObject);
        }
        if (playgame==true)
        {
            if (isDragging == false)
            {
                if (benar == true&& salah==false)
                {
                    gm.sfx.bgm.volume = 0.2f;
                    if (playsfx == true)
                    {
                        gm.sfx._alfabet(gm.alffabetId);
                        playsfx = false;
                    }
                    Vector3 defaultscale = new Vector3(0.85f, 0.85f, 1);
                    gameObject.transform.localScale = new Vector3(0.85f, 0.85f, 1);
                    if (gameObject.transform.localScale == defaultscale)
                    {
                        transform.position = gm.JumlahBidangRuang[gm.randomBidangRuang].Posisijawaban[id].position;
                    }
                    gm.JSoalBenar += 1;
                    gm.JSSoal += 1;
                    print("Benar full");
                    gm.correctAnsware();
                    playgame = false;
                }
                else if (benar == true && salah == true)
                {
                    gm.sfx.bgm.volume = 0.2f;
                    if (playsfx == true)
                    {
                        gm.sfx._alfabet(gm.alffabetId);
                        playsfx = false;
                    }
                    Vector3 defaultscale = new Vector3(0.85f, 0.85f, 1);
                    gameObject.transform.localScale = new Vector3(0.85f, 0.85f, 1);
                    if (gameObject.transform.localScale == defaultscale)
                    {
                        transform.position = gm.JumlahBidangRuang[gm.randomBidangRuang].Posisijawaban[id].position;
                    }
                    gm.JSoalBenar += 0.5f;
                    gm.JSSoal += 1;
                    print("Benar setengah");
                    gm.incorrectAnsware();
                    playgame = false;
                }
                else if (salah == true)
                {
                    gm.sfx.bgm.volume = 0.2f;
                    if (playsfx == true)
                    {
                        gm.sfx._sfx(3);
                        playsfx = false;
                    }
                    Vector3 defaultscale = new Vector3(0.85f, 0.85f, 1);
                    gameObject.transform.localScale = new Vector3(0.85f, 0.85f, 1);
                    if (gameObject.transform.localScale == defaultscale)
                    {
                        transform.position = gm.JumlahBidangRuang[gm.randomBidangRuang].Posisijawaban[id].position;
                    }
                    gm.JSoalSalah += 1;
                    gm.JSSoal += 1;
                    gm.incorrectAnsware();
                    playgame = false;
                }
            }
        }
       
    }

    public void OnMouseDown()
    {
        if (gm.playing == true && gm.pause == false)
        {
            isDragging = true;
            gameObject.transform.localScale=new Vector2(1f,1f);
            gm.sfx._sfx(1);
            player.bodyType = RigidbodyType2D.Dynamic;
            player.gravityScale = 0;

        }
    }
    public void OnMouseDrag()
    {
        if (isDragging == true)
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //target.y = transform.position.y;
            transform.position = target;
        }
    }
    public void OnMouseUp()
    {
        //player.bodyType = RigidbodyType2D.Static;
        isDragging = false;
        gameObject.transform.localScale = new Vector2(0.85f, 0.85f);
        player.bodyType = RigidbodyType2D.Static;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDragging == true)
        {
            //benar full
            if (gm.RandomJawaban == 0)//jawaban kanan
            {
                if (collision.transform.tag == "kanan bawah")
                {
                    benar = true;
                    id = 0;
                }
                else if (collision.transform.tag == "kiri bawah")
                {
                    salah = true;
                    id = 1;
                }
                else if (collision.transform.tag == "kanan atas")
                {
                    salah = true;
                    id = 2;
                }
                else if (collision.transform.tag == "kiri atas")
                {
                    salah = true;
                    id = 3;
                }
            }
            else if (gm.RandomJawaban == 1)//jawaban kiri bawah
            {
                if (collision.transform.tag == "kiri bawah")
                {
                    benar = true;
                    id = 1;
                }
                else if (collision.transform.tag == "kanan bawah")
                {
                    salah = true;

                    id = 0;
                }
                else if (collision.transform.tag == "Kanan atas")
                {
                    salah = true;
                    id = 2;
                }
                else if (collision.transform.tag == "kiri atas")
                {
                    salah = true;
                    id = 3;
                }
            }
            else if (gm.RandomJawaban == 2)//jawaban kanan atas
            {
                if (collision.transform.tag == "kiri bawah")
                {
                    salah = true;
                    id = 1;
                }
                else if (collision.transform.tag == "kanan bawah")
                {
                    salah = true;
                    id = 0;
                }
                else if (collision.transform.tag == "kanan atas")
                {
                    benar = true;
                    id = 2;
                }
                else if (collision.transform.tag == "kiri atas")
                {
                    salah = true;
                    id = 3;
                }
            }
            else if (gm.RandomJawaban == 3)//jawaban kiri atas
            {
                if (collision.transform.tag == "kiri bawah")
                {
                    salah = true;
                    id = 1;
                }
                else if (collision.transform.tag == "kanan bawah")
                {
                    salah = true;
                    id = 0;
                }
                else if (collision.transform.tag == "Kanan atas")
                {
                    salah = true;
                    id = 2;
                }
                else if (collision.transform.tag == "kiri atas")
                {
                    benar = true;
                    id = 3;
                }
            }
            //jawaban benar setengah
            if (gm.randomSetJawaban!=gm.RandomJawaban)
            {
                if (gm.randomSetJawaban == 0)//jawaban kanan
                {
                    if (collision.transform.tag == "kanan bawah")
                    {
                        benar = true;
                        salah = true;
                        id = 0;
                    }
                }
                else if (gm.randomSetJawaban == 1)//jawaban kiri bawah
                {
                    if (collision.transform.tag == "kiri bawah")
                    {
                        benar = true;
                        salah = true;
                        id = 1;
                    }
                }
                else if (gm.randomSetJawaban == 2)//jawaban kanan atas
                {
                    if (collision.transform.tag == "kanan atas")
                    {
                        benar = true;
                        salah = true;
                        id = 2;
                    }
                }
                else if (gm.randomSetJawaban == 3)//jawaban kiri atas
                {
                    if (collision.transform.tag == "kiri atas")
                    {
                        benar = true;
                        salah = true;
                        id = 3;
                    }
                }
            }
        }
     playsfx = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        benar = false;
        salah = false;

        if (isDragging==true)
        {
            if (benar == true)
            {
                benar = false;
            }
            else if (salah == true)
            {
                salah = false;
            }
            else
            {
                benar = false;
                salah = false;
            }
        }
       
    }
}
