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
        [Display(Name = "Unknown", ShortName = "?", Description = "")]
        Unknown,
        [Display(Name = "VRC", Description = "VEX Robotics Competition")]
        VRC,
        [Display(Name = "VEXU", Description = "VEXU")]
        VEXU,
        [Display(Name = "VIQ", Description = "VEX IQ")]
        VEXIQ
    }

    public enum Round
    {
        [Display(Name = "Practice", ShortName = "P")]
        Practice,
        [Display(Name = "Qualification", ShortName = "Qual")]
        Qualification,
        [Display(Name = "Quarterfinal", ShortName = "QF")]
        QuarterFinal,
        [Display(Name = "Semifinal", ShortName = "SF")]
        SemiFinal,
        [Display(Name = "Final", ShortName = "F")]
        Final,
        [Display(Name = "Round Robin", ShortName = "RR")]
        RoundRobin
    }
}