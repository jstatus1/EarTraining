using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
/// Hard Storage for now may be replaced with better storage
///<summary>
public class CollectionsStorage : MonoBehaviour
{
    [SerializeField] List<DataCollection> ListCollections;
    
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public List<DataCollection> getListCollections()
    {
        return ListCollections;
    }

    public DataCollection grabCollection(DataTypes dataTypes)
    {
        DataCollection foundCollection = new DataCollection();
        foreach(DataCollection collection in ListCollections)
        {
            if(dataTypes.Equals(collection.dataTypes))
            {
                foundCollection = collection;
            }
        }

        return foundCollection;
    }
    
}
