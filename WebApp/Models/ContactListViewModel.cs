using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ViewModel;

namespace WebApp.Models
{
    public class ContactListViewModel
    {
        public IEnumerable<ContactVM> list { get; set; }

        public PagingModel pager { get; set; }
        
    }
}