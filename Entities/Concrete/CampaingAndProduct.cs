using Core.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete
{
    public class CampaingAndProduct : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CampaingAndProductId { get; set; } //PK

        public int CampaingId { get; set; } //Kampanya Id
        public virtual Campaing Campaing { get; set; }

        public int ProductId { get; set; } //Ürün Id
        public virtual Product Product { get; set; }


    }
}
