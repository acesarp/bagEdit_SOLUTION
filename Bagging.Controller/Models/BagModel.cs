using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bagging.Controller.Models {
    public class BagModel : Model {

        public string QualityFlagName { get; set; }

        public override string ToString() {
            return $"SiteId:{SiteId}, BagNumber: {UniqueIndentifier}, ProductPrefix: {ProductPrefix}, ProductType: {ProductType}, ProductWeight: {ProductWeight}, Notes: {Notes}, QualityFlag: {QualityFlag}, QualityFlagName: {QualityFlagName}";
        }
    }
}
