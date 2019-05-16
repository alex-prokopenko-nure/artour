using System;
using System.Collections.Generic;
using System.Text;

namespace Artour.Mobile.Models
{
    public enum MenuItemType
    {
        Browse,
        About,
        Login,
        Register,
        Visit
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
