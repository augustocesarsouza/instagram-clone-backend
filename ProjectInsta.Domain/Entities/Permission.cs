using ProjectInsta.Domain.Validations;

namespace ProjectInsta.Domain.Entities
{
    public class Permission
    {
        public int Id { get; private set; }
        public string VisualName { get; private set; }
        public string PermissionName { get; private set; }

        public ICollection<UserPermission> UserPermissions { get; set; }

        public Permission()
        {
            
        }

        public Permission(string visualName, string permissionName)
        {
            Validator(visualName, permissionName);
        }

        public void Validator(string visualName, string permissionName)
        {
            DomainValidationException.When(string.IsNullOrEmpty(visualName), "Visual Name deve ser informado");
            DomainValidationException.When(string.IsNullOrEmpty(permissionName), "Permission Nae deve ser informado");

            VisualName = visualName;
            PermissionName = permissionName;
            UserPermissions = new List<UserPermission>();
        }
    }
}
