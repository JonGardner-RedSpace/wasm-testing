using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;

namespace PBS.Springroll
{
    static class Application 
    {
        [DllImport("__Internal"), RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static extern void Unity_OnBeforeSceneLoad();
        [DllImport("__Internal"), RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static extern void Unity_OnAfterSceneLoad();

        [DllImport("__Internal")]
        private static extern void AddStateEvent(string name, Action<float, float> callback);


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void OnBeforeSceneLoadRuntimeMethod()
        {
            AddStateEvent("pause", onPausedStateChange);
        }

        [MonoPInvokeCallback(typeof(Action))]
        public static void onVolumeChange(float val, float last)
        {
            Debug.Log($"this worked {val}: {last}");
        }

        [MonoPInvokeCallback(typeof(Action))]
        public static void onPausedStateChange(float val, float last)
        {
            Debug.Log($"this worked {val}: {last}");
        }

    }
}


