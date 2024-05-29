using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSlider : MonoBehaviour
{
    public Slider healthSlider;
    private GameObject _player;
    void Awake()
    {
        _player = GameObject.FindWithTag("Player");

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = _player.GetComponent<Player>().GetHealth();
    }
}
