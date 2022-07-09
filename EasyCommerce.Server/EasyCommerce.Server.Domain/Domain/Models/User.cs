using EasyCommerce.Server.Shared.Enums;
using System;

namespace EasyCommerce.Server.Shared.Domain.Models;

public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Pin { get; set; }
    public ApplicationRolesUser ApplicationRolesUser { get; set; } = 0;
}
