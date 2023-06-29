using System;
using UnityEngine;

namespace GoogleSignInUnity
{
    internal class GoogleSignInCallback : AndroidJavaProxy
    {
        internal Action<GoogleSignInResult> OnSignInResult;

        public GoogleSignInCallback() : base("com.csaratakij.googlesignin.GoogleSignInCallback")
        {

        }

        internal void onSignInResult(AndroidJavaObject javaResult)
        {
            bool isSuccess = false;
            int failStatusCode = -1;
            string idToken = "";
            string displayName = "";
            string photoUrl = "";

            isSuccess = javaResult.Get<bool>("isSuccess");
            failStatusCode = javaResult.Get<int>("failStatusCode");
            idToken = javaResult.Get<string>("idToken");
            displayName = javaResult.Get<string>("displayName");
            photoUrl = javaResult.Get<string>("photoUrl");

            var bindingResult = new GoogleSignInResult()
            {
                isSuccess = isSuccess,
                failStatusCode = failStatusCode,
                idToken = idToken,
                displayName = displayName,
                photoUrl = photoUrl
            };

            OnSignInResult?.Invoke(bindingResult);
        }
    }
}

