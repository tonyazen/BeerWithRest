using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeerWithRest.Models
{
    public class BeerRequest : IValidatableObject
    {
        public string Name { get; set; }
        public string Style { get; set; }
        public string Abv { get; set; }
        public string Brewery { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (String.IsNullOrEmpty(Name))
                yield return new ValidationResult("Name is required.")
                {
                    ErrorMessage = "Name is required.",
                    //MemberNames = new [] {nameof(Name)}
                };
        }
    }
}