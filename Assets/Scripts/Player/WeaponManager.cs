using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private GameObject playerCam;
    [SerializeField] private float range = 100f;
    RaycastHit hit;
    bool raycastHit;
    [SerializeField] private float damage = 20;
    Animator shootAnimator;
    // Start is called before the first frame update
    void Start()
    {
        playerCam = GameObject.FindGameObjectWithTag("MainCamera");
        shootAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //Debug.Log("Fire!");
            Shoot();
            ShootAnim(true);
        }
        else
        {
            ShootAnim(false);
        }
    }
    void Shoot()
    {
        //params (origin, destiny, save in Raycast hit, )
        
        raycastHit = Physics.Raycast(playerCam.transform.position, transform.forward, out hit, range);
        Debug.DrawRay(playerCam.transform.position, hit.point, Color.red);
        if (raycastHit)
        {
            //Debug.Log("something in the way");
            EnemyManager enemyManager  = hit.transform.GetComponent<EnemyManager>();
            if (enemyManager!=null)
            {
                Debug.Log("Hit to enemy");
                enemyManager.Hit(damage);
            }
        }
    }
    void ShootAnim(bool value)
    {
        shootAnimator.SetBool(ShootAnimParams.isShooting, value);
    }
}
public static class ShootAnimParams
{
    public static string isShooting= "isShooting";
}
