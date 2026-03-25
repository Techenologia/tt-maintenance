using System.Collections.Generic;
using TT.Backend.Core.Entities; // <- Ajouté pour UserEntity

namespace TT.Backend.Core.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<UserEntity> Users { get; set; } // <- remplacé User par UserEntity

        public Company()
        {
            Name = string.Empty;
            Users = new List<UserEntity>();
        }
    }
}