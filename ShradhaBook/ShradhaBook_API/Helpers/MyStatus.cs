namespace ShradhaBook_API.Helpers;

public class MyStatus
{
    public static readonly int ACTIVE = 1;
    public static readonly int INACTIVE = 0;
    public static string ACTIVE_RESULT = "Active";
    public static readonly string INACTIVE_RESULT = "Inactive";


    public static string changeStatusCat(int status)
    {
        if (status == 1) return ACTIVE_RESULT;
        return INACTIVE_RESULT;
    }

    public static int changeStatusCat(string status)
    {
        if (status.Equals(ACTIVE_RESULT))
        {
            return ACTIVE;
        }

        if (status.Equals(INACTIVE_RESULT))
        {
            status.Equals(INACTIVE_RESULT);
            return INACTIVE;
        }

        return -1;
    }

    public static List<string> getListStatusCategory()
    {
        return new List<string>
        {
            ACTIVE_RESULT, INACTIVE_RESULT
        };
    }
}