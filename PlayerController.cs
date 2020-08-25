
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Rendering;
using System;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Camera cam;
    float range = 100f;
    //[SerializeField]
    //GameObject DAmage;
    float firerate = 0.1f;
    float nextFire;
    float HeadDamage = 70;
    float BodyDamage = 20;
    [SerializeField]
    GameObject MuzzelFlash;
    public LayerMask Collectable;
    [SerializeField]
    GameDate Date;
    [SerializeField]
    LayerMask Enemy;
    [SerializeField]
    AudioSource WeaponShoot;
    [SerializeField]
    GameObject PressF;
    [SerializeField]
    float CurrentMag;
    [SerializeField]
    float AmmoToReload;
    [SerializeField]
    float ReloadMag;
    [SerializeField]
    Text Mag;
    float kills;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        AmmoToReload = Date.Ammo;
        if (Input.GetButtonDown("Fire1") && CurrentMag > 0)
        {
            WeaponShoot.Play();
        }
        else if (Input.GetButtonUp("Fire1") || CurrentMag == 0)
        {
            WeaponShoot.Stop();
        }
        if (Input.GetButton("Fire1") && Time.time > nextFire && CurrentMag > 0)
        {
            nextFire = Time.time + firerate;
            ParticleSystem Muzzel = MuzzelFlash.GetComponent<ParticleSystem>();
            CurrentMag -= 1;
            Muzzel.Play();
            shoot();
            
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
        
        RaycastHit CollectableHitInfo;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out CollectableHitInfo, 10, Collectable))
        {
            PressF.SetActive(true);
            CollectableItem Item = CollectableHitInfo.collider.GetComponent<CollectableItem>();
            GameObject hit = CollectableHitInfo.transform.gameObject;
            if (Input.GetKeyDown(KeyCode.F) && CollectableHitInfo.collider != null)
            {

                if (Item.Tag == "Ammo")
                {
                    AmmoToReload += Item.count;
                    print(Date.Ammo);
                    Destroy(hit);
                }
                else if (Item.tag == "weapon")
                {
                    print("this is new weapon !?");
                }

            }

        }
        else
        {
            PressF.SetActive(false);
        }
        Mag.text = CurrentMag.ToString();
        Date.Ammo = AmmoToReload;
    }
    void shoot()
    {
        RaycastHit HitPoint;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out HitPoint, range, Enemy))
        {
            GameObject Hit = HitPoint.transform.gameObject;
            EnemyHealth Heal = Hit.GetComponentInParent<EnemyHealth>();
            BodyPart part = Hit.GetComponent<BodyPart>();

            if (part.Tag == "Body")
            {
                Heal.damage(BodyDamage);
                print("This is Body Part");
                print(Heal.EnemyHeal);
            }
            else if (part.Tag == "Head")
            {
                Heal.damage(HeadDamage);
                print("This is Head");
                print(Heal.EnemyHeal);
            }


        }
    }
    void Reload()
    {
        if (AmmoToReload > 0)
        {
            if (CurrentMag < ReloadMag && AmmoToReload < ReloadMag)
            {
                float ToReload = ReloadMag - CurrentMag;
                if(ToReload <= AmmoToReload)
                {
                    AmmoToReload -= ToReload;
                    CurrentMag += ToReload;
                    print(AmmoToReload);
                }
                else if(ToReload >= AmmoToReload)
                {
                    CurrentMag = AmmoToReload;
                    AmmoToReload = 0f;
                }
                

            }
            else if (CurrentMag < ReloadMag && AmmoToReload >= ReloadMag)
            {
                float ToReload = ReloadMag - CurrentMag;
                AmmoToReload -= ToReload;
                CurrentMag += ToReload;
                print(AmmoToReload);
            }
            else if (CurrentMag < ReloadMag && AmmoToReload == ReloadMag)
            {
                CurrentMag = AmmoToReload;
                AmmoToReload = 0;
                print(AmmoToReload);
            }
        }
        
        
    }
}


