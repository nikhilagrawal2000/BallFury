using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text countText;
    public Text winText;
    public Text timer;
    public AudioSource collection_sound;

    [SerializeField]
    private float forceMag = 100;
    [SerializeField]
    private float jumpMag = 200;
    private Rigidbody rb;
    private int count;
    private float starttime;
    private bool finish;
    private bool gameover_bool;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        collection_sound = GetComponent<AudioSource>();
        count = 0;
        SetCountText();
        gameover();
        winText.text = "";
        starttime = Time.time;
        finish = false;
        rb.gameObject.SetActive(true);
    }

    //void Update()
    //{
    //    Vector3 dir = Vector3.zero;
    //    dir.x = -Input.acceleration.y;
    //    dir.z = Input.acceleration.x;
    //    if (dir.sqrMagnitude > 1)
    //        dir.Normalize();

    //    dir *= Time.deltaTime;
    //    transform.Translate(dir * speed);
    //}
    void Update()
    {
        if (finish)
        {
            if (Input.touchCount > 0 || Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(2);
            }
            return;

        }
        else
        {
            float t = Time.time - starttime;
            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString("f2");
            timer.text = "Time : " + minutes + ":" + seconds;
        }
    }

    void FixedUpdate()
    {
        if (Application.isMobilePlatform)
        {
            Vector3 movement = new Vector3(Input.acceleration.x, 0.0f, Input.acceleration.y);
            rb.velocity = movement * speed;
        } else
        {
            if(Input.GetKey(KeyCode.UpArrow))
            {
                rb.AddForce(new Vector3(0, 0, forceMag));
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rb.AddForce(new Vector3(-forceMag, 0, 0));
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rb.AddForce(new Vector3(forceMag, 0, 0));
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                rb.AddForce(new Vector3(0, 0, -forceMag));
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(new Vector3(0, jumpMag, 0));
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {  
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            collection_sound.Play();
            count = count + 1;
            SetCountText();
        }

        if (other.gameObject.CompareTag("Obstacles"))
        {
            rb.gameObject.SetActive(false);
            lose();
            gameover_bool = true;
           
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 18)
        {
            rb.gameObject.SetActive(false);
            winText.text = "You Win! \n \n \n \n \n  Tap To Restart";
            finish = true;
        }
    }

    void lose()
    {
        winText.text = "You Lose! \n \n \n \n Tap To Restart";
        winText.color = Color.red;
    }

    void gameover()
    {
        if (gameover_bool)
            finish = true;
    }
}

