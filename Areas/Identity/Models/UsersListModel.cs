using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Boven.Areas.Identity.Models
{
    public class UsersListModel
    {
        public List<User> Users { get; set; }

        public UsersListModel()
        {
            this.Users = new List<User>();
        }
    }


    public class User
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public List<SelectListItem> Roles { get; set; }


    };
}
