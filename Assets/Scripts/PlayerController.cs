using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float maxSpeed;
    [SerializeField] private float acc;
    [SerializeField] private float speedBase;
    [SerializeField] private float sizeBase;
    [SerializeField] private PlayerType playerType;
    [SerializeField] PlayerCtrl[] playerCtrls;
    

    [System.Serializable]
    private class PlayerCtrl
    {
        public PlayerType playerType;
        public KeyCode up;
        public KeyCode down;
        public KeyCode right;
        public KeyCode left;
    }

    private Rigidbody rb;
    private CapsuleCollider capCol;
    private Animator anim;
    private PlayerCtrl playerCtrl;

    private bool lookRight = true;
    private bool iseating = false;

    private float origRadius;
    private float currentMass;
    private float scale;
    private float horiSpeed = 0f;
    private float vertSpeed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        capCol = GetComponent<CapsuleCollider>();

        currentMass = rb.mass;
        origRadius = capCol.radius;

        SetCtrl();
    }

    private void SetCtrl()
    {
        foreach (PlayerCtrl ctrl in playerCtrls)
        {
            if (ctrl.playerType == playerType)
            {
                playerCtrl = ctrl;
                return;
            }
        }
        Debug.LogError("No player type found!");
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        MoveHorizontal();
        MoveVertical();
        ChangeSize();
        SetSpeed();
    }

    private void SetSpeed()
    {
        if (!iseating)
        {
            Vector3 speed = new Vector3(horiSpeed, 0, vertSpeed);
            rb.transform.position += speed;
            //rb2d.transform.position = new Vector3(Mathf.Clamp(rb2d.transform.position.x, -30f, 30f),
            //                                      Mathf.Clamp(rb2d.transform.position.y, -16f, 16f), 0f);
            anim.SetFloat("Speed", speed.magnitude);
        }
    }

    private void ChangeSize()
    {
        if (rb.mass != currentMass)
        {
            scale = (Mathf.Log(rb.mass, sizeBase) + 1);
            transform.localScale = scale * new Vector3(1, 0, 1);
            capCol.radius = scale * origRadius;
            currentMass = rb.mass;
        }
    }

    private void MoveVertical()
    {
        if (Input.GetKey(playerCtrl.up))
        {
            vertSpeed = Mathf.Clamp(vertSpeed + acc * Time.deltaTime, 0f, maxSpeed * Mathf.Pow(speedBase, -currentMass + 1f));
            return;
        }
        else if (Input.GetKey(playerCtrl.down))
        {
            vertSpeed = Mathf.Clamp(vertSpeed - acc * Time.deltaTime, -maxSpeed * Mathf.Pow(speedBase, -currentMass + 1f), 0f);
            return;
        }
        vertSpeed = 0f;
    }

    private void MoveHorizontal()
    {
        if (Input.GetKey(playerCtrl.right))
        {
            horiSpeed = Mathf.Clamp(horiSpeed + acc * Time.deltaTime, 0f, maxSpeed * Mathf.Pow(speedBase, -currentMass + 1f));
            if (!lookRight)
            {
                Flip();
            }
            return;
        }
        else if (Input.GetKey(playerCtrl.left))
        {
            horiSpeed = Mathf.Clamp(horiSpeed - acc * Time.deltaTime, -maxSpeed * Mathf.Pow(speedBase, -currentMass + 1f), 0f);
            if (lookRight)
            {
                Flip();
            }
            return;
        }
        horiSpeed = 0f;
    }

    private void Flip()
    {
        lookRight = !lookRight;
        Vector3 myScale = transform.localScale;
        myScale.x *= -1;
        transform.localScale = myScale;
    }

    void OnPausedGame(float timePause)
    {
        StartCoroutine(Eat(timePause));
    }

    IEnumerator Eat(float timePause)
    {
        iseating = true;
        yield return new WaitForSeconds(timePause);
        iseating = false;
    }
}
