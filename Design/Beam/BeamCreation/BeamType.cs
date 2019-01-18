using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCEStudyTools.Design.Beam.BeamCreation
{
    public class BeamType
    {
        public static readonly BeamType NORMALE = new BeamType("Normale", string.Empty);
        public static readonly BeamType LONGRINE = new BeamType("Longrine", "L");
        public static readonly BeamType BN = new BeamType("Bande noyée", "BN");
        public static readonly BeamType PR = new BeamType("Poutre en relevé", "PR");
        public static readonly BeamType PV = new BeamType("Talon PV", "Talon PV");

        public static IEnumerable<BeamType> Values
        {
            get
            {
                yield return NORMALE;
                yield return LONGRINE;
                yield return BN;
                yield return PR;
                yield return PV;
            }
        }

        public static IEnumerable<string> StringValues
        {
            get
            {
                yield return NORMALE.Name;
                yield return LONGRINE.Name;
                yield return BN.Name;
                yield return PR.Name;
                yield return PV.Name;
            }
        }

        private readonly string _name;
        private readonly string _sign;   // in kilograms

        public BeamType(string name, string sign)
        {
            _name = name;
            _sign = sign;
        }

        public string Name { get { return _name; } }

        public string Sign { get { return _sign; } }

        public override string ToString()
        {
            return _name;
        }

        public static BeamType GetBeamTypeFromName(string name)
        {
            return (from value in Values
             where value.Name.Equals(name)
             select value).First();
        }
    }
}
