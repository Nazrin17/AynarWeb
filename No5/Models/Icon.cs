
namespace No5.Models
{
    public class Icon
    {
        public int Id{ get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
    }
}
