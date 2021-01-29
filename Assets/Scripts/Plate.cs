using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Plate : MonoBehaviour
{
    private Dictionary<string, int> _map;
    private bool _completed;

    void Awake()
    {
        var conditionList = GameManager.Instance.ConditionList;
        _map = _ConvertToMap(conditionList);
        _completed = false;
    }

    private static Dictionary<string, int> _ConvertToMap(ConditionList conditionList)
    {
        var map = new Dictionary<string, int>();
        foreach (var data in conditionList.conditions)
        {
            map.Add(data.name, data.amount);
        }
        return map;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_completed)
            return;

        var tag = collision.collider.tag;
        if (_map.ContainsKey(tag) && _map[tag] > 0)
        {
            _map[tag]--;
        }

        _CheckComplete();
    }

    private void OnCollisionExit(Collision collision)
    {
        if (_completed)
            return;

        var tag = collision.collider.tag;
        if (_map.ContainsKey(tag))
        {
            _map[tag]++;
        }
    }

    private bool _CheckComplete()
    {
        bool finished = true;
        foreach (var i in _map)
        {
            if (i.Value != 0)
            {
                finished = false;
                break;
            }
        }

        if (finished)
        {
            _completed = true;
            GameManager.Instance.Complete();
        }

        return finished;
    }
}
