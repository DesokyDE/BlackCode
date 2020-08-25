
using System;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms;
using System.Collections;
using UnityEditor;

public class EnemyHealth : MonoBehaviour
{
    public float EnemyHeal = 100f;
    
    
    //Date is a variable taking date from all objects and save it
    [SerializeField]
    GameDate Date;
  
    
    public void damage(float DamagePoints)
    {
        EnemyHeal -= DamagePoints;
        if (EnemyHeal <= 0)
        {
            Destroy(gameObject);
            Date.Kills += 1;
        }

    }
   

}
