using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bagging.BagEdit.Models {
    public class ReseedModel : Model {

        public DateTime ReSeedDate { get; set; }
        [Key]
        public uint BagNo { get; set; }
        [Key]
        public uint OriginalSite { get; set; }
        public int Status { get; set; }
        public DateTime Timestamp { get; set; }

        public override string ToString() {
            return $"EntryNo:{UniqueIndentifier}, ReSeedDate: {ReSeedDate}, BagNo: {BagNo}, OriginalSite: {OriginalSite}, SiteId: {SiteId}, Notes: {Notes}";
        }
    }
}
