using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DCEStudyTools.TagsAdjustment
{
    public partial class TagTypeForm : System.Windows.Forms.Form
    {
        private Document _doc;

        private const string FRAMING_TAG_FAMILY_NAME = "M_Etiquette d'ossature";
        private const string WITH_DIMENSION_FRAMING_TAG_NAME = "Avec dimension - 2mm";
        private const string WITHOUT_DIMENSION_FRAMING_TAG_NAME = "Sans dimension - 2mm";
        private bool _wdTagExist;
        private bool _wodTagExist;

        private const string ROUND_COLUMN_FAMILY_NAME = "ECO - Poteau béton - Arrondi";
        private const string SQUARE_COLUMN_FAMILY_NAME = "ECO - Poteau béton - Carré";
        private const string RECTANGULAR_COLUMN_FAMILY_NAME = "ECO - Poteau béton - Rectangulaire";
        private bool _rdColExist;
        private bool _sColExist;
        private bool _recColExist;

        private const string COLUMN_TAG_FAMILY_NAME = "M_Etiquette de poteau porteur";
        private const string ROUND_COLUMN_TAG_NAME = "Poteau arrondi - 2mm";
        private const string SQUARE_COLUMN_TAG_NAME = "Poteau carré - 2mm";
        private const string RECTANGULAR_COLUMN_TAG_NAME = "Poteau rectangulaire - 2mm";
        private bool _rdTagExist;
        private bool _sTagExist;
        private bool _recTagExist;

        public string BeamTagWithDimension
        {
            get
            {
                return ucBeamTagWithDimension1.SelectedTagName;
            }
        }
        public string BeamTagWithoutDimension
        {
            get
            {
                return ucBeamTagWithoutDimension1.SelectedTagName;
            }
        }

        public string RoundColumnFamilyName
        {
            get
            {
                return ucRoundColumn1.SelectedFamilyName;
            }
        }
        public string SquareColumnFamilyName
        {
            get
            {
                return ucSquareColumn1.SelectedFamilyName;
            }
        }
        public string RectColumnFamilyName
        {
            get
            {
                return ucRectColumn1.SelectedFamilyName;
            }
        }

        public string RoundColumnTagName
        {
            get
            {
                return ucRoundColumn1.SelectedTagName;
            }
        }
        public string SquareColumnTagName
        {
            get
            {
                return ucSquareColumn1.SelectedTagName;
            }
        }
        public string RectColumnTagName
        {
            get
            {
                return ucRectColumn1.SelectedTagName;
            }
        }
        public bool NoDimentionForAllBN
        {
            get
            {
                return noDimension.Checked;
            }
        }

        public TagTypeForm(Document doc)
        {
            InitializeComponent();
            _doc = doc;
        }

        private void TagTypeForm_Load(object sender, EventArgs e)
        {
            // Structural Beams
            ucBeamTagWithDimension1.ComboBoxEnable = false;
            ucBeamTagWithoutDimension1.ComboBoxEnable = false;

            IList<string> beamTagNameList =
                (from fs in new FilteredElementCollector(_doc)
                .OfCategory(BuiltInCategory.OST_StructuralFramingTags)
                .OfClass(typeof(FamilySymbol))
                .Cast<FamilySymbol>()
                where fs.FamilyName.Equals(FRAMING_TAG_FAMILY_NAME)
                select fs.Name)
                .ToList();

            _wdTagExist = beamTagNameList.Any(tagName => tagName.Equals(WITH_DIMENSION_FRAMING_TAG_NAME));
            _wodTagExist = beamTagNameList.Any(tagName => tagName.Equals(WITHOUT_DIMENSION_FRAMING_TAG_NAME));

            ucBeamTagWithDimension1.TagList = beamTagNameList;
            ucBeamTagWithoutDimension1.TagList = beamTagNameList;

            if (_wdTagExist)
            {
                ucBeamTagWithDimension1.SelectedTagName = WITH_DIMENSION_FRAMING_TAG_NAME;
            }

            if (_wodTagExist)
            {
                ucBeamTagWithoutDimension1.SelectedTagName = WITHOUT_DIMENSION_FRAMING_TAG_NAME;
            }

            // Column Family
            ucRoundColumn1.FamilyComboBoxEnable = false;
            ucSquareColumn1.FamilyComboBoxEnable = false;
            ucRectColumn1.FamilyComboBoxEnable = false;

            IList<string> columnFamilyNameList =
                (from f in new FilteredElementCollector(_doc)
                .OfClass(typeof(Family))
                .Cast<Family>()
                 where f.FamilyCategory.Id == 
                 _doc.Settings.Categories.get_Item(BuiltInCategory.OST_StructuralColumns).Id
                 select f.Name)
                .ToList();

            ucRoundColumn1.FamilyList = columnFamilyNameList;
            ucSquareColumn1.FamilyList = columnFamilyNameList;
            ucRectColumn1.FamilyList = columnFamilyNameList;

            _rdColExist = columnFamilyNameList.Any(tagName => tagName.Equals(ROUND_COLUMN_FAMILY_NAME));
            _sColExist = columnFamilyNameList.Any(tagName => tagName.Equals(SQUARE_COLUMN_FAMILY_NAME));
            _recColExist = columnFamilyNameList.Any(tagName => tagName.Equals(RECTANGULAR_COLUMN_FAMILY_NAME));
            
            if (_rdColExist)
            {
                ucRoundColumn1.SelectedFamilyName = ROUND_COLUMN_FAMILY_NAME;
            }
            if (_sColExist)
            {
                ucSquareColumn1.SelectedFamilyName = SQUARE_COLUMN_FAMILY_NAME;
            }
            if (_recColExist)
            {
                ucRectColumn1.SelectedFamilyName = RECTANGULAR_COLUMN_FAMILY_NAME;
            }

            // Column Tags
            ucSquareColumn1.TagComboBoxEnable = false;
            ucRoundColumn1.TagComboBoxEnable = false;
            ucRectColumn1.TagComboBoxEnable = false;

            IList<string> columnTagNameList =
                (from fs in new FilteredElementCollector(_doc)
                .OfCategory(BuiltInCategory.OST_StructuralColumnTags)
                .OfClass(typeof(FamilySymbol))
                .Cast<FamilySymbol>()
                 where fs.FamilyName.Equals(COLUMN_TAG_FAMILY_NAME)
                 select fs.Name)
                .ToList();

            ucRoundColumn1.TagList = columnTagNameList;
            ucSquareColumn1.TagList = columnTagNameList;
            ucRectColumn1.TagList = columnTagNameList;

            _rdTagExist = columnTagNameList.Any(tagName => tagName.Equals(ROUND_COLUMN_TAG_NAME));
            _sTagExist = columnTagNameList.Any(tagName => tagName.Equals(SQUARE_COLUMN_TAG_NAME));
            _recTagExist = columnTagNameList.Any(tagName => tagName.Equals(RECTANGULAR_COLUMN_TAG_NAME));
            
            if (_rdTagExist)
            {
                ucRoundColumn1.SelectedTagName = ROUND_COLUMN_TAG_NAME;
            }
            if (_sTagExist)
            {
                ucSquareColumn1.SelectedTagName = SQUARE_COLUMN_TAG_NAME;
            }
            if (_recTagExist)
            {
                ucRectColumn1.SelectedTagName = RECTANGULAR_COLUMN_TAG_NAME;
            }

        }

        private void defaultCheck_CheckedChanged(object sender, EventArgs e)
        {
            // Beam Tags
            ucBeamTagWithDimension1.ComboBoxEnable = !defaultCheck.Checked;
            ucBeamTagWithoutDimension1.ComboBoxEnable = !defaultCheck.Checked;

            if (defaultCheck.Checked && _wdTagExist)
            {
                ucBeamTagWithDimension1.SelectedTagName = WITH_DIMENSION_FRAMING_TAG_NAME;
            }

            if (defaultCheck.Checked && _wodTagExist)
            {
                ucBeamTagWithoutDimension1.SelectedTagName = WITHOUT_DIMENSION_FRAMING_TAG_NAME;
            }

            // Column Family
            ucRoundColumn1.FamilyComboBoxEnable = !defaultCheck.Checked;
            ucSquareColumn1.FamilyComboBoxEnable = !defaultCheck.Checked;
            ucRectColumn1.FamilyComboBoxEnable = !defaultCheck.Checked;

            if (defaultCheck.Checked && _rdColExist)
            {
                ucRoundColumn1.SelectedFamilyName = ROUND_COLUMN_FAMILY_NAME;
            }
            if (defaultCheck.Checked && _sColExist)
            {
                ucSquareColumn1.SelectedFamilyName = SQUARE_COLUMN_FAMILY_NAME;
            }
            if (defaultCheck.Checked && _recColExist)
            {
                ucRectColumn1.SelectedFamilyName = RECTANGULAR_COLUMN_FAMILY_NAME;
            }

            // Column Tags
            ucRoundColumn1.TagComboBoxEnable = !defaultCheck.Checked;
            ucSquareColumn1.TagComboBoxEnable = !defaultCheck.Checked;
            ucRectColumn1.TagComboBoxEnable = !defaultCheck.Checked;
            
            if (defaultCheck.Checked && _rdTagExist)
            {
                ucRoundColumn1.SelectedTagName = ROUND_COLUMN_TAG_NAME;
            }
            if (defaultCheck.Checked && _sTagExist)
            {
                ucSquareColumn1.SelectedTagName = SQUARE_COLUMN_TAG_NAME;
            }
            if (defaultCheck.Checked && _recTagExist)
            {
                ucRectColumn1.SelectedTagName = RECTANGULAR_COLUMN_TAG_NAME;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
