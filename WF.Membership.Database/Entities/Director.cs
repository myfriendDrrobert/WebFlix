using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.Membership.Database.Entities
{
    public class Director : IEntity
    {
        public Director()
        {
            Films = new HashSet<Film>();
        }
        public int Id { get; set; }

        [MaxLength(50)]
        public string? Name { get; set; }

        public virtual ICollection<Film>? Films { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
