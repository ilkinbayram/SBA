using System;

using Core.Resources.Enums;




namespace Core.Abstract
{
    public interface IUserBaseEntity
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        string ProfilePhotoPath { get; set; }
        string WallpaperPath { get; set; }
        byte[] PasswordSalt { get; set; }
        byte[] PasswordHash { get; set; }
        string PhoneNumberPrefix { get; set; }
        string PhoneNumber { get; set; }
        DateTime? Birthday { get; set; }
        string SecurityToken { get; set; }
    }
}
