using Autodesk.Revit.DB;

namespace DCEStudyTools.Design.Beam.DimensionEditing
{
    public class EntityToUpdateLine
    {
        public EntityToUpdateLine(ModelLine line, double height, double width, string type)
        {
            Line = line;
            Height = height;
            Width = width;
            Type = type;
        }

        public ModelLine Line
        {
            set; get;
        }

        public double Height
        {
            set; get;
        }
        public double Width
        {
            set; get;
        }
        public string Type
        {
            set; get;
        }
    }
}
