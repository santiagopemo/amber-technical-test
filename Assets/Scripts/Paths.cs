using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paths : MonoBehaviour
{
    public Path GetRandomPath()
    {
        List<Path> _allPaths = new List<Path>();
        for (int i = 0; i < transform.childCount; i++)
        {
            Path path = transform.GetChild(i).GetComponent<Path>();
            if (path) _allPaths.Add(path);
        }
        int idx = Random.Range(0, _allPaths.Count);
        return _allPaths[idx];
    }
}
