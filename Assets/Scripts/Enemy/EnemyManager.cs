using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// For avoid strange movements on the player reduce the collider just in the zombie head
/// </summary>
public class EnemyManager : MonoBehaviour
{
    public GameObject player;
    [SerializeField] Animator myAnimator;
    [SerializeField] NavMeshAgent myNavMesh;
    public float damage=20f;
    public float health = 100f;
    //GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myAnimator = GetComponentInChildren<Animator>();
        myNavMesh = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //move enemy to player position
        myNavMesh.destination = player.transform.position;      

        //get speed movement of player 
        if (myNavMesh.velocity.magnitude > 1)
        {
            myAnimator.SetBool(ZombieAnimParams.isRunning, true);
        }
        else
        {
            myAnimator.SetBool(ZombieAnimParams.isRunning, false);
        }
        //health state
        if (health <= 0)
        {
            Destroy(gameObject);
            GameManager.Instance.enemiesAlive--;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player)
        {
            Debug.Log("Collision with enemy");
            player.GetComponent<PlayerManager>().Hit(damage);
        }
    }
    public void Hit(float damage)
    {
        health -= damage;
    }

}
public static class ZombieAnimParams
{
    public static string isRunning = "isRunning";
}
