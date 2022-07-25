namespace Proxfield.Text.Ini.Sample.Models
{
    public class User
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        //public DateTime? CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public Address Address { get; set; }
        public User()
        {
            this.UserId = Guid.NewGuid().ToString();
            this.IsDeleted = false;
            //this.CreatedDate = DateTime.Now;
        }
    }
}
