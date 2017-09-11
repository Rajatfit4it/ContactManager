using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class PagingModel
    {
        public int TotalRecords { get; set; }

        public int CurrentPage { get; set; }

        public int RecordsPerPage { get; set; }

        public string ActionName { get; set; }

        public string ControllerName { get; set; }


        public int PageLength
        {
            get
            {

                double _pagelength = (double)TotalRecords / (double)RecordsPerPage;
                return (int)Math.Ceiling(_pagelength);

            }
        }

    }
}