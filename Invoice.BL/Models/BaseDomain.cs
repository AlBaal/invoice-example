using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.BL.Models
{
    public abstract class BaseDomain
    {
        public int Id { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
