using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GIB
{
    public class Singleton<T> : MonoBehaviour where T: MonoBehaviour
    {
        private static T _instance;
        private static object _lock = new object();

        public static T Instance
        {
            get
            {
                
                lock( _lock)
                {
                    if( _instance == null )
                    {
                        var instances = FindObjectsOfType<T>();
                        if(instances.Length > 1)
                        {
                            Debug.LogError($"Multiple instances of {typeof(Singleton<T>)}. Deleting extras");
                            foreach( T badInstance in instances )
                            {
                                Destroy(badInstance);
                            }
                            _instance = FindObjectOfType<T>();
                            return _instance;
                        }

                        if(instances.Length ==1)
                        {
                            return instances[0];
                        }

                        if (instances.Length == 0)
                        {
                            Debug.LogWarning($"An object attempted to get the instance of {typeof(Singleton<T>)} but it was not found." +
                                $" A clone has been created.");

                            GameObject newSingleton = new GameObject();
                            _instance = newSingleton.AddComponent<T>();
                            newSingleton.name = $"{typeof(Singleton<T>)} Clone";
                        }
                    }
                    return _instance;
                }
            }
        }
    }
}
