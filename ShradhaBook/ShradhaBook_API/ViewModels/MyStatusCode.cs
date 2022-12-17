namespace ShradhaBook_API.ViewModels
{
    public class MyStatusCode
    {
        public readonly static int DUPLICATE_CODE = -1;
        public readonly static int DUPLICATE_NAME = -2;
        public readonly static int FAILURE = 0;
        public readonly static int SUCCESS = 1;
        public readonly static string DUPLICATE_CODE_RESULT = "Duplicate Code";
        public readonly static string DUPLICATE_NAME_RESULT = "Duplicate Name";
        public readonly static string FAILURE_RESULT = "Failure";
        public readonly static string SUCCESS_RESULT = "Success";
        public readonly static string INTERN_SEVER_ERROR_RESULT = "Intern server error";

    }
}
