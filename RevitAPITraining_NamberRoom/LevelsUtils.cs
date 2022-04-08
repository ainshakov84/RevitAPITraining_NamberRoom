using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;

namespace RevitAPITraining_NamberRoom
{
    internal class LevelsUtils
    {
        public static List<Level> GetLevels(ExternalCommandData commandData)
        {
            var doc = commandData.Application.ActiveUIDocument.Document;

            List<Level> levels = new FilteredElementCollector(doc)
                                                       .OfClass(typeof(Level))
                                                       .Cast<Level>()
                                                       .ToList();
            return levels;
        }
    }
}

