/*
 * This class is designed to take a normal C# dictionary
 * and convert it using a callback receiver to a dictionary
 * that is serializable by the JSON Utility
 * 
 * Author: Robot and I Team
 * Date: 1/23/2023
 */

using UnityEngine;
using System.Collections.Generic;

namespace GameMechanics
{
    [System.Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField] private List<TKey> keys = new List<TKey>();
        [SerializeField] private List<TValue> values = new List<TValue>();

        // Converts the dictionary to a list
        public void OnBeforeSerialize()
        {
            keys.Clear();
            values.Clear();
            foreach (KeyValuePair<TKey, TValue> pair in this)
            {
                keys.Add(pair.Key);
                values.Add(pair.Value);
            }
        }

        // Converts the list back to a dictionary
        public void OnAfterDeserialize()
        {
            Clear(); // Clear the dictionary

            if (keys.Count != values.Count)
            {
                Debug.LogError("Keys and Values count mismatch");
            }

            for (int i = 0; i < keys.Count; ++i)
            {
                Add(keys[i], values[i]); // Add key and value pairs into the dictionary
            }
        }
    }
}