using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Image : BaseEntity
    {
        public string Name { get; set; }

        [Column(TypeName="varchar(max)")]
        public string ImagePath {get; set;} 
        [Column(TypeName="datetime")]
        public DateTime InsertedOn {get; set;}

    }
}