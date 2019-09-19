using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPickUp : MonoBehaviour
{

    [SerializeField] private float foodMass;

    private Rigidbody player;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale *= Mathf.Log10(foodMass) + 1;
    }


    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    body = collision.GetComponentInParent<Rigidbody2D>();
    //    StartCoroutine(AddMass(foodMass));
    //    collision.GetComponent<PlayerController>().SendMessage("OnPausedGame", foodMass, SendMessageOptions.DontRequireReceiver);
    //    StartCoroutine(DestroyProcess());
    //}

    private void OnTriggerEnter(Collider collision)
    {
        player = collision.GetComponentInParent<Rigidbody>();
        StartCoroutine(AddMass(foodMass));
        collision.GetComponent<PlayerController>().SendMessage("OnPausedGame", foodMass, SendMessageOptions.DontRequireReceiver);
        StartCoroutine(DestroyProcess());
    }


    IEnumerator AddMass(float foodMass)
    {
        for (int i = 1; i <= foodMass; i++)
        {
            yield return new WaitForSeconds(1f);
            player.mass += 1f;
            Debug.Log("player mass: " + player.mass);
        }
    }
    IEnumerator DestroyProcess()
    {
        yield return new WaitForSeconds(foodMass);
        Destroy(gameObject);
    }
}
