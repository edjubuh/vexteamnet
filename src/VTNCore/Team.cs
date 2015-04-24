using Microsoft.Data.Entity.Metadata;

namespace VexTeamNetwork.Models
{
    public class Team
    {
        public const string NumberRegularExpression = "^[1-9]\\d{0,3}[A-Z]{0,1}$|^[A-Z]{0,4}[1-9]{0,1}$";

        public string Number { get; set; }

        public string Name { get; set; }

        public string RobotName { get; set; }

        public string Organiation { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public string Country { get; set; }

        public Grade Grade { get; set; }

        public Program Program { get; set; }

        public bool IsRegistered { get; set; }
    }

    public static partial class ModelConfiguration
    {
        public static ModelBuilder.EntityBuilder<Team> Configure(this ModelBuilder.EntityBuilder<Team> builder)
        {
            builder.Key("Number");
            builder.Property(t => t.Number).MaxLength(5).Required();

            return builder;
        }
    }
}