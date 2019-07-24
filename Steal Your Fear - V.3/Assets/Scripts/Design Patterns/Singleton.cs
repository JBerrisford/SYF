using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class to make sure only 1 of this class can be in the scene at any time.
public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<T>();

                if (instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = typeof(T).Name;
                    instance = go.AddComponent<T>();            
                }
            }

            return instance;
        }
    }

    public virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(this);
        }
        else
        {
            Debug.Log("Manager Deleted - " + typeof(T).Name);
            Destroy(this);
        }
    }
}
