namespace KNC.Helper
{
    public class Helper
    {
        public string GenerateStudentCode(int id,string program,int count)
        {
            string code = "STU" + id.ToString("D4");
            return code;
        }
    }
}
