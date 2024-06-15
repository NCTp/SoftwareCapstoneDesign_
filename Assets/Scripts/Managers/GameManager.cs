using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Transform[] spawnpoints;
    
    private float _score;
    private int _stage = 1;
    private int _level = 1;
    private float _exp = 0.0f;
    
    void Awake()
    {
        // 싱글톤 설정
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _stage = 1;
    }
    public void AddScore(float points)
    {
        _score += points;
    }

    public float GetScore()
    {
        return _score;
    }

    public int GetStage()
    {
        return _stage;
    }

    public int GetLevel()
    {
        return _level;
    }

    public void SetLevel(int level)
    {
        _level = level;
    }

    public void AddExp(float amount)
    {
        _exp += amount;
        if (_exp >= 100.0f)
        {
            _level += 1;
            _exp -= 100.0f;
        }
    }

// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
