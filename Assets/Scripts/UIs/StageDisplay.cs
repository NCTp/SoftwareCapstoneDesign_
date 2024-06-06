using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageDisplay : MonoBehaviour
{
    public TextMeshProUGUI stageText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float stage = GameManager.instance.GetStage();
        stageText.text = stage.ToString();
    }
}
