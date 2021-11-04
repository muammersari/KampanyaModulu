using Core.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete
{
    public class Product : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; } //PK
        public string Name { get; set; } // Ürün Adı
        public int KDV { get; set; } // Ürün Kdv Oranı
        public virtual List<CampaingAndProduct> Campaings { get; set; }
        public string Amout { get; set; }
    }
}
