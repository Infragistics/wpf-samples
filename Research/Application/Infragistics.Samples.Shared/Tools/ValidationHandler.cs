using System;
using System.Collections.Generic;

namespace Infragistics.Samples.Shared.Tools
{
    public class ValidationHandler
    {
        private Dictionary<string, string> BrokenRules { get; set; }
        public ValidationHandler()
        {
            this.BrokenRules = new Dictionary<string, string>();
        }
        public string this[string property]
        {
            get
            {
                return this.BrokenRules[property];
            }
        }
        public bool BrokenRuleExists(string property)
        {
            return this.BrokenRules.ContainsKey(property);
        }
        public bool ValidateRule(string property, string message, Func<bool> ruleCheck)
        {
            if (!ruleCheck())
            {
                this.BrokenRules.Add(property, message);
                return false;
            }
            RemoveBrokenRule(property);
            return true;
        }
        public void RemoveBrokenRule(string property)
        {
            if (this.BrokenRules.ContainsKey(property))
            {
                this.BrokenRules.Remove(property);
            }
        }
    }
}