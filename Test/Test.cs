using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;

using static DCEStudyTools.Utils.Getter.RvtElementGetter;
using static DCEStudyTools.Properties.Settings;

namespace DCEStudyTools.Test
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class Test : IExternalCommand
    {
        private UIApplication _uiapp;
        private UIDocument _uidoc;
        private Document _doc;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            _uiapp = commandData.Application;
            _uidoc = _uiapp.ActiveUIDocument;
            _doc = _uidoc.Document;
            
            try
            {
                
                IList<ViewPlan> allStructuralPlans = GetAllStructuralPlans(_doc, false);

                Dictionary<ViewPlan, ViewPlan> viewPlanDic = new Dictionary<ViewPlan, ViewPlan>();


                StringForm form = new StringForm(_doc, allStructuralPlans);

                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    return Result.Succeeded;
                }
                else
                {
                    return Result.Cancelled;
                }

                //// Delete "Etage 1 - " in the level name
                //string pattern = @"(Etage\s?[0-9]{0,2}\s?-?\s?)";

                //Transaction t = new Transaction(_doc, "Modify level name");
                //t.Start();
                //foreach (Level lvl in strLevels)
                //{
                //    lvl.Name = Regex.Replace(lvl.Name, pattern, String.Empty);
                //}
                //t.Commit();
            }
            catch (Exception e)
            {
                message = e.Message;
                return Result.Failed;
            }

        }

        private bool EqualWithTolerance (double a, double b, double delta)
        {
            double delta_feet = UnitUtils.Convert(delta, DisplayUnitType.DUT_METERS, DisplayUnitType.DUT_DECIMAL_FEET);

            if (Math.Abs(a - b) < delta_feet)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

      
    }
}

