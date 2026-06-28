namespace UseAppSettings.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }  
        public int? DeptId { get; set; }
        public int? Manager { get; set; }
        public DateOnly? CreatedDate { get; set; }
        public string? MobileNo { get; set; }
        public string? Email { get; set; }
    }
}
