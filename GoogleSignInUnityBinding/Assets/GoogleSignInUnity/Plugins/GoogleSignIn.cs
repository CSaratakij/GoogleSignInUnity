using System;
using UnityEngine;

namespace GoogleSignInUnity
{
    public static class GoogleSignIn
    {
        public static Action<GoogleSignInResult> OnSignInResult;

        private static GoogleSignInCallback callback;
        private static AndroidJavaObject unityActivity;
        private static AndroidJavaClass googleSignInJavaClass;

        private static void DispatchResult(GoogleSignInResult result)
        {
            OnSignInResult?.Invoke(result);
        }

        public static bool IsPlatformSupport()
        {
            #if UNITY_ANDROID && !UNITY_EDITOR
                return true;
            #else
                return false;
            #endif
        }

        public static bool IsInitialized()
        {
            if (googleSignInJavaClass == null)
            {
                return false;
            }

            return googleSignInJavaClass.CallStatic<bool>("IsInitialize");
        }

        public static void Initialize()
        {
            if (IsInitialized())
            {
                return;
            }

            callback = new GoogleSignInCallback();
            callback.OnSignInResult += DispatchResult;

            unityActivity = GoogleSignInUtility.GetCurrentUnityActivity();

            googleSignInJavaClass = new AndroidJavaClass("com.csaratakij.googlesignin.GoogleSignInActivity");
            googleSignInJavaClass.CallStatic("Initialize", unityActivity, callback);
        }

        public static void SignIn(string clientId)
        {
            googleSignInJavaClass?.CallStatic("SignIn", clientId);
        }

        public static void SignOut()
        {
            googleSignInJavaClass?.CallStatic("SignOut");
        }
    }
}
