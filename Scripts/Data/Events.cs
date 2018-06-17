using System;
using UnityEngine;

namespace Plugboard
{
    /// <summary>
    /// Holds list of <see cref="Data.EventType"/>s.
    /// </summary>
    public sealed class Events : ScriptableObject
    {
        /// <summary>
        /// All defined event types, set via custom property drawer.
        /// </summary>
        [SerializeField, HideInInspector]
        private Data.EventType[] eventTypes;

        /// <summary>
        /// Finds an <see cref="Data.EventType"/> by name. Not very efficient.
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
