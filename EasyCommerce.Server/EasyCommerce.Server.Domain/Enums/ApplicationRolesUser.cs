namespace EasyCommerce.Server.Shared.Enums;

public enum ApplicationRolesUser
{
    Viewer = 0,        //Can only display the get methods -> not including users
    Editor = 1,       //Can only display the get, post and put methods -> not including users and customers(post and put)
    Support = 2,     //Can only display the get, post and put methods -> not including users
    Admin = 3      //Can show all methods
}
