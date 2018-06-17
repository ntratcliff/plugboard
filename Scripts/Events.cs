using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Plugboard
{
    /// <summary>
    /// Holds list of <see cref="Data.EventType"/>s.
    /// </summary>
    public sealed class Events : ScriptableObject
    {
        private const string DEFAULT_OBJECT_PATH = "Assets/Plugins/plugboard/Data/Events.asset";
        /// <summary>
        /// Finds an <see cref="Events"/> scriptable object in the AssetDatabase and loads it.
        /// Only do this in the editor.
        /// </summary>
        /// <returns>The loaded Events object</returns>
        internal static Events LoadAsset()
        {
#if UNITY_EDITOR
            // find Events asset
            string[] assetGUIDs = AssetDatabase.FindAssets("t:Events");

            if(assetGUIDs.Length > 1)
            {
                Debug.LogWarning("[Plugboard] More than one Events asset found in project! There should only ever be one.");
            }
            else if(assetGUIDs.Length == 0)
            {
                // create asset
                AssetDatabase.CreateAsset(CreateInstance<Events>(), DEFAULT_OBJECT_PATH);
                AssetDatabase.SaveAssets();
                return AssetDatabase.LoadAssetAtPath<Events>(DEFAULT_OBJECT_PATH);
            }

            return AssetDatabase.LoadAssetAtPath<Events>(AssetDatabase.GUIDToAssetPath(assetGUIDs[0]));
#else
            Debug.LogError("[Plugboard] Attempt to load Events object from outside of editor! This should never happen.");
            return null;
#endif
        }

        /// <summary>
        /// All defined event types, set via custom property drawer.
        /// </summary>
        [SerializeField]
        private Data.EventType[] eventTypes = new Data.EventType[0];

        /// <summary>
        /// Finds an <see cref="Data.EventType"/> by name.
        /// </summary>
        /// <param name="name">The name of the event</param>
        public Data.EventType FindByName(string name)
        {
            for (int i = 0; i < eventTypes.Length; i++)
            {
                if (eventTypes[i].Name.Equals(name))
                    return eventTypes[i];
            }

            // TODO: NoSuchEvent exception or something like that
            throw new Exception("No event with name: " + name);
        }

        /// <summary>
        /// Gets an <see cref="Data.EventType"/> by ID.
        /// </summary>
        public Data.EventType this [int i]
        {
            get { return eventTypes[i]; }
        }

        /// <summary>
        /// Finds an EventType by name. Not very efficient.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Data.EventType this [string name]
        {
            get { return FindByName(name); }
        }
    }
}
