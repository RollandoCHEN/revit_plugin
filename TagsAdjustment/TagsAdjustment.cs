using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

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
                AdjustBeamTags(form.BeamTagWithDimension, form.BeamTagWithoutDimension, form.NoDimentionForAllBN);
            
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
            FilteredElementCollector fec = new FilteredElementCollector(_doc)
                 .OfCategory(BuiltInCategory.OST_StructuralColumnTags)
                 .OfClass(typeof(IndependentTag));

            // Loop for each tag to check if it's necessary to change tis type
            foreach (IndependentTag tag in fec)
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
                        FamilySymbol newColmnTagType = GetFamilyTypeByName(
                            _doc,
                            BuiltInCategory.OST_StructuralColumnTags,
                            roundColTagName);
                        tag.ChangeTypeId(newColmnTagType.Id);
                    }
                    // if the tag type is not the same as the column type, change it
                    if (sColumnFamily.Equals(squareColFamilyName) && !tagType.Name.Contains("carré"))
                    {
                        FamilySymbol newColmnTagType = GetFamilyTypeByName(
                            _doc,
                            BuiltInCategory.OST_StructuralColumnTags,
                            squareColTagName);
                        tag.ChangeTypeId(newColmnTagType.Id);
                    }
                    // if the tag type is not the same as the column type, change it
                    if (sColumnFamily.Equals(rectColFamilyName) && !tagType.Name.Contains("rectang"))
                    {
                        FamilySymbol newColmnTagType = GetFamilyTypeByName(
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

            IList<IndependentTag> allTagList =
                new FilteredElementCollector(_doc)
                 .OfCategory(BuiltInCategory.OST_StructuralFramingTags)
                 .OfClass(typeof(IndependentTag))
                 .Cast<IndependentTag>().ToList();

            // Get all the structural framing elements 
            // whose value of "Poutre type" is "BN" or "Talon PV"
            FilteredElementCollector colBN
                = GetElementsByShareParamValue(
                    _doc,
                    BuiltInCategory.OST_StructuralFraming,
                    "Poutre type", "BN");
            FilteredElementCollector colWidth20
                = GetElementsByShareParamValue(
                    _doc,
                    BuiltInCategory.OST_StructuralFraming,
                    "Largeur", 0.2);
            FilteredElementCollector colPV
                = GetElementsByShareParamValue(
                    _doc,
                    BuiltInCategory.OST_StructuralFraming,
                    "Poutre type", "Talon PV");

            IList<IndependentTag> tagWithoutDList = new List<IndependentTag>();

            if (noDimensionForAllBN)
            {
                tagWithoutDList =
                (from tag in new FilteredElementCollector(_doc)
                 .OfCategory(BuiltInCategory.OST_StructuralFramingTags)
                 .OfClass(typeof(IndependentTag))
                 .Cast<IndependentTag>()
                 where
                 colBN.Any(x => x.Id == tag.GetTaggedLocalElement().Id)
                 || colPV.Any(x => x.Id == tag.GetTaggedLocalElement().Id)
                 select tag)
                 .ToList();
            }
            else
            {
                tagWithoutDList =
                    (from tag in new FilteredElementCollector(_doc)
                     .OfCategory(BuiltInCategory.OST_StructuralFramingTags)
                     .OfClass(typeof(IndependentTag))
                     .Cast<IndependentTag>()
                     where
                     (colBN.Any(x => x.Id == tag.GetTaggedLocalElement().Id) && colWidth20.Any(x => x.Id == tag.GetTaggedLocalElement().Id))
                     || colPV.Any(x => x.Id == tag.GetTaggedLocalElement().Id)
                     select tag)
                     .ToList();
            }

            StringBuilder sb = new StringBuilder();

            // Get new framing tag type to change
            FamilySymbol tagTypeWithoutD = GetFamilyTypeByName(
                _doc,
                BuiltInCategory.OST_StructuralFramingTags,
                withoutDimensionTagName);
            FamilySymbol tagTypeWithD = GetFamilyTypeByName(
                _doc,
                BuiltInCategory.OST_StructuralFramingTags,
                withDimensionTagName);


            using (Transaction t = new Transaction(_doc, "Change beam tags type"))
            {
                t.Start();
                foreach (Element elem in allTagList)
                {
                    if (tagWithoutDList.Any(x => x.Id == elem.Id))
                    {
                        elem.ChangeTypeId(tagTypeWithoutD.Id);
                    }
                    else
                    {
                        elem.ChangeTypeId(tagTypeWithD.Id);
                    }
                }
                t.Commit();
            }
        }

        private static FamilySymbol GetFamilyTypeByName(Document doc, BuiltInCategory bic, string typeName)
        {
            return (from tt in new FilteredElementCollector(doc)
                                .OfCategory(bic)
                                .OfClass(typeof(FamilySymbol))
                                .Cast<FamilySymbol>()
                    where tt.Name.Equals(typeName)
                    select tt).First();
        }

        private FilteredElementCollector GetElementsByShareParamValue(
            Document doc, BuiltInCategory bic, string sParaName, string sValue
            )
        {
            SharedParameterElement para =
                                (from p in new FilteredElementCollector(doc)
                                 .OfClass(typeof(SharedParameterElement))
                                 .Cast<SharedParameterElement>()
                                 where p.Name.Equals(sParaName)
                                 select p).First();

            ParameterValueProvider provider
                = new ParameterValueProvider(para.Id);

            FilterStringRuleEvaluator evaluator
                = new FilterStringEquals();

            string sType = sValue;
            
            FilterRule rule = new FilterStringRule(
                provider, evaluator, sType, false);
            
            ElementParameterFilter filter
                = new ElementParameterFilter(rule);

            FilteredElementCollector collector
                = new FilteredElementCollector(doc)
                .OfCategory(bic)
                .WherePasses(filter);
            return collector;
        }

        private FilteredElementCollector GetElementsByShareParamValue(
            Document doc, BuiltInCategory bic, string sParaName, double dValue
            )
        {
            SharedParameterElement para =
                                (from p in new FilteredElementCollector(doc)
                                 .OfClass(typeof(SharedParameterElement))
                                 .Cast<SharedParameterElement>()
                                 where p.Name.Equals(sParaName)
                                 select p).First();

            ParameterValueProvider provider
                = new ParameterValueProvider(para.Id);

            FilterNumericRuleEvaluator evaluator
                = new FilterNumericEquals();

            double sLength_feet = UnitUtils.Convert(
                dValue, 
                DisplayUnitType.DUT_METERS, 
                DisplayUnitType.DUT_DECIMAL_FEET);
            double epsilon = 0.0001;

            FilterRule rule = new FilterDoubleRule(
                provider, evaluator, sLength_feet, epsilon);

            ElementParameterFilter filter
                = new ElementParameterFilter(rule);

            FilteredElementCollector collector
                = new FilteredElementCollector(doc)
                .OfCategory(bic)
                .WherePasses(filter);
            return collector;
        }
    }
}

