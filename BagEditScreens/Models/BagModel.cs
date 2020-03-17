using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bagging.BagEdit.Models {
    public class BagModel : Model {

        internal string QualityFlagName { get; set; }

        public override string ToString() {
            return $"SiteId:{SiteId}, BagNumber: {UniqueIndentifier}, ProductType: {ProductType}, ProductWeight: {ProductWeight}, Notes: {Notes}, QualityFlag: {QualityFlag}, QualityFlagName: {QualityFlagName}";
        }
    }
}
