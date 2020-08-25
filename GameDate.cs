using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDate : MonoBehaviour
{
    public float playerHealth;
    public float Ammo;
    public float Kills;
    [SerializeField]
    Text AmmoCount;
    [SerializeField]
    Text KillsCount;
    // Start is called before the first frame update
    void Start()
    {
        Ammo = 60f;
    }

    // Update is called once per frame
    void Update()
    {
        KillsCount.text = Kills.ToString();
        AmmoCount.text = Ammo.ToString();
    }
}
