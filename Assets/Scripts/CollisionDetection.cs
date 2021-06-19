// Refer to https://stackoverflow.com/a/50415096
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection: MonoBehaviour

{
    static private List<KeyValuePair<GameObject, GameObject>> collisionList =
        new List<KeyValuePair<GameObject, GameObject>>();

    static public List<KeyValuePair<GameObject, GameObject>> collisionInfo
    {
        get {
            // Sanity check
            return collisionList;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log($"Trigger Entered: ({this.gameObject.name}, {other.gameObject.name})");
        // Debug.Log(other.gameObject.name + " entered " + this.gameObject.name);

        //Get the two Objects involved in the collision
        GameObject col1 = this.gameObject;
        GameObject col2 = other.gameObject;

        //Add to the collison List
        RegisterTouchedItems(collisionList, col1, col2);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Debug.Log($"Trigger Exit: ({this.gameObject.name}, {other.gameObject.name})");

        //Get the two Objects involved in the collision
        GameObject col1 = this.gameObject;
        GameObject col2 = other.gameObject;

        //Remove from the collison List
        UnRegisterTouchedItems(collisionList, col1, col2);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // Debug.Log($"Collision Entered: ({this.gameObject.name}, {other.gameObject.name})");

        //Get the two Objects involved in the collision
        GameObject col1 = this.gameObject;
        GameObject col2 = other.gameObject;

        //Add to the collison List
        RegisterTouchedItems(collisionList, col1, col2);
    }

    void OnCollisionExit2D(Collision2D other)
    {
        // Debug.Log($"Collision Exit: ({this.gameObject.name}, {other.gameObject.name})");

        //Get the two Objects involved in the collision
        GameObject col1 = this.gameObject;
        GameObject col2 = other.gameObject;

        //Remove from the collison List
        UnRegisterTouchedItems(collisionList, col1, col2);
    }

    public static bool IsTouching(GameObject obj1, GameObject obj2)
    {
        int matchIndex = 0;
        return _itemExist(collisionList, obj1, obj2, ref matchIndex);
    }

    private void UnRegisterTouchedItems(List<KeyValuePair<GameObject, GameObject>> existingObj, GameObject col1, GameObject col2)
    {
        int matchIndex = 0;

        //Remove if it exist
        if (_itemExist(existingObj, col1, col2, ref matchIndex))
        {
            // Debug.Log($"Removed ({col1.gameObject.name}, {col2.gameObject.name}) collision/trigger.");
            existingObj.RemoveAt(matchIndex);
        }
    }

    private void RegisterTouchedItems(List<KeyValuePair<GameObject, GameObject>> existingObj, GameObject col1, GameObject col2)
    {
        int matchIndex = 0;

        //Add if it doesn't exist
        if (!_itemExist(existingObj, col1, col2, ref matchIndex))

        {
            KeyValuePair<GameObject, GameObject> item = new KeyValuePair<GameObject, GameObject>(col1, col2);
            existingObj.Add(item);
        }
    }

    private static bool _itemExist(List<KeyValuePair<GameObject, GameObject>> existingObj, GameObject col1,
    GameObject col2, ref int matchIndex)
    {
        bool existInList = false;
        for (int i = 0; i < existingObj.Count; i++)
        {
            //Check if key and value exist and vice versa
            if ((existingObj[i].Key == col1 && existingObj[i].Value == col2) ||
                    (existingObj[i].Key == col2 && existingObj[i].Value == col1))
            {
                existInList = true;
                matchIndex = i;
                break;
            }
        }
        return existInList;
    }

    public static void clearTouchedItems()
    {
        // Warning: Touched items are cleared across all environments!
        Debug.Log($"clearTouchedItems Warning: Don't use this if other environments still need to use the registered collisions.");
        collisionList.Clear();
    }

    public static void removeTouchedInstance(GameObject obj)
    {
        for (int i = collisionList.Count - 1; i >= 0; i--)
        {
            KeyValuePair<GameObject, GameObject> pair = collisionList[i];
            if (pair.Key == obj || pair.Value == obj)
            {
                // Debug.Log($"Removed instance {obj.name} from collision list at i={i}.");
                collisionList.RemoveAt(i);
            }
        }
    }
}