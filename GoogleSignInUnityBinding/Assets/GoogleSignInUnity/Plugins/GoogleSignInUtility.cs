using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoogleSignInUnity
{
    public static class GoogleSignInUtility
    {
        internal static AndroidJavaObject GetCurrentUnityActivity()
        {
            using (var unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                return unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
            }
        }
    }
}
