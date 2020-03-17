using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bagging.Controller.Models {
    public class Model {

        /// <summary>
        /// Unique value that identifies the entity
        /// </summary>
        public uint UniqueIndentifier { get; set; }
        public UInt16 SiteId { get; set; }
        public string ProductType { get; set; }
        public string ProductPrefix { get; set; }
        public DateTime ProductDate { get; set; }
        public string Notes { get; set; }
        /// <summary>
        /// Product weight in kgs
        /// </summary>
        public decimal ProductWeight { get; set; }
        public int QualityFlag { get; set; }
    }
}
