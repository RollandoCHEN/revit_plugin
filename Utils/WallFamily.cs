using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static DCEStudyTools.Properties.Settings;

namespace DCEStudyTools.Utils
{
    public class WallFamily
    {
        private List<WallType> _wallTypesList = new List<WallType>();
        private Document _doc;
        public List<WallType> WallTypesList
        {
            get
            {
                return _wallTypesList;
            }
        }

        public WallFamily(Document doc)
        {
            _doc = doc;

            // Retrieve all the Wall types
            List<WallType> wallTypesList =
                    (from WallType wt in new FilteredElementCollector(_doc)
                     .OfClass(typeof(WallType))
                     .OfCategory(BuiltInCategory.OST_Walls)
                     where wt.FamilyName == Default.FAMILY_NAME_BASIC_WALL
                     select wt)
                     .Cast<WallType>()
                     .ToList();

            _wallTypesList = wallTypesList;
        }

        public void GetWallTypeProperties(WallType wallType,out bool isStructural, out string wallMat, out double wallThickness)
        {
            CompoundStructureLayer strLayer =
                (from CompoundStructureLayer layer in wallType.GetCompoundStructure().GetLayers()
                 where layer.Function == MaterialFunctionAssignment.Structure
                 select layer)
                 .FirstOrDefault();

            if (strLayer != null)
            {
                isStructural = true;

                if (strLayer.MaterialId != ElementId.InvalidElementId)
                {
                    wallMat = _doc.GetElement(strLayer.MaterialId).Name;
                }
                else
                {
                    wallMat = String.Empty;
                }
                
                wallThickness = UnitUtils.Convert(
                        strLayer.Width,
                        DisplayUnitType.DUT_DECIMAL_FEET,
                        DisplayUnitType.DUT_CENTIMETERS);
            }
            else
            {
                isStructural = false;
                wallMat = String.Empty;
                wallThickness = 0;
            }
        }

        public void AdjustWholeWallFamilyTypeName()
        {
            foreach (WallType wallType in WallTypesList)
            {
                AdjustWallTypeName(wallType);
            }
        }

        public void AdjustWallTypeName(WallType wallType)
        {
            double wallThickness;
            string wallMat;
            bool isStructural;
            GetWallTypeProperties(wallType, out isStructural, out wallMat, out wallThickness);
            if (isStructural)
            {
                string matSign = RvtMaterial.GetMatSyntaxe(wallMat);

                string targetWallTypeName = $"{Default.SYNT_WALL}-{matSign}-EP{wallThickness}";

                if (!wallType.Name.Equals(targetWallTypeName))
                {
                    Transaction t = new Transaction(_doc);
                    t.Start("Change wall type name");
                    wallType.Name = targetWallTypeName;
                    t.Commit();
                }
            }
        }

        public void AdjustWholeWallFamilyProperties()
        {
            foreach (WallType wallType in WallTypesList)
            {
                // if the wall type name is in format of "MUR-BAdd-EPdd", update the properties
                Regex regex = new Regex(@"MUR-BA\d{2}-EP\d{2,}");
                Match match = regex.Match(wallType.Name);
                if (match.Success)
                {
                    AdjustWallTypeProperties(wallType);
                }
            }
        }

        public void AdjustWallTypeProperties(WallType wallType)
        {
            string actualTypeName = wallType.Name;

            string targetMatSign = actualTypeName.Between("-", "-");
            string targetWallMat = RvtMaterial.GetMatName(targetMatSign);
            
            double targetThickness = Convert.ToDouble(actualTypeName.After("EP"));

            double wallThickness;
            string wallMat;
            bool isStructural;
            GetWallTypeProperties(wallType, out isStructural, out wallMat, out wallThickness);

            if (isStructural)
            {
                if (!wallMat.Equals(targetWallMat))
                {
                    Material targetMaterial =
                        (from Material m in new FilteredElementCollector(_doc)
                         .OfClass(typeof(Material))
                         where m != null
                         select m)
                         .Cast<Material>()
                         .FirstOrDefault(m => m.Name.Equals(targetWallMat));

                    Transaction t = new Transaction(_doc);
                    t.Start("Change the material of the structural layer of the wall type");
                    foreach (CompoundStructureLayer layer in wallType.GetCompoundStructure().GetLayers())
                    {
                        if (layer.Function == MaterialFunctionAssignment.Structure)
                        {
                            layer.MaterialId = targetMaterial.Id;
                        }
                    }
                    t.Commit();
                }

                if (wallThickness != targetThickness)
                {
                    TaskDialog.Show("Revit", $"{isStructural}, {targetThickness}, {wallThickness}");
                    Transaction t = new Transaction(_doc);
                    t.Start("Change the structural layer thickness of the wall type property");
                    foreach (CompoundStructureLayer layer in wallType.GetCompoundStructure().GetLayers())
                    {
                        if (layer.Function == MaterialFunctionAssignment.Structure)
                        {
                            layer.Width = UnitUtils.Convert(targetThickness, DisplayUnitType.DUT_CENTIMETERS, DisplayUnitType.DUT_DECIMAL_FEET);
                        }
                    }
                    t.Commit();
                }
            }
        }
    }
}
