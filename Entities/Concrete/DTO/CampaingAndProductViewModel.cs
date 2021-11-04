using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete.DTO
{
    public class CampaingAndProductViewModel
    {
        public List<CampaingAndProduct> CampaingAndProducts { get; set; }
        public List<Campaing> Campaings { get; set; }
       
        public List<Product> Products { get; set; }
        public List<Schedule> Schedules { get; set; }
    }
}
