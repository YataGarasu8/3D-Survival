using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private static CharacterManager _instance;
    public static CharacterManager Instance 
    { 
        get 
        { 
            if (_instance == null)//방어 코드 Hierarchy에 게임 오브젝트가 존재하지 않아도 시작시 생성된다.
            {
                _instance = new GameObject("CharacterManager").AddComponent<CharacterManager>();
            }
            return _instance;
        } 
    }
    private Player _player;
    public Player Player { get { return _player; } set { _player = value; } }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
}
