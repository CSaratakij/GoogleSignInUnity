namespace GoogleSignInUnity
{
    public struct GoogleSignInResult
    {
        public bool isSuccess;
        public int failStatusCode;
        public string idToken;
        public string displayName;
        public string photoUrl;

        public override string ToString()
        {
            return string.Format("{ isSuccess: {0}, failStatusCode: {1}, idToken: {2}, displayName: {3}, photoUrl: {4} }", isSuccess, failStatusCode, idToken, displayName, photoUrl);
        }
    }
}
