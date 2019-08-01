using Autodesk.Revit.DB;

namespace DCEStudyTools.Utils.Setter
{
    public class PropertyValueSetter
    {
        static public void SetElementIdValueTo(Element element, string paraName, ElementId value)
        {
            foreach (Parameter pr in element.Parameters)
            {
                if (pr.Definition.Name.Equals(paraName) && pr.StorageType == StorageType.ElementId)
                {
                    pr.Set(value);
                }
            }
        }

        static public void SetDoubleValueTo(Element element, string paraName, double value)
        {
            foreach (Parameter pr in element.Parameters)
            {
                if (pr.Definition.Name.Equals(paraName) && pr.StorageType == StorageType.Double)
                {
                    pr.Set(value);
                }
            }
        }

        static public void SetStringValueTo(Element element, string paraName, string value)
        {
            foreach (Parameter pr in element.Parameters)
            {
                if (pr.Definition.Name.Equals(paraName) && pr.StorageType == StorageType.String)
                {
                    pr.Set(value);
                }
            }
        }
    }
}
