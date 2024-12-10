using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BackEnd.Model
{
    public class User
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("username")]
        public string Username { get; set; } = "";

        [Column("password")]
        public string Password { get; set; } = "";

        [Column("organzation_id")]
        public int OrganizationId { get; set; }

        [Column("firstName")]
        public string? FirstName { get; set; }

        [Column("lastName")]
        public string? LastName { get; set; }

        [Column("weight")]
        public decimal? Weight { get; set; }

        [Column("height")]
        public decimal? Height { get; set; }

        [Column("age")]
        public int? Age { get; set; }

        [Column("genders")]
        public Genders? Gender { get; set; }

    }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Genders
    {
        Male = 1,
        Female = 2,
        Other = 3
    }
}