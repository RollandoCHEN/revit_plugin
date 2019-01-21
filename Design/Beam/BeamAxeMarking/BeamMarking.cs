using Autodesk.Revit.DB;
using Autodesk.Revit.DB.ExtensibleStorage;
using Autodesk.Revit.UI;
using DCEStudyTools.Design.Beam.BeamCreation;
using DCEStudyTools.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DCEStudyTools.Design.Beam.BeamAxeMarking
{
    class BeamMarking
    {
        protected const double HEIGHT_BY_DEFAULT = 20;
        protected const double WIDTH_BY_DEFAULT = 20;
        protected string TYPE_BY_DEFAULT = BeamCreation.BeamType.NORMALE.Name;

        public static void CreateEntitiesForModelLineAndSetValue(
            Document doc, ModelLine line, 
            double height, double width, string type, ElementId id)
        {
            
            // Create Transaction for working with schema
            using (Transaction trans = new Transaction(doc, "Create Extensible Store"))
            {
                trans.Start();

                // Create a schema builder
                SchemaBuilder builder = new SchemaBuilder(Guids.MODELLINE_SCHEMA_GUID);

                // Set read and write access levels
                builder.SetReadAccessLevel(AccessLevel.Public);

                builder.SetWriteAccessLevel(AccessLevel.Public);

                // Set name to this schema builder
                builder.SetSchemaName("BeamDimension");

                builder.SetDocumentation(

                    "Data store for dimension of the potential structural beam");

                // Create field1
                FieldBuilder fieldBuilder1 =
                  builder.AddSimpleField("SectionHeight", typeof(double));

                // Set unit type
                fieldBuilder1.SetUnitType(UnitType.UT_Length);

                // Create field2
                FieldBuilder fieldBuilder2 =
                  builder.AddSimpleField("SectionWidth", typeof(double));

                // Set unit type
                fieldBuilder2.SetUnitType(UnitType.UT_Length);

                // Create field3
                FieldBuilder fieldBuilder3 =
                  builder.AddSimpleField("BeamType", typeof(String));

                // Create field4
                FieldBuilder fieldBuilder4 =
                  builder.AddSimpleField("BeamId", typeof(ElementId));

                // Register the schema object
                Schema schema = builder.Finish();

                // Now create entity (object) for this schema (class)
                Entity ent = new Entity(schema);
                
                if (null != line)
                {
                    line.SetEntity(ent);
                }
                trans.Commit();

                SetEntityFieldsValue(doc, line, height, width, type, id);

            }
        }

        public static void SetEntityFieldsValue(
            Document doc, ModelLine line, 
            double height, double width, string type, ElementId id)
        {
            Schema schema = Schema.Lookup(Guids.MODELLINE_SCHEMA_GUID);
            Entity ent = line.GetEntity(schema);

            Field sectionHeight = schema.GetField("SectionHeight");

            Field sectionWidth = schema.GetField("SectionWidth");

            Field beamType = schema.GetField("BeamType");

            Field beamId = schema.GetField("BeamId");

            using (Transaction trans = new Transaction(doc, "Set value to entities"))
            {
                trans.Start();
                // Set value to entity
                ent.Set(sectionHeight, height, DisplayUnitType.DUT_CENTIMETERS);
                ent.Set(sectionWidth, width, DisplayUnitType.DUT_CENTIMETERS);
                ent.Set(beamType, type);

                if (id != ElementId.InvalidElementId)
                {
                    ent.Set(beamId, id);
                }

                if (null != line)
                {
                    line.SetEntity(ent);
                }
                trans.Commit();
            }
        }

        protected ModelLine CreateModelLine(Document doc, XYZ startPoint, XYZ endPoint, GraphicsStyle lineStyle)
        {
            View active = doc.ActiveView;

            // Set category "Line" visible
            if (active.ViewTemplateId == ElementId.InvalidElementId)        // No view template applied on active view
            {
                // Set category "Line" visible for active view
                List<BuiltInCategory> bitList = new List<BuiltInCategory>()
                {
                    BuiltInCategory.OST_Lines
                };
                Utils.ViewTemplate.SetCategoriesVisible(doc, active, bitList);
            }
            else
            {
                View template = doc.GetElement(active.ViewTemplateId) as View;

                // Set category "Line" visible for view template
                List<BuiltInCategory> bitList = new List<BuiltInCategory>()
                {
                    BuiltInCategory.OST_Lines
                };
                Utils.ViewTemplate.SetCategoriesVisible(doc, template, bitList);
            }
            

            // Create ModelLine in doc
            Line geoLine = Line.CreateBound(startPoint, endPoint);
            ModelLine line;
            using (Transaction t = new Transaction(doc, "Create Model Line"))
            {
                t.Start();
                line = doc.Create.NewModelCurve(geoLine, active.SketchPlane) as ModelLine;
                line.LineStyle = lineStyle;
                t.Commit();
            }

            return line;
        }

        protected void CreateOrGetLineStyle(
            Document doc,
            XYZ startPoint, XYZ endPoint,
            out GraphicsStyle lineStyle)
        {

            Category LinesCat = doc.Settings.Categories.get_Item(BuiltInCategory.OST_Lines);
            if (LinesCat.SubCategories.Contains("Axe Poutre"))
            {
                Category SubCat = LinesCat.SubCategories.get_Item("Axe Poutre");
                lineStyle = SubCat.GetGraphicsStyle(GraphicsStyleType.Projection);
            }
            else
            {
                lineStyle = NewLineStyle(doc, "Axe Poutre", 4, new Color(255, 0, 255), "Caché");
            }
        }

        protected bool Get2PointsFromUser(
            UIDocument uidoc,
            out XYZ startPoint, out XYZ endPoint)
        {
            startPoint = null;
            endPoint = null;
            do
            {
                try
                {
                    startPoint = uidoc.Selection.PickPoint("Ciquez pour saisir le point de départ de la poutre.");
                }
                catch (Autodesk.Revit.Exceptions.OperationCanceledException)
                {
                    return false;
                }

                try
                {
                    endPoint = uidoc.Selection.PickPoint("Saisissez le point d'arrivée de la poutre.");
                }
                catch (Autodesk.Revit.Exceptions.OperationCanceledException)
                {
                    endPoint = null;
                }
            } while (endPoint == null);
            return true;
        }

        protected GraphicsStyle NewLineStyle(
            Document doc,
            string styleName,
            int weight,
            Color color,
            string patternName)
        {
            // Use this to access the current document in a macro.
            //
            //Document doc = this.ActiveUIDocument.Document;

            // Find existing linestyle.  Can also opt to
            // create one with LinePatternElement.Create()

            FilteredElementCollector fec
              = new FilteredElementCollector(doc)
                .OfClass(typeof(LinePatternElement));

            LinePatternElement linePatternElem = fec
              .Cast<LinePatternElement>()
              .First(linePattern
               => linePattern.Name == patternName);

            // The new linestyle will be a subcategory 
            // of the Lines category        

            Categories categories = doc.Settings.Categories;

            Category lineCat = categories.get_Item(
              BuiltInCategory.OST_Lines);

            Category newLineStyleCat;
            using (Transaction t = new Transaction(doc, "Create LineStyle"))
            {
                t.Start();

                // Add the new linestyle 

                newLineStyleCat = categories
                  .NewSubcategory(lineCat, styleName);

                doc.Regenerate();

                // Set the linestyle properties 
                // (weight, color, pattern).

                newLineStyleCat.SetLineWeight(
                    weight,
                  GraphicsStyleType.Projection);

                newLineStyleCat.LineColor = color;

                newLineStyleCat.SetLinePatternId(
                  linePatternElem.Id,
                  GraphicsStyleType.Projection);

                t.Commit();
            }

            return newLineStyleCat.GetGraphicsStyle(GraphicsStyleType.Projection);
        }
    }
}
