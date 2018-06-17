﻿namespace Plugboard.Data
{
    // TODO: give this a better name, like EventInfo?
    [System.Serializable]
    public struct EventType
    {
        /// <summary>
        /// The human-readable name of this event.
        /// </summary>
        [UnityEngine.SerializeField]
        internal string name; 
        public string Name { get { return name; } }

        /// <summary>
        /// The unique ID of this event.
        /// </summary>
        [UnityEngine.SerializeField]
        internal int id;
        public int ID { get { return id; } }
    }
}