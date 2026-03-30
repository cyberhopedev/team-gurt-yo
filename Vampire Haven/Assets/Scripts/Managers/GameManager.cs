using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            StartCoroutine(MakePersistent());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator MakePersistent()
    {
        yield return null;
        DontDestroyOnLoad(gameObject);
    }
}