using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{   [System.Serializable]
    public class Pool
    {
        public string Tag;
        public int Size;
        public GameObject Prefab;
     }
    public static ObjectPooler Instance;
    private void Awake()
    {
        Instance = this;
    }

    public List<Pool> Pools;
    public Dictionary<string, Queue<GameObject>> PoolDictionary ;
    // Start is called before the first frame update
    void Start()
    {
        PoolDictionary = new Dictionary<string, Queue<GameObject>>();
        
        foreach(Pool pool in Pools) 
        {
            Queue<GameObject> ObjectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.Size; i++)
            {
                GameObject Obj = Instantiate(pool.Prefab);
                Obj.SetActive(false);
                ObjectPool.Enqueue(Obj);
            }
            PoolDictionary.Add(pool.Tag, ObjectPool);
        }

    }
    public GameObject SpawnFromPool (string Tag,Vector3 Position,Quaternion Rotation)
    {
        if (!PoolDictionary.ContainsKey(Tag))
        {
            Debug.LogWarning("Pool with Tag " + Tag + " doesn't exist.");
            return null;
        }
       
        GameObject ObjectToSpawn = PoolDictionary[Tag].Dequeue();
        
        ObjectToSpawn.SetActive(true);
        ObjectToSpawn.transform.position = Position;
        ObjectToSpawn.transform.rotation = Rotation;
        
        IPooledObject PooledObj = ObjectToSpawn.GetComponent<IPooledObject>();

        if (PooledObj!=null)
        {
            Debug.Log("ana fl object pooler");
            PooledObj.OnObjectSpawn();
        }
            
       PoolDictionary[Tag].Enqueue(ObjectToSpawn);
        return ObjectToSpawn;

    }

}
