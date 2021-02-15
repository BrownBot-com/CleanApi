using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Api.Helpers.Queries
{
    public class QueryParameters
    {
        public QueryParameters()
        {

        }

        public QueryParameters(string filter, string sort, string pageSize, string page)
        {
            Filter = filter;
            Sort = sort;

            uint pageSizeVal = 100;
            if (uint.TryParse(pageSize, out pageSizeVal))
            {
                PageSize = pageSizeVal;
            }

            uint pageVal = 0;
            if (uint.TryParse(page, out pageVal))
            {
                this.Page = pageVal;
            }
        }

        private uint _page = 1;
        public uint Page 
        {
            get { return _page; }
            set
            {
                if (value <= 0)
                { 
                    _page = 1; 
                }
                else
                {
                    _page = value;
                }
            }
        }
        public uint PageSize { get; set; } = 100;
        public string Filter { get; set; }
        public string Sort { get; set; }

        public int FilteredCount { get; set; }

        public uint PageFirstItemNumber
        {
            get
            {
                return (Page - 1)  * PageSize;
            }
        }
    }
}
