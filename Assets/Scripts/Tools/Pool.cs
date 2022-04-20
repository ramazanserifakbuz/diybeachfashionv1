using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public GameObject Taken = null;
    private static Pool instance;
    Dictionary<string, GameObject> SubPoolDictionary = new Dictionary<string, GameObject>();


    // Start is called before the first frame update

    void Awake()
    {
        instance = GetComponent<Pool>();
        CreatePool();
        FillAllPools(50);
    }
    public static Pool Instance()
    {
        return instance;
    }
    public void CreatePool()
    {
        //Creating SubPools
        for (int i = transform.childCount; i > 0; i--)
        {
            GameObject Obj = transform.GetChild(0).gameObject;
            GameObject SubPool = new GameObject();

            SubPool.name = Obj.name;
            SubPool.transform.position = transform.position;
            Obj.transform.SetParent(SubPool.transform, true);
            SubPool.SetActive(false);
            SubPoolDictionary.Add(SubPool.name, SubPool);
        }
        for (int i = 0; i <= SubPoolDictionary.Count - 1; i++)
        {
            SubPoolDictionary.ElementAt(i).Value.transform.SetParent(this.gameObject.transform, true);
        }

        if (Taken == null)
        {
            Taken = new GameObject();
            Taken.name = "Taken*";
            Taken.transform.SetParent(this.gameObject.transform, true);
        }
    }
    public void FillAllPools(int number)
    {
        if (number > 0)
        {
            for (int i = 0; i <= SubPoolDictionary.Count - 1; i++)
            {
                FillPool(SubPoolDictionary.ElementAt(i).Key, number);
            }
        }
        else
        {
            Debug.LogError("Number must be positive!");
        }
    }
    public void FillPool(string name, int number)
    {
        if (number > 0)
        {
            Debug.Log(name);
            GameObject SubPool = SubPoolDictionary[name];
            GameObject SampleObj = SubPool.transform.GetChild(0).gameObject;

            for (int i = 0; i <= number - 1; i++)
            {
                GameObject NewObj = Instantiate(SampleObj);
                NewObj.name = SampleObj.name;
                NewObj.transform.SetParent(SubPool.transform, true);
            }

        }
        else
        {
            Debug.LogError("Number must be positive!");
        }
    }
    public void Set(GameObject Obj, GameObject Target)
    {
        Obj.transform.SetParent(Target.transform, true);
    }
    public GameObject Return(string name)
    {
        return SubPoolDictionary[name].transform.GetChild(0).gameObject;
    }
    public GameObject Get(string name)
    {
        Debug.Log(name);
        if (SubPoolDictionary[name].transform.childCount > 0)
        {
            GameObject Obj = Return(name);
            Set(Obj, Taken);
            Obj.SetActive(true);
            return Obj;
        }
        else
        {
            Debug.LogError("There is no enough object in " + name + " subpool");
            return null;
        }
    }
    public void SendBack(GameObject Obj)
    {
        if (itExists(Obj.name))
        {
            Obj.transform.position = transform.position;
            Set(Obj, SubPoolDictionary[Obj.name]);
        }
        else if (itExists(Obj.tag))
        {
            Obj.transform.position = transform.position;
            Set(Obj, SubPoolDictionary[Obj.tag]);
        }
    }
    public void SendBackAllChildrenOf(GameObject Parent)
    {
        int total = Parent.transform.childCount;

        for (int i = 0; i < total; i++)
        {
            Debug.Log(Parent.transform.GetChild(0).gameObject.name + " GET BACK!");
            SendBack(Parent.transform.GetChild(0).gameObject);
        }
    }
    public void sendBackAllTakens()
    {
        SendBackAllChildrenOf(Taken);

    }
    public bool itExists(string Name)
    {
        if (SubPoolDictionary.ContainsKey(Name))
        {
            return true;
        }
        else
        {
            Debug.LogError(Name + " does not exist in SubPoolDictionary!");
            return false;
        }
    }


}
