using UnityEngine;

namespace Plugboard
{
#if UNITY_EDITOR
    [ExecuteInEditMode]
#endif
    public class Dispatcher : MonoBehaviour
    {
        [SerializeField, HideInInspector]
        public Events Events;

        private void Awake()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying && Events == null)
            {
                Events = Events.LoadAsset();
            }
            Debug.Assert(Events != null);
#endif
        }
    }
}
