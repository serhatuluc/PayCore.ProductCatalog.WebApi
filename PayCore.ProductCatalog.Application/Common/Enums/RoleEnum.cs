using PayCore.ProductCatalog.Domain.Entities;
using System.ComponentModel;


namespace PayCore.ProductCatalog.Application.Enums
{
    public enum RoleEnum
    {
        [Description(Role.Admin)]
        Admin = 1,

        [Description(Role.User)]
        User = 2
    }
}
