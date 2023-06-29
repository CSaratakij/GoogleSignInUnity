using System;
using UnityEngine;

namespace GoogleSignInUnity
{
    public static class OneTapSignIn
    {
        public static Action<GoogleSignInResult> OnSignInResult;

        private static GoogleSignInCallback callback;
        private static AndroidJavaObject unityActivity;
        private static AndroidJavaClass oneTapSignInJavaClass;

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
            if (oneTapSignInJavaClass == null)
            {
                return false;
            }

            return oneTapSignInJavaClass.CallStatic<bool>("IsInitialize");
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

            oneTapSignInJavaClass = new AndroidJavaClass("com.csaratakij.googlesignin.OneTapSignInActivity");
            oneTapSignInJavaClass.CallStatic("Initialize", unityActivity, callback);
        }

        public static void SignIn(string clientId)
        {
            oneTapSignInJavaClass?.CallStatic("SignIn", clientId);
        }

        public static void SignOut()
        {
            oneTapSignInJavaClass?.CallStatic("SignOut");
        }
    }
}
