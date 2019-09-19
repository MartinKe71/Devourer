//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class P1FoodPickUp : MonoBehaviour
//{
//    [SerializeField] private float foodMass;

//    private Rigidbody2D playerBody;


//    // Start is called before the first frame update
//    void Start()
//    {
//        transform.localScale *= Mathf.Log10(foodMass) + 1;
//    }

//private void OnTriggerEnter2D(Collider2D collision)
//{
//    playerBody = collision.GetComponentInParent<Rigidbody2D>();
//    StartCoroutine(AddMass(foodMass));
//    collision.GetComponent<P1Controller>().SendMessage("OnPausedGame", foodMass, SendMessageOptions.DontRequireReceiver);
//    StartCoroutine(DestroyProcess());
//}


//    IEnumerator AddMass(float foodMass)
//    {
//        for (int i = 1; i <= foodMass; i++)
//        {
//            yield return new WaitForSeconds(1f);
//            playerBody.mass += 1f;
//        }
//    }
//    IEnumerator DestroyProcess()
//    {
//        yield return new WaitForSeconds(foodMass);
//        Destroy(gameObject);
//    }
//}
