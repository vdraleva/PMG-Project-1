namespace PMG.Domain
{
    using Microsoft.AspNetCore.Identity;
    using System;

    public class PMGUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UCN { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Role { get; set; }
        public string BookmarkId{ get; set; }

        public Bookmark Bookmark { get; set; }
    }
}