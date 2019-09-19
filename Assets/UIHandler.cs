using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHandler : MonoBehaviour
{
    public TextMeshProUGUI p1Score;
    public TextMeshProUGUI p2Score;
    public Slider scoreBar;


    // Start is called before the first frame update
    void Start()
    {
        scoreBar.image.fillAmount = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
