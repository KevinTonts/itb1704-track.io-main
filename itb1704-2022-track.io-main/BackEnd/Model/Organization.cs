using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Model
{
    public class Organization
    {
        [Column("id")]
        public int Id { get; set; }

    }
}