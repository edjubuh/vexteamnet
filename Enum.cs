using System.ComponentModel.DataAnnotations;

namespace VexTeamNetwork.Models
{
    public enum Grade
    {
        [Display(Name="Middle School", ShortName="MS")]
        MiddleSchool,
        [Display(Name="High School", ShortName="HS")]
        HighSchool,
        [Display(Name="University", ShortName="UN")]
        University
    }

    public enum Program
    {
        [Display(Name="VRC", Description="VEX Robotics Competition")]
        VRC,
        [Display(Name="VEXU", Description="VEXU")]
        VEXU
    }
}