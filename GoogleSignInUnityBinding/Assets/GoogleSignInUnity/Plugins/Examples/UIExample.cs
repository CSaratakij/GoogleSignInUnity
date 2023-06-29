using UnityEngine;
using UnityEngine.UI;

namespace GoogleSignInUnity.Example
{
    public class UIExample : MonoBehaviour
    {
        [Header("Setting")]
        [SerializeField] private string clientId;

        [Header("UI")]
        [SerializeField] private Button btnGoogleSignIn;
        [SerializeField] private Button btnOneTapSignIn;
        [SerializeField] private Button btnSignOut;

        private bool isSignIn = false;

        private void Awake()
        {
            if (GoogleSignIn.IsPlatformSupport())
            {
                GoogleSignIn.Initialize();
                GoogleSignIn.OnSignInResult += OnSignInResult;
            }
            else
            {
                Debug.LogError("GoogleSignIn : not support on this platform");
            }

            if (OneTapSignIn.IsPlatformSupport())
            {
                OneTapSignIn.Initialize();
                OneTapSignIn.OnSignInResult += OnSignInResult;
            }
            else
            {
                Debug.LogError("OneTapSignIn : not support on this platform");
            }
        }

        private void Start()
        {
            if (btnGoogleSignIn)
            {
                btnGoogleSignIn.onClick.AddListener(() =>
                {
                    GoogleSignIn.SignIn(clientId);
                });
            }

            if (btnOneTapSignIn)
            {
                btnOneTapSignIn.onClick.AddListener(() =>
                {
                    OneTapSignIn.SignIn(clientId);
                });
            }

            if (btnSignOut)
            {
                btnSignOut.onClick.AddListener(() =>
                {
                    GoogleSignIn.SignOut();
                    OneTapSignIn.SignOut();
                });
            }
        }

        private void OnDestroy()
        {
            if (GoogleSignIn.IsPlatformSupport())
            {
                GoogleSignIn.OnSignInResult -= OnSignInResult;
            }

            if (OneTapSignIn.IsPlatformSupport())
            {
                OneTapSignIn.OnSignInResult -= OnSignInResult;
            }
        }

        private void OnSignInResult(GoogleSignInResult result)
        {
            isSignIn = result.isSuccess;

            ShowButton(btnGoogleSignIn, !isSignIn);
            ShowButton(btnOneTapSignIn, !isSignIn);
            ShowButton(btnSignOut, isSignIn);

            Debug.Log(result);
        }

        private void ShowButton(Button button, bool isShow)
        {
            if (!button)
            {
                return;
            }

            var canvasGroup = button.GetComponent<CanvasGroup>();
            var layoutElement = button.GetComponent<LayoutElement>();

            if (canvasGroup)
            {
                canvasGroup.alpha = (isShow) ? 1 : 0;
                canvasGroup.interactable = isShow;
                canvasGroup.blocksRaycasts = isShow;
            }

            if (layoutElement)
            {
                layoutElement.ignoreLayout = !isShow;
            }
        }
    }
}
