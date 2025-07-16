namespace SecurityMasterClass.Models
{
    public class RoleAssignViewModel
    {

        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool RoleExist { get; set; } //Kullanıcının bu role sahipliği kontrol ediliyor
    }
}
