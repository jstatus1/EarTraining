using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
/// Hard Storage for now may be replaced with better storage
///<summary>
public class CollectionsStorage : MonoBehaviour
{
    [SerializeField] List<DataCollection> ListCollections;

    public List<DataCollection> getListCollections()
    {
        return ListCollections;
    }

    public DataCollection grabCollection(string search)
    {
        DataCollection foundCollection = new DataCollection();
        foreach(DataCollection collection in ListCollections)
        {
            if(search.Equals(collection.dataTypes.ToString()))
            {
                foundCollection = collection;
            }
        }

        return foundCollection;
    }
    
}
