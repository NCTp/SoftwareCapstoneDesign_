using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEnergySlider : MonoBehaviour
{
    public Slider energySlider;
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
        energySlider.value = _player.GetComponent<Player>().GetEnergy();
    }
}
