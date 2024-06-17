using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthSlider : MonoBehaviour
{
    public Slider healthSlider;
    private Enemy _boss;

    public void DisableAnimator()
    {
        this.GetComponent<Animator>().enabled = false;
    }
    void Awake()
    {
        _boss = GameObject.FindWithTag("Boss").transform.GetChild(0).gameObject.GetComponent<Enemy>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = _boss.health;
    }
}
