using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public float health = 100f;
    public TMP_Text healthText;

    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        healthText.text = $"Heath: {health}";
    }

    // Update is called once per frame
    void Update()
    {
        
        

    }
    public void Hit(float damage)
    {
        health -= damage;
        healthText.text = $"Health: {health}";
        if (health <= 0)
        {
            gameManager.GameOver();            
        }
    }
}
