using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

using static DCEStudyTools.Utils.Getter.RvtElementGetter;
using static DCEStudyTools.Properties.Settings;

namespace DCEStudyTools.TagsAdjustment
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class TagsAdjustment : IExternalCommand
    {
        private UIApplication _uiapp;
        private UIDocument _uidoc;
        private Document _doc;
        

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            _uiapp = commandData.Application;
            _uidoc = _uiapp.ActiveUIDocument;
            _doc = _uidoc.Document;

            TagTypeForm form = new TagTypeForm(_doc);

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                AdjustBeamTags(
                    form.BeamTagWithDimension, 
                    form.BeamTagWithoutDimension, 
                    form.NoDimentionForAllBN);
            
                AdjustColumnTags(
                    form.RoundColumnFamilyName,
                    form.SquareColumnFamilyName,
                    form.RectColumnFamilyName,
                    form.RoundColumnTagName,
                    form.SquareColumnTagName,
                    form.RectColumnTagName);

                return Result.Succeeded;
            }
            else
            {
                return Result.Cancelled;
            }

        }

        private void AdjustColumnTags(
            string roundColFamilyName,
            string squareColFamilyName,
            string rectColFamilyName,
            string roundColTagName,
            string squareColTagName,
            string rectColTagName)
        {

            // Get all the column tags
            IList<IndependentTag> allTagsList = GetAllTagsInCategory(_doc, BuiltInCategory.OST_StructuralColumnTags);

            // Loop for each tag to check if it's necessary to change its type
            foreach (IndependentTag tag in allTagsList)
            {
                // Get column tagged by this tag
                FamilyInstance columnInstence = tag.GetTaggedLocalElement() as FamilyInstance;

                // Column family name identify the section form, instead of type name as for the column tags
                // Get column family name
                FamilySymbol columnType = columnInstence.Symbol;
                string sColumnFamily = columnType.FamilyName;
                // Get tag type name
                FamilySymbol tagType = _doc.GetElement(tag.GetTypeId()) as FamilySymbol;
                string sTagType = tagType.Name;

                using (Transaction t = new Transaction(_doc, "Change column tags type"))
                {
                    t.Start();
                    // if the tag type is not the same as the column type, change it
                    if (sColumnFamily.Equals(roundColFamilyName) && !tagType.Name.Contains("arrondi"))
                    {
                        FamilySymbol newColmnTagType = GetFamilySymbolByName(
                            _doc,
                            BuiltInCategory.OST_StructuralColumnTags,
                            roundColTagName);
                        tag.ChangeTypeId(newColmnTagType.Id);
                    }
                    // if the tag type is not the same as the column type, change it
                    if (sColumnFamily.Equals(squareColFamilyName) && !tagType.Name.Contains("carré"))
                    {
                        FamilySymbol newColmnTagType = GetFamilySymbolByName(
                            _doc,
                            BuiltInCategory.OST_StructuralColumnTags,
                            squareColTagName);
                        tag.ChangeTypeId(newColmnTagType.Id);
                    }
                    // if the tag type is not the same as the column type, change it
                    if (sColumnFamily.Equals(rectColFamilyName) && !tagType.Name.Contains("rectang"))
                    {
                        FamilySymbol newColmnTagType = GetFamilySymbolByName(
                            _doc,
                            BuiltInCategory.OST_StructuralColumnTags,
                            rectColTagName);
                        tag.ChangeTypeId(newColmnTagType.Id);
                    }
                    t.Commit();
                }
            }
        }

        private void AdjustBeamTags(string withDimensionTagName, string withoutDimensionTagName, bool noDimensionForAllBN)
        {
            // Adjust beam tags

            IList<IndependentTag> allBeamTagsList = GetAllTagsInCategory(_doc, BuiltInCategory.OST_StructuralFramingTags);

            // Get all the structural framing elements 
            // whose value of "Poutre type" is "BN" or "Talon PV"
            FilteredElementCollector collectorBN
                = GetElementsByShareParamValue(
                    _doc,
                    BuiltInCategory.OST_StructuralFraming,
                    "Poutre type", "BN");
            FilteredElementCollector collectorWidth20
                = GetElementsByShareParamValue(
                    _doc,
                    BuiltInCategory.OST_StructuralFraming,
                    "Largeur", 0.2);
            FilteredElementCollector collectorPV
                = GetElementsByShareParamValue(
                    _doc,
                    BuiltInCategory.OST_StructuralFraming,
                    "Poutre type", "Talon PV");

            IList<IndependentTag> tagWithoutDimList = new List<IndependentTag>();

            if (noDimensionForAllBN)
            {
                tagWithoutDimList =
                (from tag in allBeamTagsList
                 where collectorBN.Any(x => x.Id == tag.GetTaggedLocalElement().Id)
                 || collectorPV.Any(x => x.Id == tag.GetTaggedLocalElement().Id)
                 select tag)
                 .ToList();
            }
            else
            {
                tagWithoutDimList =
                    (from tag in allBeamTagsList
                     where (collectorBN.Any(x => x.Id == tag.GetTaggedLocalElement().Id) 
                     && collectorWidth20.Any(x => x.Id == tag.GetTaggedLocalElement().Id))
                     || collectorPV.Any(x => x.Id == tag.GetTaggedLocalElement().Id)
                     select tag)
                     .ToList();
            }

            StringBuilder sb = new StringBuilder();

            // Get new framing tag type to change
            FamilySymbol tagTypeWithoutDim 
                = GetFamilySymbolByName(
                    _doc,
                    BuiltInCategory.OST_StructuralFramingTags,
                    withoutDimensionTagName);
            FamilySymbol tagTypeWithDim 
                = GetFamilySymbolByName(
                    _doc,
                    BuiltInCategory.OST_StructuralFramingTags,
                    withDimensionTagName);


            using (Transaction t = new Transaction(_doc, "Change beam tags type"))
            {
                t.Start();
                foreach (Element elem in allBeamTagsList)
                {
                    if (tagWithoutDimList.Any(x => x.Id == elem.Id))
                    {
                        elem.ChangeTypeId(tagTypeWithoutDim.Id);
                    }
                    else
                    {
                        elem.ChangeTypeId(tagTypeWithDim.Id);
                    }
                }
                t.Commit();
            }
        }
    }
}

