
using Core.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete
{
    public class Campaing : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CampaingId { get; set; } //PK

        public string CampaingName { get; set; } // Kampanya Adı
        public string CampaingDescription { get; set; } // Kampanya Açılaması

        public int ScheduleId { get; set; } //Tarife Id
        public virtual Schedule Schedule { get; set; }

        public int Price { get; set; } //Kampanya Fiyatı

    }
}
