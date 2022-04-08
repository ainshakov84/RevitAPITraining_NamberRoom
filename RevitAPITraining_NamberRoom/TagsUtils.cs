using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITraining_NamberRoom
{
    internal class TagsUtils
    {
        public static List<FamilySymbol> GetPipeTagTypes(ExternalCommandData commandData)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            var familySymbols = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_PipeTags)
                .OfClass(typeof(FamilySymbol))
                .Cast<FamilySymbol>()
                .ToList();
            return familySymbols;
        }

        public static List<FamilySymbol> GetViewTagTypes(ExternalCommandData commandData)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            var familySymbols = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_ViewportLabel)
                .OfClass(typeof(FamilySymbol))
                .Cast<FamilySymbol>()
                .ToList();
            return familySymbols;
        }

        public static List<RoomTagType> GetRoomTagTypes(ExternalCommandData commandData)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            var familySymbols = new FilteredElementCollector(doc)
                .OfClass(typeof(FamilySymbol))
                .OfCategory(BuiltInCategory.OST_RoomTags)
                .Cast<RoomTagType>()
                .ToList();
            return familySymbols;
        }
    }
}

