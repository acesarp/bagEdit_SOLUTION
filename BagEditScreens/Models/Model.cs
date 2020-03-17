using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bagging.BagEdit.Models {
    public class Model {

        /// <summary>
        /// Unique value that identifies the entity
        /// </summary>
        public uint UniqueIndentifier { get; set; }
        internal UInt16 SiteId { get; set; }
        internal UInt16 ProductType { get; set; }
        internal DateTime ProductDate { get; set; }

        internal string Notes { get; set; }
        internal decimal ProductWeight { get; set; }
        internal int QualityFlag { get; set; }
    }
}
