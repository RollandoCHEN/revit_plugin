using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCEStudyTools.Utils
{
    public class RvtMaterial
    {
        public static readonly RvtMaterial BA25 = new RvtMaterial(
            Properties.Settings.Default.MAT_NAME_BA25,
            Properties.Settings.Default.MAT_SYNTAXE_BA25);

        public static readonly RvtMaterial BA30 = new RvtMaterial(
            Properties.Settings.Default.MAT_NAME_BA30,
            Properties.Settings.Default.MAT_SYNTAXE_BA30);

        public static readonly RvtMaterial BA35 = new RvtMaterial(
            Properties.Settings.Default.MAT_NAME_BA35,
            Properties.Settings.Default.MAT_SYNTAXE_BA35);

        public static readonly RvtMaterial BA40 = new RvtMaterial(
            Properties.Settings.Default.MAT_NAME_BA40,
            Properties.Settings.Default.MAT_SYNTAXE_BA40);

        public static readonly RvtMaterial BA45 = new RvtMaterial(
            Properties.Settings.Default.MAT_NAME_BA45,
            Properties.Settings.Default.MAT_SYNTAXE_BA45);

        public static IEnumerable<RvtMaterial> Values
        {
            get
            {
                yield return BA25;
                yield return BA30;
                yield return BA35;
                yield return BA40;
                yield return BA45;
            }
        }

        public string MatName { get; private set; }
        public string MatSyntaxe { get; private set; }

        public RvtMaterial(string rvtMatName, string syntaxeBA)
        {
            MatName = rvtMatName;
            MatSyntaxe = syntaxeBA;
        }

        public static string GetMatSyntaxe(string mat)
        {
            foreach (RvtMaterial rvtMat in Values)
            {
                if (rvtMat.MatName.Equals(mat))
                {
                    return rvtMat.MatSyntaxe;
                }
            }
            return Properties.Settings.Default.MAT_SYNTAXE_OTHER;
        }

        public static string GetMatName(string matSyntaxe)
        {
            foreach (RvtMaterial mat in Values)
            {
                if (mat.MatSyntaxe.Equals(matSyntaxe))
                {
                    return mat.MatName;
                }
            }
            return Properties.Settings.Default.MAT_NAME_BA25;
        }
    }
}
