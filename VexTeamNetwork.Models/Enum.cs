using System.ComponentModel.DataAnnotations;

namespace VexTeamNetwork.Models
{
    public enum Grade
    {
        [Display(Name = "Unknown", ShortName = "?", Description = "")]
        Unknown,
        [Display(Name = "Middle School", ShortName = "MS", Description = "Middle School")]
        MiddleSchool,
        [Display(Name = "High School", ShortName = "HS", Description = "High School")]
        HighSchool,
        [Display(Name = "University", ShortName = "UN", Description = "College")]
        University,
        [Display(Name = "Elementary School", ShortName = "EL", Description = "Elementary")]
        ElementarySchool
    }

    public enum Program
    {
        [Display(Name = "VRC", Description = "VEX Robotics Competition")]
        VRC,
        [Display(Name = "VEXU", Description = "VEXU")]
        VEXU,
        [Display(Name = "VIQ", Description = "VEX IQ")]
        VEXIQ
    }
}