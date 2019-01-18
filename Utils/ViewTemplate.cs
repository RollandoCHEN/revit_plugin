using Autodesk.Revit.DB;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DCEStudyTools.Utils
{
    class ViewTemplate
    {
        /*  The BuiltInParameter bip could be one or more of following params :
         *  BuiltInParameter.VIEW_SCALE_PULLDOWN_METRIC,
         *  BuiltInParameter.VIEW_DETAIL_LEVEL,
         *  BuiltInParameter.VIEW_PARTS_VISIBILITY,
         *  BuiltInParameter.VIS_GRAPHICS_MODEL,
         *  BuiltInParameter.VIS_GRAPHICS_ANNOTATION,
         *  BuiltInParameter.VIS_GRAPHICS_ANALYTICAL_MODEL,
         *  BuiltInParameter.VIS_GRAPHICS_IMPORT,
         *  BuiltInParameter.VIS_GRAPHICS_FILTERS,
         *  BuiltInParameter.VIS_GRAPHICS_RVT_LINKS,
         *  BuiltInParameter.GRAPHIC_DISPLAY_OPTIONS_MODEL,
         *  BuiltInParameter.GRAPHIC_DISPLAY_OPTIONS_SHADOWS,
         *  BuiltInParameter.GRAPHIC_DISPLAY_OPTIONS_SKETCHY_LINES,
         *  BuiltInParameter.GRAPHIC_DISPLAY_OPTIONS_LIGHTING,
         *  BuiltInParameter.GRAPHIC_DISPLAY_OPTIONS_PHOTO_EXPOSURE,
         *   BuiltInParameter.GRAPHIC_DISPLAY_OPTIONS_BACKGROUND,
         *  BuiltInParameter.VIEW_PHASE_FILTER,
         *  BuiltInParameter.VIEW_DISCIPLINE,
         *  BuiltInParameter.VIEW_SHOW_HIDDEN_LINES,
         *  BuiltInParameter.VIEWER3D_RENDER_SETTINGS
         */
        public static void IncludParamToViewTemplate(
            Document doc,
            Autodesk.Revit.DB.View viewTemplate, 
            List<BuiltInParameter> bipList)
        {
            Transaction t = new Transaction(doc);
            foreach (BuiltInParameter bip in bipList)
            {
                if (viewTemplate.GetNonControlledTemplateParameterIds().Count != 0)
                {
                    ElementId VMDMode_Id = ElementId.InvalidElementId;
                    List<ElementId> nonControlledParams = viewTemplate.GetNonControlledTemplateParameterIds().ToList();
                    foreach (var id in nonControlledParams)
                    {
                        if (id.IntegerValue == (int)bip)
                        {
                            VMDMode_Id = id;
                            break;
                        }
                    }
                    if (VMDMode_Id != ElementId.InvalidElementId)  // The param is not include in view template
                    {
                        nonControlledParams.Remove(new ElementId(bip));
                        t.Start("Set non controlled params to template");
                        viewTemplate.SetNonControlledTemplateParameterIds(nonControlledParams);
                        t.Commit();
                    }
                }
            }
        }

        public static void SetOnlyCategoriesVisible(
            Document doc, Autodesk.Revit.DB.View view, List<BuiltInCategory> bicList)
        {
            Transaction t = new Transaction(doc);
            t.Start("Set all categories hidden");
            foreach (Category cat in doc.Settings.Categories)
            {
                if (cat.get_AllowsVisibilityControl(view))
                {
                    cat.set_Visible(view, false);
                }
            }
            t.Commit();

            SetCategoriesVisible(doc, view, bicList);
        }

        public static void SetCategoriesVisible(
            Document doc, Autodesk.Revit.DB.View view, List<BuiltInCategory> bicList)
        {
            Transaction t = new Transaction(doc);
            t.Start("Set input categories visible");
            foreach (BuiltInCategory bic in bicList)
            {
                view.SetCategoryHidden(doc.Settings.Categories.get_Item(bic).Id, false);
            }
            t.Commit();
        }

        public static Autodesk.Revit.DB.View FindViewTemplateOrDefault(Document doc,
            ViewType viewType,
            string viewTemplateName,
            out bool templateIsFound)
        {
            // Get the view template
            List<Autodesk.Revit.DB.View> list = (from v in new FilteredElementCollector(doc)
                                .OfClass(typeof(Autodesk.Revit.DB.View))
                                .Cast<Autodesk.Revit.DB.View>()
                                                 where v.IsTemplate == true && v.ViewType == viewType && v.ViewName.Equals(viewTemplateName)
                                                 select v).ToList();
            Autodesk.Revit.DB.View template;

            if (list.Count != 0)         
            {
                templateIsFound = true;
                template = list.First();
            }
            else                    // Dont find the template, get any view template of the given type
            {
                templateIsFound = false;
                template = (from v in new FilteredElementCollector(doc)
                                     .OfClass(typeof(Autodesk.Revit.DB.View))
                                     .Cast<Autodesk.Revit.DB.View>()
                                where v.IsTemplate == true && v.ViewType == viewType
                                select v).FirstOrDefault();
                if (template == null)   // Dont even find an existing view template in the doc
                {
                    string msg = string.Format("Please ensure that there is at least one {0} template in the project!",
                        viewType.ToString());
                    MessageBox.Show(msg, "No view template exists");
                }
            }
            return template;
        }

        public static void AddFilterToViewOrTemplate(Document doc, Autodesk.Revit.DB.View view, SelectionFilterElement filterElement,
            Color cutFillColor, string[] cutFillPatterns, Color projFillColor, string[] projFillPatterns)
        {
            if (view.ViewTemplateId == ElementId.InvalidElementId)        // No view template applied on active view
            {
                // Add filter to view
                AddFilterToView(
                    doc, view, filterElement, 
                    cutFillColor, cutFillPatterns, projFillColor, projFillPatterns);
            }
            else
            {
                Autodesk.Revit.DB.View template = doc.GetElement(view.ViewTemplateId) as Autodesk.Revit.DB.View;

                // Add filter to template
                AddFilterToView(
                    doc, template, filterElement, 
                    cutFillColor, cutFillPatterns, projFillColor, projFillPatterns);
            }
        }

        public static void AddFilterToView(
            Document doc, Autodesk.Revit.DB.View view, SelectionFilterElement filterElement,
            Color cutFillColor, string[] cutFillPatterns, Color projFillColor, string[] projFillPatterns)
        {
            using (Transaction t = new Transaction(doc, "Add filter to view and set overrides"))
            {
                t.Start();
                ElementId filterId = filterElement.Id;
                view.AddFilter(filterId);

                doc.Regenerate();

                OverrideGraphicSettings overrideSettings = view.GetFilterOverrides(filterId);
                Element cutFill = (from element in new FilteredElementCollector(doc)
                                   .OfClass(typeof(FillPatternElement)).Cast<Element>()
                                   where cutFillPatterns.Any(pattern => pattern.Equals(element.Name))
                                   select element)
                                   .FirstOrDefault();
                Element projectFill = (from element in new FilteredElementCollector(doc)
                                       .OfClass(typeof(FillPatternElement)).Cast<Element>()
                                       where projFillPatterns.Any(pattern => pattern.Equals(element.Name))
                                       select element)
                                       .FirstOrDefault();

                overrideSettings.SetCutFillColor(cutFillColor);
                overrideSettings.SetCutFillPatternId(cutFill.Id);
                overrideSettings.SetProjectionFillColor(projFillColor);
                overrideSettings.SetProjectionFillPatternId(projectFill.Id);
                view.SetFilterOverrides(filterId, overrideSettings);
                t.Commit();
            }
        }

        public static void RemoveFilterFromViewOrTemplate(Document doc, Autodesk.Revit.DB.View view, SelectionFilterElement filterElement)
        {
            if (view.ViewTemplateId == ElementId.InvalidElementId)        // No view template applied on active view
            {
                // Remove filter from view
                RemoveFilterFromView(doc, view, filterElement);
            }
            else
            {
                Autodesk.Revit.DB.View template = doc.GetElement(view.ViewTemplateId) as Autodesk.Revit.DB.View;

                // Remove filter from template
                RemoveFilterFromView(doc, template, filterElement);
            }
        }

        public static void RemoveFilterFromView(Document doc, Autodesk.Revit.DB.View view, SelectionFilterElement filterElement)
        {
            Transaction t = new Transaction(doc);
            bool filterExistsInActiveView =
                view.GetFilters().Any(id => doc.GetElement(id).Name.Equals("Voiles Porteurs"));
            if (filterExistsInActiveView)
            {
                t.Start("Remove filter from active view");
                view.RemoveFilter(filterElement.Id);
                t.Commit();
            }
        }
    }
}
