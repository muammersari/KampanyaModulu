using Core.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete
{
    public class Schedule : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ScheduleId { get; set; } //PK
        public string Name { get; set; } // Tarife Adı
        public int KDV { get; set; } // Tarife Kdv Oranı
        public int OIV { get; set; } // Tarife ÖİV Oranı

        public virtual List<Campaing> Campaings { get; set; }


    }
}
