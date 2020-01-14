using Boven.Areas.WData.BovenDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boven.Areas.Admin.Models
{
    public class SampleModel
    {
        public int SampleId { get; set; }
        public string SampleName { get; set; }
        public int ErrandId { get; set; }

        public static implicit operator SampleModel(Sample em)
        {
            var vm = new SampleModel
            {
                SampleId = em.SampleId,
                SampleName = em.SampleName,
                ErrandId = em.ErrandId
            };
            return vm;
        }
    }
}
