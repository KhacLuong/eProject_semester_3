namespace ShradhaBook_API.Helpers;

public class MyStatusCode
{
    public static readonly int DUPLICATE_CODE = -1;
    public static readonly int DUPLICATE_NAME = -2;
    public static readonly int DUPLICATE_PHONE = -3;
    public static readonly int DUPLICATE_EMAIL = -4;
    public static readonly int DUPLICATE = -5;
    public static readonly int NOTFOUND = -6;


    public static readonly int FAILURE = 0;
    public static readonly int SUCCESS = 1;
    public static readonly string DUPLICATE_CODE_RESULT = "Duplicate Code";
    public static readonly string DUPLICATE_NAME_RESULT = "Duplicate Name";
    public static readonly string DUPLICATE_PHONE_RESULT = "Duplicate Phone";
    public static readonly string DUPLICATE_EMAIL_RESULT = "Duplicate Email";
    public static readonly string FAILURE_RESULT = "Failure";
    public static readonly string SUCCESS_RESULT = "Success";
    public static readonly string INTERN_SEVER_ERROR_RESULT = "Intern server error";
    public static readonly string NOT_FOUND_RESULT = "Not found";
    public static readonly string UPDATE_SUCCESS_RESULT = "Update successfully";
    public static readonly string UPDATE_FAILURE_RESULT = "Update failure";
    public static readonly string ADD_SUCCESS_RESULT = "Addition successfully";
    public static readonly string ADD_FAILURE_RESULT = "Addition failure";
    public static readonly string DELLETE_FAILURE_RESULT = "Delete failure";
    public static readonly string DELLETE_SUCCESS_RESULT = "Delete success";
}