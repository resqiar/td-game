using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject shootingPoint;
    public Animator animator;
    public float gunDamage = 10f;
    public float gunRange = 250f;
    private AudioSource audio;

    public GameObject muzzleFlash;
    public Transform muzzlePos;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // reset the animation
        if(animator.GetBool("isFiring")){
            animator.SetBool("isFiring", false);
        }

        if (Input.GetButtonDown("Fire1")){
            // shoot
            animator.SetBool("isFiring", true);
            DisplayMuzzleFlash();
            audio.Play();

            FireBullet();
        }
    }

    void DisplayMuzzleFlash() {
        GameObject light = Instantiate(muzzleFlash, muzzlePos);
        Destroy(light, 1f);
    }

    void FireBullet() {
        RaycastHit hit;

        // shoot a raycast forward from shooting point
        if(Physics.Raycast(shootingPoint.transform.position, transform.forward, out hit, gunRange)) {
            // if the hit object has enemy manager
            EnemyManager manager = hit.transform.GetComponent<EnemyManager>();

            if (manager != null) {
                // apply damage to the enemy 
                manager.ApplyDamage(gunDamage);
            }
        }
    }
}
