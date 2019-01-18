using Autodesk.Revit.DB;
using Autodesk.Revit.DB.ExtensibleStorage;
using Autodesk.Revit.UI;
using DCEStudyTools.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCEStudyTools.Configuration
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class LevelConfiguration : IExternalCommand
    {
        private UIApplication _uiapp;
        private UIDocument _uidoc;
        private Document _doc;
        
        private List<Level> _strLevelsAfterConfig;
        private List<Level> _allLevels;
        private string[] _actualStrLevelNames;
        private string[] _allLevelNames;

        public string[] AllLevelNames
        {
            get
            {
                return _allLevelNames;
            }
        }

        public string [] ActuralStrLevelNames
        {
            get
            {
                return _actualStrLevelNames;
            }
        }

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            _uiapp = commandData.Application;
            _uidoc = _uiapp.ActiveUIDocument;
            _doc = _uidoc.Document;

            _allLevels =
                new FilteredElementCollector(_doc)
                .OfClass(typeof(Level)).Cast<Level>()
                .OrderBy(lev => lev.Elevation)
                .ToList();

            _allLevelNames = 
                (from lev in new FilteredElementCollector(_doc)
                .OfClass(typeof(Level)).Cast<Level>()
                .OrderBy(lev => lev.Elevation)
                select lev.Name).ToArray();

            _actualStrLevelNames =
                (from lev in new FilteredElementCollector(_doc)
                 .OfClass(typeof(Level)).Cast<Level>()
                 where lev.GetEntitySchemaGuids().Count != 0
                 select lev.Name).ToArray();

            LevelConfigForm form = new LevelConfigForm(this);

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return Result.Succeeded;
            }
            else
            {
                return Result.Cancelled;
            }
        }

        public void AddSTRMarkToLevel(List<string> strLevelNames)
        {
            using (Transaction trans = new Transaction(_doc, "Create Extensible Store"))
            {
                trans.Start();

                SchemaBuilder builder = new SchemaBuilder(Guids.LEVEL_SHEMA_GUID);

                builder.SetReadAccessLevel(AccessLevel.Public);
                builder.SetWriteAccessLevel(AccessLevel.Public);

                builder.SetSchemaName("StructuralLevels");

                builder.SetDocumentation("Indicate if the level is for structural elements and the number of the level");

                // Create field1
                FieldBuilder fieldBuilder1 = builder.AddSimpleField("StrLevelNumber", typeof(int));

                // Register the schema object
                Schema schema = builder.Finish();

                Field levelNumber = schema.GetField("StrLevelNumber");

                _strLevelsAfterConfig =
                    (from lev in new FilteredElementCollector(_doc)
                     .OfClass(typeof(Level))
                     .Cast<Level>()
                     .OrderBy(lev => lev.Elevation)
                     where strLevelNames.Any(name => name.Equals(lev.Name))
                     select lev)
                     .ToList();

                // Delete the entity for all levels
                foreach (Level lev in _allLevels)
                {
                    lev.DeleteEntity(schema);
                }

                foreach (Level lev in _strLevelsAfterConfig)
                {
                    Entity ent = new Entity(schema);

                    ent.Set(levelNumber, _strLevelsAfterConfig.IndexOf(lev));

                    lev.SetEntity(ent);
                }

                trans.Commit();
            }
        }
    }
}
