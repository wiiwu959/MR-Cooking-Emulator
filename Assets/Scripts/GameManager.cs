using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance{
        get {
            if(_instance != null)
            {
                return _instance;
            }
            _instance = FindObjectOfType<GameManager>();
            if(_instance != null)
            {
                return _instance;
            }
            _CreateDefault();
            return _instance;

        }
    }

    [SerializeField]
    private ConditionList _conditionList;
    public ConditionList ConditionList => _conditionList;

    private static void _CreateDefault() {
        GameObject singletonObject = new GameObject("GameManager");
        _instance = singletonObject.AddComponent<GameManager> ();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Complete()
    {
    }
}
