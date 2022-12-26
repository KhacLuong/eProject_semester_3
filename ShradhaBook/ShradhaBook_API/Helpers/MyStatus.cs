namespace ShradhaBook_API.Helpers
{
    public class MyStatus
    {
        public readonly static int ACTIVE = 1;
        public readonly static int INACTIVE = 0;
        public  static string ACTIVE_RESULT = "Active";
        public readonly static string INACTIVE_RESULT = "Inactive";


        public static string changeStatusCat(int status)
        {
            if (status==1)
            {
                return MyStatus.ACTIVE_RESULT;
            }
            return MyStatus.INACTIVE_RESULT;
        }

        public static int changeStatusCat(string status)
        {
            if (status.Equals(ACTIVE_RESULT))
            {
                return MyStatus.ACTIVE;
            }
            else if (status.Equals(INACTIVE_RESULT))
            {
                status.Equals(INACTIVE_RESULT);
                return MyStatus.INACTIVE;
            }
            return -1;
        }
        public  static List<string> getListStatusCategory()
        {


            return new List<string>
                {
                    ACTIVE_RESULT,INACTIVE_RESULT
                };

        }

    }
}
