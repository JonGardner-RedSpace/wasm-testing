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

        // https://stackoverflow.com/questions/26882526/generic-pinvoke-in-c-sharp <-- Somthing similar
        [DllImport("__Internal")] //TODO: write a generic wrapper for this to ensure type safety. 
        private static extern void AddStateCallback(string name, string sig, Action<float, float> callback);

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void OnBeforeSceneLoadRuntimeMethod()
        {
            AddStateCallback("soundVolume", "vff", onVolumeChange);
        }

        [MonoPInvokeCallback(typeof(Action<float, float>))]
        public static void onVolumeChange(float val, float last)
        {
            Debug.Log($"this worked!! {val}: {last}");
        }
    }
}


