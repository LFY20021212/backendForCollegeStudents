namespace backendForCollegeStudents.Models
{
    public class VerificationCode
    {
        public int Id { get; set; }
        public string CodeType { get; set; }
        public string CodeContent { get; set; }
        public string User { get; set; }
    }
}
