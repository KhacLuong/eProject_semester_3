namespace ShradhaBook_API.Helpers
{
    public class MyStatusCode
    {
        public readonly static int DUPLICATE_CODE = -1;
        public readonly static int DUPLICATE_NAME = -2;
        public readonly static int DUPLICATE_PHONE = -3;
        public readonly static int DUPLICATE_EMAIL = -4;
        public readonly static int DUPLICATE=-5;

        public readonly static int FAILURE = 0;
        public readonly static int SUCCESS = 1;
        public readonly static string DUPLICATE_CODE_RESULT = "Duplicate Code";
        public readonly static string DUPLICATE_NAME_RESULT = "Duplicate Name";
        public readonly static string DUPLICATE_PHONE_RESULT = "Duplicate Phone";
        public readonly static string DUPLICATE_EMAIL_RESULT = "Duplicate Email";
        public readonly static string FAILURE_RESULT = "Failure";
        public readonly static string SUCCESS_RESULT = "Success";
        public readonly static string INTERN_SEVER_ERROR_RESULT = "Intern server error";
        public readonly static string NOT_FOUND_RESULT = "Not found";
        public readonly static string UPDATE_SUCCESS_RESULT = "Update successfully";
        public readonly static string UPDATE_FAILURE_RESULT = "Update failure";
        public readonly static string ADD_SUCCESS_RESULT = "Addition successfully";
        public readonly static string ADD_FAILURE_RESULT = "Addition failure";
        public readonly static string DELLETE_FAILURE_RESULT = "Delete failure";
        public readonly static string DELLETE_SUCCESS_RESULT = "Delete success";





    }
}
