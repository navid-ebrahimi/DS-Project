using System;
using System.Collections.Generic;
using System.Linq;

namespace MyClasses
{
    public class Drug
    {
        public string name { get; set; }
        public string price { get; set; }
        public string effects {get; set; }
        public string diseaseAlergies { get; set; }
        public string ownEffects { get; set; }
        public Drug(string name, string price)
        {
            this.name = name;
            this.price = price;
        }
        public void AddOwnEffect(string name, string effect)
        {
            if(this.ownEffects != null) this.ownEffects = " ";
            this.ownEffects += name + "," + effect;
        }
        public void DeleteAlergies(string name)
        {
            var diseaseNames = this.diseaseAlergies.Split(new char[]{' '}, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim())
                .Select(x => x.Split(new char[] {','})).ToArray();
            for(int i=0; i<diseaseNames.Length; i++)
            {
                if(diseaseNames[i][0] != name)
                {
                    this.diseaseAlergies += diseaseNames[i][0] + "," + diseaseNames[i][1];
                    if(i != diseaseNames.Length-1)
                        this.diseaseAlergies += " ";
                }
            }
        }
        public string[] getAlergies()
        {
            if(diseaseAlergies == null) return null;
            var result = diseaseAlergies.Split(new char[]{' '},StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .Select(x => x.Split(new char[] {','})[0]).ToArray();
            return result;
        }
    }
    public class Disease
    {
        public string name { get; set; }
        public string drugsAlergies { get; set; }
        public Disease(string name)
        {
            this.name = name;
        }
        public string[] getDrugsAlergies()
        {
            string[] seperators = new string[] {"(Drug_", ","};
            return this.drugsAlergies.Split(new char[]{';'}).Select(x => x.Trim())
                .Select(x => x.Split(seperators, StringSplitOptions.RemoveEmptyEntries)[0])
                .ToArray();
        }
        public void removeDrugAlergy(string drugName)
        {
            string[] seperators = new string[] {"(Drug_", ")"};
            var drugs = this.drugsAlergies.Split(new char[] {';'})
                .Select(x => x.Trim()).ToArray();
            this.drugsAlergies = "";
            for(int i=0; i<drugs.Length; i++)
            {
                var nameAndSign = drugs[i].Split(seperators, StringSplitOptions.RemoveEmptyEntries)[0].Split(new char[] {','});
                if(nameAndSign[0] != drugName)
                {
                    this.drugsAlergies += "(Drug_" + nameAndSign[0] + "," + nameAndSign[1] + ")";
                    if(i != drugs.Length-1)
                        this.drugsAlergies += " ; ";
                }
            }
            this.drugsAlergies = this.drugsAlergies.Trim(new char[] {' ',';'});
        }
        public string[] getPositiveDrugs()
        {
            List<string> result = new List<string>();
            string[] seperators = new string[] {"(Drug_", ")"};
            var drugs = this.drugsAlergies.Split(new char[] {';'})
                .Select(x => x.Trim()).ToArray();
            this.drugsAlergies = "";
            for(int i=0; i<drugs.Length; i++)
            {
                var nameAndSign = drugs[i].Split(seperators, StringSplitOptions.RemoveEmptyEntries)[0].Split(new char[] {','});
                if(nameAndSign[1]=="+")
                    result.Add(nameAndSign[0]);
            }
            return result.ToArray();
        }
    }
}