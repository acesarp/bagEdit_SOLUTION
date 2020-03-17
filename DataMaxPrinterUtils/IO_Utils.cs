using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace Bagging.DataMaxPrinterUtils {
    public static class IO_Utils {

        public static Image GetImage(string path) {
            try {
                return Image.FromFile(path);
            }catch(OutOfMemoryException ex) {
                throw ex;
            }catch(ArgumentException ex) {
                throw ex;
            }
            catch(FileNotFoundException ex) {
                throw ex;
            }

        } 
    }
}
