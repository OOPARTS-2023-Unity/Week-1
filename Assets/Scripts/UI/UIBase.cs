using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    protected RectTransform _rectTransform;

    Dictionary<string, Dictionary<Type, Component>> _components
        = new Dictionary<string, Dictionary<Type, Component>>();
    Dictionary<string, GameObject> _gameObjects
        = new Dictionary<string, GameObject>();

    protected virtual void LoadComponentsNeeded<T>(Type type) where T : Component
    {
        LoadComponentsNeeded<T>(Enum.GetNames(type));
    }

    protected virtual void LoadComponentsNeeded<T>(string[] nameList) where T : Component
    {
        foreach (string name in nameList)
        {
            if (name.Equals("self"))
            {
                if (!_components.ContainsKey(name))
                {
                    _components[name] = new Dictionary<Type, Component>();
                }
                _components[name][typeof(T)] = GetComponent<T>();
            }
            else
            {
                T[] targets = GetComponentsInChildren<T>();
                bool found = false;
                foreach (T t in targets)
                {
                    if (t.name.Equals(name))
                    {
                        if (!_components.ContainsKey(name))
                        {
                            _components[name] = new Dictionary<Type, Component>();
                        }
                        _components[name][typeof(T)] = t;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    Debug.LogError($"Couldn't find object name: {name}");
                }
            }
        }
    }

    protected virtual void LoadObjectsNeeded(Type type)
    {
        LoadObjectsNeeded(Enum.GetNames(type));
    }

    protected virtual void LoadObjectsNeeded(string[] nameList)
    {
        foreach (string name in nameList)
        {
            GameObject go = _rectTransform.Find(name).gameObject;
            if (go == null)
            {
                Debug.LogError($"Couldn't find object name: {name}");
            }
            else
            {
                _gameObjects[name] = go;
            }
        }
    }

    protected virtual GameObject LoadObjectFromResources(string name, Transform parent, Type[] types, string objectName, string path = null)
    {
        GameObject prefab = GameManager.UI.GetPrefabResource(name, path);
        GameObject newObject = Instantiate(prefab);
        newObject.name = objectName;
        newObject.transform.SetParent(parent);

        foreach (Type type in types)
        {
            Component target = newObject.GetComponent(type);
            if (target != null)
            {
                if (!_components.ContainsKey(objectName))
                {
                    _components[objectName] = new Dictionary<Type, Component>();
                }
                _components[objectName][type] = target;
            }
        }
        _gameObjects[objectName] = newObject;
        return newObject;
    }

    protected virtual GameObject LoadObjectFromResources(Enum name, Transform parent, Type[] types, string objectName, string path = null)
    {
        return LoadObjectFromResources(name.ToString(), parent, types, objectName, path);
    }

    protected virtual T GetComp<T>(string name) where T : Component
    {
        return (T)_components[name][typeof(T)];
    }

    protected virtual GameObject GetGameObjectLoaded(string name)
    {
        return _gameObjects[name];
    }

    protected virtual T GetComp<T>(Enum name) where T : Component
    {
        return GetComp<T>(name.ToString());
    }

    protected virtual GameObject GetGameObjectLoaded(Enum name)
    {
        return GetGameObjectLoaded(name.ToString());
    }

    protected virtual void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }
}
