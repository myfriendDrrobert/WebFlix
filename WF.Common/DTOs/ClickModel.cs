using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.Common.DTOs
{
    public record ClickModel
    {
        public ClickModel(string pageType, int id)
        {
            this.pageType = pageType;
            this.id = id;
        }
        public string pageType { get; set; }

        public int id { get; set; }

    }
}
