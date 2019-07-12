using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using DCEStudyTools.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


using static DCEStudyTools.Utils.PropertyValueSetter;
using static DCEStudyTools.Properties.Settings;
using System.Text.RegularExpressions;

namespace DCEStudyTools.Utils
{
    public class ColumnFamily
    {
        readonly static string rectColumnPath =
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
            Path.DirectorySeparatorChar +
            Default.FAMILY_NAME_RECT_COLUMN + Default.RVT_FAMILY_EXTENSION;

        readonly static string rondColumnPath =
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
            Path.DirectorySeparatorChar +
            Default.FAMILY_NAME_ROND_COLUMN + Default.RVT_FAMILY_EXTENSION;

        const StructuralType STCOLUMN
          = StructuralType.Column;

        private List<FamilySymbol> _rectColumnTypesList = new List<FamilySymbol>();
        private List<FamilySymbol> _rondColumnTypesList = new List<FamilySymbol>();
        private Family _rectColumnfamily;
        private Family _rondColumnfamily;
        private Document _doc;

        public Family GetRectColumnFamily
        {
            get
            {
                return _rectColumnfamily;
            }
        }
        public Family GetRondColumnFamily
        {
            get
            {
                return _rondColumnfamily;
            }
        }
        public List<FamilySymbol> RectColumnTypesList
        {
            get
            {
                return _rectColumnTypesList;
            }
        }
        public List<FamilySymbol> RondColumnTypesList
        {
            get
            {
                return _rondColumnTypesList;
            }
        }

        public ColumnFamily(Document doc)
        {
            _doc = doc;
            // Retrieve the Column family "POT-BA-RECT"
            Family rectColumn =
                (from fa in new FilteredElementCollector(doc)
                .OfClass(typeof(Family)).Cast<Family>()
                 where fa.Name.Equals(Default.FAMILY_NAME_RECT_COLUMN)
                 select fa)
                .FirstOrDefault();

            // if the family doesn't exist, then load family
            if (null == rectColumn)
            {
                Transaction t = new Transaction(doc);
                t.Start("Load family");
                File.WriteAllBytes(rectColumnPath, Resources.POT_BA_RECT);
                if (!doc.LoadFamily(rectColumnPath, out rectColumn))
                {
                    string message1 = "Unable to load '" + rectColumnPath + "'.";
                    TaskDialog.Show("Revit", message1);
                }
                TaskDialog.Show("Revit", $"The family is writen to {rectColumnPath}");
                //File.Delete(path);
                t.Commit();
            }

            // Retrieve the Column family "POT-BA-ROND"
            Family rondColumn =
                (from fa in new FilteredElementCollector(doc)
                .OfClass(typeof(Family)).Cast<Family>()
                 where fa.Name.Equals(Default.FAMILY_NAME_ROND_COLUMN)
                 select fa)
                .FirstOrDefault();

            // if the family doesn't exist, then load family
            if (null == rondColumn)
            {
                Transaction t = new Transaction(doc);
                t.Start("Load family");
                File.WriteAllBytes(rondColumnPath, Resources.POT_BA_ROND);
                if (!doc.LoadFamily(rondColumnPath, out rondColumn))
                {
                    string message1 = "Unable to load '" + rondColumnPath + "'.";
                    TaskDialog.Show("Revit", message1);
                }
                TaskDialog.Show("Revit", $"The family is writen to {rondColumnPath}");
                //File.Delete(path);
                t.Commit();
            }

            // Retrieve all the Column types of the family "POT-BA-RECT"
            List<FamilySymbol> rectColumnTypesList =
                    (from col in new FilteredElementCollector(_doc)
                     .OfClass(typeof(FamilySymbol))
                     .OfCategory(BuiltInCategory.OST_StructuralColumns)
                     .Cast<FamilySymbol>()
                     where col.Family.Id == rectColumn.Id
                     select col)
                     .ToList();

            // Retrieve all the Beam types of the family "POT-BA-ROND"
            List<FamilySymbol> rondColumnTypesList =
                    (from col in new FilteredElementCollector(_doc)
                     .OfClass(typeof(FamilySymbol))
                     .OfCategory(BuiltInCategory.OST_StructuralColumns)
                     .Cast<FamilySymbol>()
                     where col.Family.Id == rondColumn.Id
                     select col)
                     .ToList();

            _rectColumnfamily = rectColumn;
            _rondColumnfamily = rondColumn;
            _rectColumnTypesList = rectColumnTypesList;
            _rondColumnTypesList = rondColumnTypesList;
        }

        public static void GetRectColumnSymbolProperties(FamilySymbol colSymbol, out string colMat, out double colHeight, out double colWidth)
        {
            colMat =
                (from Parameter pr in colSymbol.Parameters
                 where pr.Definition.Name.Equals(Default.PARA_NAME_STR_MATERIAL)
                 select pr)
                 .First()
                 .AsValueString();

            colHeight = UnitUtils.Convert(
                    (from Parameter pr in colSymbol.Parameters
                     where pr.Definition.Name.Equals(Default.PARA_NAME_DIM_HEIGHT)
                     select pr)
                     .First()
                     .AsDouble(),
                    DisplayUnitType.DUT_DECIMAL_FEET,
                    DisplayUnitType.DUT_CENTIMETERS);
            colWidth = UnitUtils.Convert(
                    (from Parameter pr in colSymbol.Parameters
                     where pr.Definition.Name.Equals(Default.PARA_NAME_DIM_WIDTH)
                     select pr)
                     .First()
                     .AsDouble(),
                    DisplayUnitType.DUT_DECIMAL_FEET,
                    DisplayUnitType.DUT_CENTIMETERS);
        }

        public static void GetRondColumnSymbolProperties(FamilySymbol colSymbol, out string colMat, out double colDiameter)
        {
            colMat =
                (from Parameter pr in colSymbol.Parameters
                 where pr.Definition.Name.Equals(Default.PARA_NAME_STR_MATERIAL)
                 select pr)
                 .First()
                 .AsValueString();

            colDiameter = UnitUtils.Convert(
                    (from Parameter pr in colSymbol.Parameters
                     where pr.Definition.Name.Equals(Default.PARA_NAME_DIM_DIAMETER)
                     select pr)
                     .First()
                     .AsDouble(),
                    DisplayUnitType.DUT_DECIMAL_FEET,
                    DisplayUnitType.DUT_CENTIMETERS);
        }

        public void AdjustWholeColumnFamilyTypeName()
        {
            foreach (FamilySymbol rectColumnSymbol in RectColumnTypesList)
            {
                AdjustColumnTypeName(rectColumnSymbol);
            }
            foreach (FamilySymbol rondColumnSymbol in RondColumnTypesList)
            {
                AdjustColumnTypeName(rondColumnSymbol);
            }
        }

        public void AdjustColumnTypeName(FamilySymbol colSymbol)
        {
            double colHeight, colWidth, colDiameter;
            string colMat;

            string targetColumnTypeName;

            if (colSymbol.Family.Name.Equals(Default.FAMILY_NAME_RECT_COLUMN))
            {
                GetRectColumnSymbolProperties(colSymbol, out colMat, out colHeight, out colWidth);
            
                string matSign = RvtMaterial.GetMatSyntaxe(colMat);

                targetColumnTypeName = $"{Default.SYNT_COLUMN}-{matSign}-{colWidth}x{colHeight}";
                
            }
            else
            {
                GetRondColumnSymbolProperties(colSymbol, out colMat, out colDiameter);

                string matSign = RvtMaterial.GetMatSyntaxe(colMat);

                targetColumnTypeName = $"{Default.SYNT_COLUMN}-{matSign}-D{colDiameter}";
            }

            if (!colSymbol.Name.Equals(targetColumnTypeName))
            {
                Transaction t = new Transaction(_doc);
                t.Start("Change column type name");
                colSymbol.Name = targetColumnTypeName;
                t.Commit();
            }
        }

        public void AdjustWholeColumnFamilyProperties()
        {
            foreach (FamilySymbol colSymbol in RectColumnTypesList)
            {
                // if the symbol name is in format of "ss+-BAdd-dd+xdd+", update the properties
                Regex rectRegex = new Regex(@"[A-Z]{2,}-BA\d{2}-\d{2,}x\d{2,}");
                Regex rondRegex = new Regex(@"[A-Z]{2,}-BA\d{2}-D\d{2,}");
                Match rectMatch = rectRegex.Match(colSymbol.Name);
                Match rondMatch = rondRegex.Match(colSymbol.Name);
                if (rectMatch.Success || rondMatch.Success)
                {
                    AdjustColumnTypeProperties(colSymbol);
                }
            }
        }

        public void AdjustColumnTypeProperties(FamilySymbol colSymbol)
        {
            string actualTypeName = colSymbol.Name;

            string targetMatSign = actualTypeName.Between("-", "-");
            string targetColMat = RvtMaterial.GetMatName(targetMatSign);

            double targetWidth, targetHeight, targetDiameter;

            string dimensionString = actualTypeName.After("-");

            double colHeight, colWidth, colDiameter;
            string colMat;

            if (colSymbol.Family.Name.Equals(Default.FAMILY_NAME_RECT_COLUMN))
            {
                targetWidth = Convert.ToDouble(dimensionString.Before("x"));
                targetHeight = Convert.ToDouble(dimensionString.After("x"));

                GetRectColumnSymbolProperties(colSymbol, out colMat, out colHeight, out colWidth);

                if (!colMat.Equals(targetColMat))
                {
                    Material targetMaterial =
                        (from Material m in new FilteredElementCollector(_doc)
                         .OfClass(typeof(Material))
                         where m != null
                         select m)
                         .Cast<Material>()
                         .FirstOrDefault(m => m.Name.Equals(targetColMat));

                    Transaction t = new Transaction(_doc);
                    t.Start("Change column material property");
                    SetElementIdValueTo(colSymbol,
                        Default.PARA_NAME_STR_MATERIAL,
                        targetMaterial.Id);
                    t.Commit();
                }

                if (colHeight != targetHeight)
                {
                    Transaction t = new Transaction(_doc);
                    t.Start("Change column height property");
                    SetDoubleValueTo(colSymbol,
                        Default.PARA_NAME_DIM_HEIGHT,
                        UnitUtils.Convert(targetHeight, DisplayUnitType.DUT_CENTIMETERS, DisplayUnitType.DUT_DECIMAL_FEET));
                    t.Commit();
                }

                if (colWidth != targetWidth)
                {
                    Transaction t = new Transaction(_doc);
                    t.Start("Change column width property");
                    SetDoubleValueTo(colSymbol,
                        Default.PARA_NAME_DIM_WIDTH,
                        UnitUtils.Convert(targetWidth, DisplayUnitType.DUT_CENTIMETERS, DisplayUnitType.DUT_DECIMAL_FEET));
                    t.Commit();
                }
            }
            else
            {
                targetDiameter = Convert.ToDouble(dimensionString.After(Default.KEYWORD_ROND_COLUMN_DIM));

                GetRondColumnSymbolProperties(colSymbol, out colMat, out colDiameter);

                if (!colMat.Equals(targetColMat))
                {
                    Material targetMaterial =
                        (from Material m in new FilteredElementCollector(_doc)
                         .OfClass(typeof(Material))
                         where m != null
                         select m)
                         .Cast<Material>()
                         .FirstOrDefault(m => m.Name.Equals(targetColMat));

                    Transaction t = new Transaction(_doc);
                    t.Start("Change column material property");
                    SetElementIdValueTo(colSymbol,
                        Default.PARA_NAME_STR_MATERIAL,
                        targetMaterial.Id);
                    t.Commit();
                }

                if (colDiameter != targetDiameter)
                {
                    Transaction t = new Transaction(_doc);
                    t.Start("Change column diameter property");
                    SetDoubleValueTo(colSymbol,
                        Default.PARA_NAME_DIM_HEIGHT,
                        UnitUtils.Convert(colDiameter, DisplayUnitType.DUT_CENTIMETERS, DisplayUnitType.DUT_DECIMAL_FEET));
                    t.Commit();
                }
            }
        }
    }
}
