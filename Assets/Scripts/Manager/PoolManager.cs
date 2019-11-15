using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectTypes
{
    Shape,
}

[System.Serializable]
public class PoolObjects
{
    public GameObject prefab;
    public int ammount;
    public ObjectTypes objectType;
}

public class PoolManager : MonoBehaviour
{

    #region Events
    public delegate void PoolManagerEvent(IPoolObject pooledObject);
    public PoolManagerEvent OnObjectPooled;
    #endregion

    public static PoolManager instance;
    public List<PoolObjects> poolObjects = new List<PoolObjects>();
    Vector3 poolPosition = new Vector3(1000, 1000, 1000);
    Dictionary<ObjectTypes, List<IPoolObject>> poolDictionary;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        poolDictionary = new Dictionary<ObjectTypes, List<IPoolObject>>();
        foreach (PoolObjects obj in poolObjects)
        {
            List<IPoolObject> objectsToAdd = new List<IPoolObject>();
            Transform spawnParent = new GameObject(obj.objectType.ToString()).transform;
            spawnParent.parent = transform;
            for (int i = 0; i < obj.ammount; i++)
            {
                GameObject instantiateObject = Instantiate(obj.prefab, spawnParent);
                IPoolObject instantiateObjectInterface = instantiateObject.GetComponent<IPoolObject>();
                if (instantiateObjectInterface != null)
                {
                    instantiateObjectInterface.OnObjectDestroy += OnObjectDestroy;
                    instantiateObjectInterface.OnObjectSpawn += OnObjectSpawn;
                    OnObjectDestroy(instantiateObjectInterface);
                    objectsToAdd.Add(instantiateObjectInterface);
                }
                else
                {
                    Debug.Log("il prefab: " + instantiateObject.ToString() + "     type:" + obj.objectType.ToString() + " non implementa l'interfaccia IPoolObject");
                    break;
                }
            }
            poolDictionary.Add(obj.objectType, objectsToAdd);
        }
    }

    private void OnObjectDestroy(IPoolObject objectToDestroy)
    {
        objectToDestroy.CurrentState = State.InPool;
        objectToDestroy.gameObject.transform.position = poolPosition;
        objectToDestroy.OwnerObject = null;
    }

    private void OnObjectSpawn(IPoolObject objectToSpawn)
    {
        objectToSpawn.CurrentState = State.InUse;
    }

    public GameObject GetPooledObject(ObjectTypes type, GameObject _ownerObject)
    {
        foreach (IPoolObject _object in poolDictionary[type])
        {
            if (_object.CurrentState == State.InPool)
            {
                _object.OwnerObject = _ownerObject;
                if (OnObjectPooled != null)
                {
                    OnObjectPooled(_object);
                }
                return _object.gameObject;
            }
        }
        Debug.Log("Nessun " + type + " disponibile");
        return null;
    }

    private void OnDisable()
    {
        foreach (ObjectTypes type in poolDictionary.Keys)
        {
            foreach (IPoolObject pooledObject in poolDictionary[type])
            {
                pooledObject.OnObjectDestroy -= OnObjectDestroy;
                pooledObject.OnObjectSpawn -= OnObjectSpawn;
            }
        }
    }
}
