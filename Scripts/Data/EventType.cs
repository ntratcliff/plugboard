namespace Plugboard.Data
{
    public struct EventType
    {
        /// <summary>
        /// The human-readable name of this event.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The unique ID of this event.
        /// </summary>
        public int ID { get; private set; }
        
        /// <summary>
        /// Defines a new event type
        /// </summary>
        /// <param name="name">The human-readable name of the event</param>
        /// <param name="id">The unique ID of this event</param>
        internal EventType(string name, int id)
        {
            Name = name;
            ID = id;
        }
    }
}